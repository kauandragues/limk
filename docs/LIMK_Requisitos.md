# LIMK Volleyball · Requisitos da versão implementada

Revisão de 21 de novembro de 2026. Esta especificação descreve o aplicativo Windows Forms e o esquema MySQL distribuídos com o projeto. Os identificadores dos requisitos foram mantidos para permitir a comparação com as revisões anteriores.

## Objetivo e usuário

O **usuário** administra as equipes às quais está vinculado. Na função de treinador ou integrante da comissão técnica, ele cadastra o elenco, monta uma jogada em fases, salva a tática da equipe e apresenta a sequência durante o treino. A conta que cria uma equipe recebe o vínculo na mesma operação de cadastro. Usuários já vinculados à mesma equipe trabalham sobre os mesmos dados.

Na interface, **fase** é o quadro de uma tática. **Atleta do elenco** é o cadastro da pessoa naquela equipe. **Atleta no quadro** é a representação desenhada em uma fase e pode ter um vínculo com o elenco ou apenas um número e um lado.

## Requisitos funcionais

| ID | Função | Critério de aceitação observável |
|---|---|---|
| RF01 | Cadastrar uma conta | Nome, e-mail, senha e confirmação válidos criam a conta e suas preferências. O aplicativo retorna ao login com o e-mail preenchido. Um e-mail já utilizado é recusado. |
| RF02 | Entrar no sistema | O login aceita e-mail e senha válidos e abre o início com o nome do usuário. Uma tentativa inválida mantém a janela de entrada. |
| RF03 | Sair da conta | **Sair da conta**, no menu lateral, encerra a sessão e volta ao login. A próxima conta recebe a sua própria tela inicial. Fechar esse login encerra o aplicativo. Uma saída cancelada na ficha de atletas mantém a sessão em uso. |
| RF04 | Administrar equipes | O usuário cadastra uma equipe com nome e escudo opcional, consulta seus dados, edita-os e pode excluir a equipe após confirmar o impacto. Pode escolher, substituir ou remover o escudo. |
| RF05 | Consultar equipes acessíveis | A lista contém as equipes vinculadas ao usuário. A busca por nome permite localizar a equipe e **Ver Elenco** abre seus atletas. |
| RF07 | Cadastrar e consultar atletas | A ficha cadastra um atleta no elenco selecionado, com nome, camisa, posição e titularidade. Aceita apelido e data de nascimento. A lista pesquisa por nome ou apelido e filtra por equipe e posição. |
| RF08 | Editar a ficha e o vínculo no elenco | A edição conserva a equipe do vínculo e permite alterar camisa e titularidade. Para um atleta compartilhado, dados pessoais e posições só podem ser editados por um usuário com acesso a todas as equipes a que o atleta está vinculado. |
| RF09 | Registrar posições de vôlei | O cadastro de um novo atleta pede pelo menos uma posição do catálogo. A edição permitida pode alterar ou limpar a seleção. Cada posição aparece uma única vez por atleta. |
| RF10 | Retirar um atleta do elenco ativo | Após confirmação, a ação de exclusão na ficha inativa o vínculo naquela equipe. O atleta sai da lista ativa e das novas opções da prancheta. As fases já salvas preservam seu número, lado e posição. |
| RF11 | Administrar táticas | O usuário cria uma tática na prancheta, pesquisa-a por nome, filtra por equipe e tipo, abre, edita ou apresenta a jogada. **Excluir tática** no cartão identifica a tática e a equipe. Confirmar remove a tática e seus quadros e atualiza a lista. Cancelar conserva a jogada. |
| RF12 | Classificar a tática | Tipo de tática e sistema de jogo podem ser escolhidos em catálogos ou deixados em branco. A equipe é obrigatória para salvar. |
| RF14 | Criar e editar a sequência de fases | A prancheta começa com uma fase. É possível nomear, criar, duplicar, selecionar e excluir fases. Uma cópia possui elementos independentes. A última fase é mantida. |
| RF15 | Reordenar fases | Arrastar um cartão altera sua posição na sequência e recalcula os tempos de início. A fase selecionada continua em edição. A nova ordem é recuperada depois de salvar. |
| RF16 | Configurar duração e rotação | Cada fase possui duração positiva em milissegundos. Fases novas usam a preferência de duração. A rotação aceita P1 a P6 ou pode ficar em branco. |
| RF17 | Visualizar a quadra | A área de desenho preserva as proporções da quadra ao redimensionar. A roda do mouse altera o zoom mantendo as coordenadas relativas dos elementos. |
| RF18 | Representar atletas dos dois lados | É possível adicionar e remover atletas genéricos, selecionar o lado principal ou adversário e editar o número no quadro atual. |
| RF19 | Associar um atleta do elenco ao quadro | A lista oferece os vínculos ativos da equipe da tática. Selecionar um atleta sugere sua camisa. O usuário pode ajustar o número e o lado da representação. O mesmo vínculo só pode ser usado uma vez no quadro. |
| RF20 | Posicionar atletas e bola na fase selecionada | Arrastar o elemento com o botão esquerdo atualiza sua posição. Também é possível informar X e Y de 0 a 100% e clicar em **Aplicar alterações**. Por exemplo, X = 25% e Y = 75% correspondem a 0,25 e 0,75 no banco. **Salvar Tática** conserva essa posição ao reabrir, independentemente do tamanho de exibição da quadra. |
| RF21 | Inserir e remover a bola | Cada fase aceita até uma bola. O comando de adição seleciona a bola existente quando ela já está presente. O usuário pode removê-la só daquela fase. |
| RF22 | Salvar e reabrir a tática | Uma gravação conserva os dados da tática e a ordem, duração, rotação e elementos de cada fase. Ao editar uma tática salva, seu identificador e sua equipe são preservados. |
| RF23 | Apresentar a jogada | Cartões e botões **Anterior** e **Próxima** mostram fases individuais na edição. **Executar** começa pela primeira fase e anima o deslocamento até a seguinte durante a duração da fase de origem. A última permanece visível pelo próprio tempo. **Parar** durante uma transição restaura a fase de origem. |
| RF24 | Configurar preferências pessoais | O usuário seleciona as cores dos atletas principais, dos adversários e da bola, além da duração padrão das fases. Pode restaurar os valores padrão na tela e salvar a configuração. |
| RF25 | Consultar o resumo inicial | O início mostra totais de equipes, atletas ativos distintos, táticas e quadros acessíveis. Exibe até três táticas por identificador decrescente e até três equipes por nome, com atalhos para o trabalho. |

