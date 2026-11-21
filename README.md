# LIMK Volleyball

O **LIMK Volleyball** é uma aplicação desktop para criação, organização e apresentação de táticas de voleibol.

O sistema foi pensado para ajudar treinadores e membros da comissão técnica a montar jogadas de forma visual, utilizando uma prancheta digital com atletas, bola e diferentes momentos de uma tática.

## Problema que o projeto resolve

Durante treinos, táticas de voleibol podem ser explicadas utilizando pranchetas físicas, desenhos ou anotações separadas.

O LIMK busca centralizar essas informações em uma única aplicação, permitindo:

- cadastrar equipes e atletas;
- montar táticas em uma quadra digital;
- mover atletas e a bola;
- criar sequências de jogadas;
- salvar e recuperar táticas;
- apresentar as jogadas durante os treinos.

## Tecnologias

O projeto utiliza:

- **C#**
- **.NET**
- **Windows Forms**
- **MySQL**
- **Git e GitHub**

A aplicação é desenvolvida em um único projeto Windows Forms e se conecta diretamente ao banco de dados MySQL.

## Como rodar

### Requisitos

Para executar o projeto, é necessário ter instalado:

- .NET SDK;
- Visual Studio com suporte a desenvolvimento desktop com .NET;
- MySQL Server.

### Pelo Visual Studio

1. Clone o repositório:

```bash
git clone <URL_DO_REPOSITORIO>
```

2. Para uma instalação nova, importe `banco/limk_banco.sql` no MySQL.
3. Copie `Limk.VolleyBall/.env.example` para `Limk.VolleyBall/.env` e preencha `DB_SERVER`, `DB_PORT`, `DB_NAME`, `DB_USER` e `DB_PASSWORD`. Use `limk` em `DB_NAME` para o banco criado pelo script.
4. Abra `Limk.slnx` no Visual Studio e execute o projeto. Na tela de login, use o e-mail e a senha de um usuário cadastrado na tabela `usuario`. A coluna `senha_hash` armazena o hash bcrypt da senha.

Ao publicar o aplicativo, configure as variáveis de conexão no ambiente de destino ou forneça um `.env` local ao lado do executável.

As três contas de demonstração do script usam a senha **`Limk@Teste2026!`** no ambiente de desenvolvimento:

| E-mail | Equipe no script |
| --- | --- |
| `ana@limk.example` | LIMK Campinas |
| `bruno@limk.example` | LIMK Campinas |
| `carla@limk.example` | Vôlei Hortolândia |

Digite essa senha no login. O bcrypt não permite recuperar a senha original a partir do hash: `BCrypt.Verify` compara a senha digitada com o hash salvo. Os `INSERTs` usam hashes gerados a partir da senha de demonstração, com um salt próprio para cada conta.

O login informa o ID, o nome e o e-mail do usuário ao formulário principal. A interface `ITelaUsuario` permite que o formulário entregue essa identidade a cada tela antes de carregá-la, tanto pelo menu quanto pelos atalhos.

O acesso aos dados segue os vínculos da tabela `usuario_equipe`:

- o usuário consulta as equipes às quais está vinculado;
- os atletas aparecem pelos vínculos ativos com essas equipes, preservando o número da camisa de cada equipe;
- as táticas e os quadros seguem a equipe da tática;
- os totais e os cartões do início usam a mesma regra;
- uma conta sem equipes vinculadas apresenta listas vazias e totais iguais a zero.

Dois usuários vinculados à mesma equipe compartilham acesso aos dados dela. A troca de usuário descarta a tela anterior e suas informações. Os formulários de cadastro continuam como interface.

## Usando a prancheta

