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
git clone https://github.com/kauandragues/limk.git
```

2. Para uma instalação nova, execute [banco/limk-completo.sql](banco/limk-completo.sql) no MySQL.
3. Copie `Limk.VolleyBall/.env.example` para `Limk.VolleyBall/.env` e preencha `DB_SERVER`, `DB_PORT`, `DB_NAME`, `DB_USER` e `DB_PASSWORD`. Use `limk` em `DB_NAME` para o banco criado pelo script.
4. Abra `Limk.slnx` no Visual Studio e execute o projeto. Na tela de login, clique em **Criar conta** ou use o e-mail e a senha de um usuário já cadastrado. A coluna `usuario.senha_hash` armazena o hash bcrypt da senha.

Ao publicar o aplicativo, configure as variáveis de conexão no ambiente de destino ou forneça um `.env` local ao lado do executável.

Se você já importou as táticas de exemplo com as equipes em cima e embaixo, execute a [correção da orientação](docs/entrega/corrigir-orientacao-taticas.sql) no banco `limk` e reabra a tática. Nas formações corrigidas, a equipe principal ocupa a metade esquerda da quadra e a adversária ocupa a direita.

As três contas de demonstração do script usam a senha **`Limk@Teste2026!`** no ambiente de desenvolvimento:

| E-mail | Equipe no script |
| --- | --- |
| `ana@limk.example` | LIMK Campinas |
| `bruno@limk.example` | LIMK Campinas |
| `carla@limk.example` | Vôlei Hortolândia |

Digite essa senha no login. O bcrypt não permite recuperar a senha original a partir do hash: `BCrypt.Verify` compara a senha digitada com o hash salvo. Os `INSERTs` usam hashes gerados a partir da senha de demonstração, com um salt próprio para cada conta.

O cadastro pede nome, e-mail, senha e confirmação. A senha precisa ter pelo menos 8 caracteres e respeitar o limite de 72 bytes do bcrypt; ela é gravada somente como hash. E-mails já cadastrados são recusados. Ao concluir, a tela volta ao login com o e-mail preenchido. A nova conta recebe as preferências padrão e começa sem equipes vinculadas; o usuário pode criar sua primeira equipe pela tela **Equipes**. O cadastro usa as tabelas existentes `usuario` e `preferencia_usuario`, sem alterar o esquema do banco.

`Backend/DadosUsuario.cs` valida e grava a conta e suas preferências em uma transação. `User Constrols/TelaCadastroUsuario.Designer.cs` contém o layout editável no Visual Studio; `TelaCadastroUsuario.cs` trata os campos e o botão. `FormLogin.cs` alterna entre login e cadastro na mesma janela.

O login informa o ID, o nome e o e-mail do usuário ao formulário principal. A interface `ITelaUsuario` permite que o formulário entregue essa identidade a cada tela antes de carregá-la, tanto pelo menu quanto pelos atalhos.

O acesso aos dados segue os vínculos da tabela `usuario_equipe`:

- o usuário consulta as equipes às quais está vinculado;
- os atletas aparecem pelos vínculos ativos com essas equipes, preservando o número da camisa de cada equipe;
- as táticas e os quadros seguem a equipe da tática;
- os totais e os cartões do início usam a mesma regra;
- uma conta sem equipes vinculadas apresenta listas vazias e totais iguais a zero.

O usuário administra as equipes às quais está vinculado. Para trocar de conta, clique em **Sair da conta** no menu lateral. O LIMK fecha a tela atual e volta ao login. Fechar a janela de login encerra o programa.

## Usando a prancheta

1. Abra **Prancheta Tática** para iniciar uma tática com uma fase vazia.
2. Com nenhum elemento selecionado, use o painel **Propriedades** à direita para informar nome, equipe, tipo e sistema de jogo da tática, além do nome, duração e rotação da fase selecionada. Clique em uma área vazia da quadra para voltar a esses campos. Tipo, sistema e rotação são opcionais. A equipe de uma tática já salva não pode ser trocada.
3. Adicione atletas e a bola. Ao selecionar um atleta, você pode vinculá-lo ao elenco da equipe, escolher o lado e ajustar o número e a posição. Também é possível usar atletas sem vínculo cadastrado. Cada fase aceita uma bola.
4. Use **Nova fase** para criar uma fase vazia ou **Duplicar** para copiar a fase selecionada. Arraste os cartões para mudar a ordem dos quadros; solte na metade esquerda para inserir antes ou na direita para inserir depois de outro cartão. A numeração e os tempos de início são recalculados, mantendo o quadro em edição. Cada fase mantém seus próprios elementos: mover um atleta ou alterar seu número em uma cópia não modifica a original nem o cadastro do elenco. Clique em **Salvar Tática** para gravar a nova ordem.
5. **Aplicar alterações** atualiza a edição. **Salvar Tática** grava os dados e as fases no banco, incluindo o que foi digitado no painel. Ao trocar de fase, os campos também são mantidos na edição. Salve antes de sair da tela para manter a edição no banco.
6. Abra uma tática pelo repositório **Táticas** ou pelos cartões do início. Clicar nos cartões ou em **Anterior/Próxima** mostra imediatamente o quadro escolhido para edição. **Executar** anima atletas e bola, avançando do primeiro quadro até o último na ordem da sequência. **Duração (ms)** define o tempo para sair do quadro atual e chegar ao seguinte: `1000` corresponde a um segundo. O último quadro permanece visível pela própria duração durante a apresentação. **Parar** interrompe a prévia e restaura as posições do quadro de origem da transição. O botão **Apresentar** do repositório abre e inicia a sequência.

A transição associa atletas cadastrados pelo vínculo com o elenco. Nos atletas sem vínculo, mantenha o mesmo número e lado entre os quadros para representar o mesmo elemento. A bola se move quando está presente nos dois quadros. Elementos que só existem em um dos quadros aparecem ou desaparecem ao concluir a transição. A animação altera apenas o desenho, preservando as posições definidas em cada quadro.

Em **Táticas**, cada cartão tem o botão **Excluir tática**. Confira o nome da jogada e da equipe no aviso. Confirmar remove os quadros e as posições daquela tática. Cancelar mantém a jogada disponível na lista.

### Organização do código

O [guia ilustrado do código](docs/Guia_do_codigo_LIMK.pdf) acompanha a entrada no sistema, o desenho de um atleta, o zoom, o arraste e o salvamento de uma jogada.

- `Backend/Tatica.cs`, `QuadroTatica.cs` e `AtletaQuadro.cs` representam os dados editados. Os métodos `Copiar()` criam cópias independentes das fases, atletas e bola.
- `Tatica.Validar()` verifica as regras da jogada. `DadosPrancheta` verifica o acesso à equipe e grava cabeçalho e fases na mesma transação.
- `User Constrols/TelaPranchetaTatica.cs` liga os botões aos dados e transfere os elementos entre a fase selecionada e o desenho. O painel de propriedades alterna entre tática/fase, atleta e bola conforme a seleção. `PranchetaControl.cs` calcula o retângulo da quadra, desenha os elementos e trata o mouse. O layout está em `TelaPranchetaTatica.Designer.cs`.

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

## Documentação do projeto

- Trabalho escrito final em [PDF](docs/entrega/trabalho-escrito.pdf) e [Word editável](docs/entrega/trabalho-escrito.docx).
- [Especificação de requisitos](docs/LIMK_Requisitos.md).
- [Casos de uso](docs/entrega/casos-de-uso.pdf) e [diagramas de classes atuais](docs/diagramas-de-classes.md). O [PDF da entrega de 21/11](docs/entrega/diagrama-de-classes.pdf) registra a versão anterior à refatoração.
- [Modelo entidade-relacionamento](docs/mer.md), [esquema físico](docs/eer.md) e [diagramas do banco em PDF](docs/entrega/modelo-de-dados.pdf).
- [Script integrado de implantação e demonstração](banco/limk-completo.sql) e [consultas documentadas por tela](docs/entrega/consultas-interfaces.sql).
- [Testes funcionais e roteiro de verificação](docs/testes-funcionais.md).