## Regras de negócio

| ID | Regra aplicada |
|---|---|
| RN01 | O e-mail de cadastro é único. O cadastro valida o formato, a confirmação da senha, o mínimo de oito caracteres e o limite de 72 bytes da senha em UTF-8. Cadastro e login removem espaços nas extremidades do e-mail. |
| RN02 | As consultas e gravações de equipes, elencos e táticas usam o vínculo do usuário com a equipe. A exclusão de uma tática verifica esse vínculo no próprio comando de exclusão. |
| RN03 | A criação da equipe grava a equipe e o vínculo do criador na mesma transação. |
| RN04 | Há no máximo um conjunto de preferências por usuário. Ausente a linha, a leitura devolve os padrões. |
| RN05 | O cadastro pessoal e as posições pertencem ao atleta. Camisa, titularidade e situação ativa pertencem ao vínculo com a equipe. Um atleta pode constar em mais de uma equipe. |
| RN06 | Cada equipe reserva uma única camisa de 1 a 99 por vínculo. Um vínculo inativo continua reservando a camisa. |
| RN07 | As posições são selecionadas em um catálogo comum. O par atleta e posição é único. |
| RN08 | Cada tática pertence a uma equipe. Tipo e sistema de jogo são opcionais. A equipe de uma tática salva é fixa no editor. |
| RN09 | Uma equipe pode estar vazia. Para salvar uma tática pela aplicação é necessário ao menos um quadro. |
| RN10 | A ordem dos quadros começa em 1 e é única dentro da tática. A apresentação segue essa ordem. |
| RN11 | A duração de uma fase é positiva. A rotação opcional identifica P1 a P6. O posicionamento dos elementos é definido pelo usuário. |
| RN12 | O número de 1 a 99 é único por lado e por quadro. Pode aparecer no outro lado ou em outra fase. |
| RN13 | O vínculo com o elenco é opcional e pertence à equipe da tática. Um vínculo já salvo continua identificando o atleta mesmo após sua inativação no elenco. |
| RN14 | O número salvo no quadro registra aquela cena. Alterar a camisa do elenco preserva o número já gravado nos quadros. |
| RN15 | Um quadro pode ter zero ou uma bola e uma formação parcial de atletas. |
| RN16 | As coordenadas têm origem no canto superior esquerdo. O centro do elemento fica entre 0 e 1 nos dois eixos, inclusive nas bordas. |
| RN18 | A nova duração padrão afeta fases novas. As cores pertencem à conta que visualiza a jogada. |
| RN19 | Retirar um atleta pela ficha desativa apenas o vínculo na equipe selecionada. O cadastro e a referência nas táticas permanecem. |
| RN20 | Excluir uma tática apaga os seus quadros, os elementos e os registros dependentes. Excluir uma equipe apaga seus vínculos e táticas e remove os cadastros dos atletas que ficaram sem vínculo com qualquer equipe. Atletas que ainda têm vínculo com outra equipe mantêm o cadastro. |
| RN21 | As cores são armazenadas como #RRGGBB. Os nomes obrigatórios são validados antes de salvar. |
| RN22 | O escudo opcional é uma imagem de até 4 MiB e 4096 pixels por lado. O seletor oferece PNG, JPEG, BMP e GIF. Os bytes são gravados em equipe.escudo, do tipo MEDIUMBLOB. |