1. Abra **Prancheta Tática** para iniciar uma tática com uma fase vazia.
2. Com nenhum elemento selecionado, use o painel **Propriedades** à direita para informar nome, equipe, tipo e sistema de jogo da tática, além do nome, duração e rotação da fase selecionada. Clique em uma área vazia da quadra para voltar a esses campos. Tipo, sistema e rotação são opcionais. A equipe de uma tática já salva não pode ser trocada.
3. Adicione atletas e a bola. Ao selecionar um atleta, você pode vinculá-lo ao elenco da equipe, escolher o lado e ajustar o número e a posição. Também é possível usar atletas sem vínculo cadastrado. Cada fase aceita uma bola.
4. Use **Nova fase** para criar uma fase vazia ou **Duplicar** para copiar a fase selecionada. Arraste os cartões para mudar a ordem dos quadros; solte na metade esquerda para inserir antes ou na direita para inserir depois de outro cartão. A numeração e os tempos de início são recalculados, mantendo o quadro em edição. Cada fase mantém seus próprios elementos: mover um atleta ou alterar seu número em uma cópia não modifica a original nem o cadastro do elenco. Clique em **Salvar Tática** para gravar a nova ordem.
5. **Aplicar alterações** atualiza a edição. **Salvar Tática** grava os dados e as fases no banco, incluindo o que foi digitado no painel. Ao trocar de fase, os campos também são mantidos na edição. Salve antes de sair da tela para manter a edição no banco.
6. Abra uma tática pelo repositório **Táticas** ou pelos cartões do início. Clicar nos cartões ou em **Anterior/Próxima** mostra imediatamente o quadro escolhido para edição. **Executar** anima atletas e bola, avançando do primeiro quadro até o último na ordem da sequência. **Duração (ms)** define o tempo para sair do quadro atual e chegar ao seguinte: `1000` corresponde a um segundo. O último quadro permanece visível pela própria duração durante a apresentação. **Parar** interrompe a prévia e restaura as posições do quadro de origem da transição. O botão **Apresentar** do repositório abre e inicia a sequência.

A transição associa atletas cadastrados pelo vínculo com o elenco. Nos atletas sem vínculo, mantenha o mesmo número e lado entre os quadros para representar o mesmo elemento. A bola se move quando está presente nos dois quadros. Elementos que só existem em um dos quadros aparecem ou desaparecem ao concluir a transição. A animação altera apenas o desenho, preservando as posições definidas em cada quadro.

### Organização do código

- `Backend/Tatica.cs`, `QuadroTatica.cs` e `AtletaQuadro.cs` representam os dados editados. Os métodos `Copiar()` criam cópias independentes das fases, atletas e bola.
- `Backend/DadosPrancheta.cs` concentra as consultas e gravações no MySQL, a validação dos dados e a verificação de acesso às equipes do usuário.
- `User Constrols/TelaPranchetaTatica.cs` liga os botões aos dados e transfere os elementos entre a fase selecionada e o desenho. O painel de propriedades alterna entre tática/fase, atleta e bola conforme a seleção. Todo o layout continua em `TelaPranchetaTatica.Designer.cs`.

O salvamento utiliza as tabelas existentes `tatica`, `quadro`, `atleta_quadro` e `bola_quadro`. Nome, descrição e referências da tática são gravados junto com todas as fases em uma transação: uma falha desfaz a gravação inteira. Ao atualizar, as fases são substituídas dentro dessa transação, preservando o ID da tática e seus favoritos. Os números são guardados em cada fase, mesmo que o elenco mude depois. As coordenadas permanecem normalizadas entre 0 e 1.

O banco continua com 15 tabelas. A prancheta usa a configuração de conexão já existente no `.env`.

## Gerenciando equipes

A tela **Equipes** mostra somente os vínculos do usuário conectado. Clique em um cartão, no nome, no escudo ou no lápis para carregar a equipe no painel da direita. A busca também mantém o painel de acordo com a seleção visível.

- **Cadastrar nova equipe** e **Limpar** abrem uma ficha vazia. Ao salvar, a nova equipe é vinculada ao usuário conectado.
- **Salvar equipe** grava o nome e o escudo da equipe em edição. Uma equipe compartilhada é atualizada para os outros usuários que possuem acesso a ela.
- **Escolher escudo** aceita uma imagem de até 4 MB, com até 4096 pixels por lado. O cartão e a prévia preservam a proporção. **Remover** retira o escudo da edição; salve para gravar essa mudança.
- **Ver Elenco** abre a tela de atletas já filtrada pela equipe do cartão.
- A **lixeira** pede confirmação. A exclusão remove a equipe, suas táticas, quadros, elementos e atletas exclusivos. Um atleta vinculado a outra equipe mantém seu cadastro e perde somente o vínculo com a equipe excluída. A operação é gravada em uma transação: uma falha desfaz a exclusão inteira.

`Backend/DadosEquipe.cs` reúne as gravações e a verificação de acesso. `Backend/ImagemEscudo.cs` valida e carrega as imagens sem manter o arquivo aberto. O comportamento dos botões fica em `TelaEquipes.cs` e `CardEquipes.cs`; os controles permanecem nos respectivos arquivos `.Designer.cs`.
