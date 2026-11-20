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

O login informa o ID, o nome e o e-mail do usuário ao formulário principal. A interface `ITelaUsuario` permite que o formulário entregue essa identidade a cada tela antes de carregá-la, tanto pelo menu quanto pelos atalhos.

O acesso aos dados segue os vínculos da tabela `usuario_equipe`:

- o usuário consulta as equipes às quais está vinculado;
- os atletas aparecem pelos vínculos ativos com essas equipes, preservando o número da camisa de cada equipe;
- as táticas e os quadros seguem a equipe da tática;
- os totais e os cartões do início usam a mesma regra;
- uma conta sem equipes vinculadas apresenta listas vazias e totais iguais a zero.

Dois usuários vinculados à mesma equipe compartilham acesso aos dados dela. A troca de usuário descarta a tela anterior e suas informações. Os formulários de cadastro e os botões de abrir/apresentar táticas salvas continuam como interface. A prancheta edita os elementos em memória.