## Requisitos não funcionais e restrições técnicas

| ID | Implementação | Critério de verificação |
|---|---|---|
| RNF01 | Aplicativo C# com Windows Forms para .NET 10 em Windows | O executável abre o formulário de login e, depois de autenticar, a janela principal LIMK Volleyball. |
| RNF02 | Acesso direto ao MySQL via ADO.NET e MySql.Data | Os dados salvos voltam a ser consultados em outra sessão. A configuração aceita servidor e porta e usa localhost e 3306 por padrão. |
| RNF03 | Credenciais e consultas | A conta da aplicação usa hash bcrypt. SQL recebe os valores da interface por parâmetros e a conexão obtém suas credenciais do ambiente. |
| RNF04 | Posicionamento relativo | Redimensionar ou ampliar a quadra conserva as coordenadas dos atletas e da bola. |
| RNF05 | Operações de dados assíncronas | Consultas e gravações usam operações assíncronas. O descarte das telas solicita cancelamento das operações associadas. |
| RNF06 | Integridade das gravações | Cadastro de conta com preferências, equipe com acesso inicial e salvamento da tática com todos os quadros usam transações. A exclusão individual usa um único DELETE com as ações em cascata do InnoDB. |
| RNF07 | Mensagens compreensíveis | Login, validação de campos, falha de conexão e confirmação de exclusão apresentam mensagens em português. |
| RNF08 | Tratamento da edição local | Na ficha de atletas, sair com alterações pede confirmação de descarte e uma gravação em curso impede a saída. Na prancheta, o usuário grava a edição explicitamente com **Salvar Tática**. |
| RNF09 | Banco de demonstração | O script [limk-completo.sql](../banco/limk-completo.sql) cria 15 tabelas e insere contas, elencos e sequências de exemplo em uma base nova. |

## Rastreabilidade

| Área | Componentes do código | Tabelas principais |
|---|---|---|
| Cadastro e sessão | FormLogin, FormPrincipal, TelaLogin, TelaCadastroUsuario, MenuLateral | usuario, preferencia_usuario |
| Equipes e acesso | TelaEquipes, CardEquipes, DadosEquipe, DadosUsuario | equipe, usuario_equipe |
| Elenco e posições | TelaAtletas, Atleta, DadosAtletas | atleta, equipe_atleta, posicao, atleta_posicao |
| Biblioteca de táticas | TelaTaticas, CardTática, DadosUsuario, DadosPrancheta | tatica, equipe, tipo_tatica, sistema_jogo |
| Editor e apresentação | TelaPranchetaTatica, PranchetaControl, Tatica, QuadroTatica, DadosPrancheta | tatica, quadro, atleta_quadro, bola_quadro |
| Início e preferências | TelaInicial, DadosInicio, TelaConfiguracoes, DadosPreferenciasUsuario | usuario_equipe, equipe_atleta, tatica, quadro, preferencia_usuario |

O esquema físico inclui também a tabela `tatica_favorita`. Ela contém dados de demonstração e participa das exclusões em cascata. As funções disponíveis ao usuário estão descritas nos requisitos acima.
