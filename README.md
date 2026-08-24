# LIMK

Aplicação acadêmica para criação, edição e apresentação de táticas de vôlei. O sistema permitirá que treinadores organizem equipes e atletas, posicionem jogadores e outros elementos em uma quadra e representem uma jogada como uma sequência de momentos.



> Projeto em desenvolvimento.



* cadastrar equipes e atletas;
* criar, consultar, editar e excluir táticas;
* posicionar e mover jogadores e a bola na quadra;
* criar rodízios e formações;
* representar uma jogada como uma sequência de momentos;
* adicionar setas e textos à prancheta;
* apresentar as táticas durante treinos;
* salvar os dados em um banco MySQL.

## Ferramentas e versões utilizadas

|Ferramenta|Versão adotada|Utilização|
|-|-|-|
|.NET SDK|10.0.x|Compilação de toda a solução|
|C#|Versão fornecida pelo .NET 10|Linguagem principal|
|.NET MAUI|10.0|Aplicativo para Windows e Android|
|ASP.NET Core|10.0|Web API|
|Android SDK Platform|API 36|Compilação e execução no Android|
|Microsoft OpenJDK|21|Ferramentas de compilação do Android|
|MySQL Server|8.x|Persistência dos dados|
|xUnit|Versão registrada em `Limk.Tests.csproj`|Testes automatizados|
|Git|2.x ou superior|Versionamento e colaboração|
|Visual Studio|Visual Studio 2022 atualizado ou versão posterior compatível com .NET 10|Desenvolvimento pela interface gráfica|

Para conferir as versões instaladas no computador:

```powershell
dotnet --version
dotnet workload list
java -version
mysql --version
git --version
```

## Estrutura da solução

```text
Limk.sln
├── Limk.App    Aplicativo MAUI para Windows e Android
├── Limk.Api    API ASP.NET Core
├── Limk.Core   Entidades e regras de negócio compartilhadas
└── Limk.Tests  Testes automatizados
```

As referências principais seguem esta direção:

```text
Limk.App   ──→ Limk.Core
Limk.Api   ──→ Limk.Core
Limk.Tests ──→ Limk.Core
```

O `Limk.Core` é uma biblioteca e, por isso, não é executado diretamente.

## Requisitos

Para compilar toda a solução, instale:

* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0);
* workload do .NET MAUI;
* MySQL Server, quando a persistência estiver implementada;
* Git.

Para executar no Android, também são necessários:

* Android SDK;
* Microsoft OpenJDK 21;
* Android Emulator e uma imagem de sistema, ou um aparelho Android com depuração USB;
* virtualização do processador habilitada;
* Plataforma do Hipervisor do Windows habilitada no Windows.

Verifique o SDK do .NET instalado:

```powershell
dotnet --version
```

Instale a workload do MAUI:

```powershell
dotnet workload install maui
```

Quem utilizar o Visual Studio pode instalar essa workload e os componentes Android pelo Visual Studio Installer, sem executar esse comando manualmente.

## Usando o Visual Studio

O Visual Studio pode ser utilizado para preparar, compilar e executar o projeto sem digitar comandos no terminal.

### Instalar os componentes necessários

1. Abra o **Visual Studio Installer**.
2. Encontre a instalação do Visual Studio e selecione **Modificar**.
3. Marque a carga de trabalho **Desenvolvimento de interface do usuário de aplicativos multiplataforma .NET**.
4. Marque também **Desenvolvimento ASP.NET e para a Web**.
5. Mantenha selecionados os componentes opcionais de Android, incluindo Android SDK, Android Emulator e Microsoft OpenJDK.
6. Selecione **Modificar/Instalar** e aguarde a conclusão.

O MySQL Server precisa ser instalado separadamente quando a persistência for utilizada.

### Clonar e abrir o projeto

1. Abra o Visual Studio.
2. Na tela inicial, selecione **Clonar um repositório**. A mesma opção também está disponível em **Git > Clonar Repositório**.
3. Cole a URL do repositório, escolha a pasta de destino e selecione **Clonar**.
4. Abra o arquivo `Limk.sln` caso a Solution não seja carregada automaticamente.
5. Aguarde o Visual Studio restaurar os pacotes e carregar todos os projetos.

Se o repositório já estiver no computador, selecione **Abrir um projeto ou uma solução** e escolha `Limk.sln`.

### Compilar a solução

No menu superior, selecione:

```text
Compilar > Compilar Solução
```

O atalho correspondente é `Ctrl + Shift + B`. O resultado aparecerá na janela **Saída** e na **Lista de Erros**.

### Executar somente a API

1. No **Gerenciador de Soluções**, clique com o botão direito em `Limk.Api`.
2. Selecione **Definir como Projeto de Inicialização**.
3. Pressione `F5` para executar com depuração ou `Ctrl + F5` para executar sem depuração.
4. Observe no navegador ou na janela de saída os endereços e as portas utilizados pela API.

### Executar o App no Windows

1. Clique com o botão direito em `Limk.App` e selecione **Definir como Projeto de Inicialização**.
2. Na barra superior, escolha **Windows Machine** como destino.
3. Pressione `F5` ou selecione o botão de execução.

### Executar o App no Android

1. Abra **Ferramentas > Android > Android Device Manager**.
2. Selecione **Novo** e crie um dispositivo virtual com uma imagem compatível, preferencialmente API 36.
3. Inicie o emulador criado.
4. Defina `Limk.App` como projeto de inicialização.
5. Na lista de destinos da barra superior, escolha o emulador Android.
6. Pressione `F5` para compilar, instalar e executar o aplicativo.

Também é possível conectar um aparelho físico com o modo de desenvolvedor e a depuração USB habilitados. Nesse caso, selecione o aparelho na lista de destinos.

### Executar a API e o App juntos

A API deve permanecer em execução enquanto o App faz requisições. Existem duas opções:

**Opção simples:**

1. Defina `Limk.Api` como projeto de inicialização e execute com `Ctrl + F5`.
2. Sem fechar a API, defina `Limk.App` como projeto de inicialização.
3. Escolha Windows ou Android e execute o App com `F5`.

**Projetos de inicialização múltiplos:**

1. Clique com o botão direito na Solution e abra **Configurar Projetos de Inicialização**. Dependendo da versão, essa opção fica em **Propriedades > Projeto de Inicialização**.
2. Selecione **Vários projetos de inicialização**.
3. Configure a ação **Iniciar** para `Limk.Api` e `Limk.App`.
4. Confirme, escolha o destino do MAUI e pressione `F5`.

### Executar os testes

1. Abra **Teste > Gerenciador de Testes**.
2. Aguarde a descoberta dos testes do `Limk.Tests`.
3. Selecione **Executar Todos os Testes**.

### Configurar os segredos da API

1. Clique com o botão direito em `Limk.Api`.
2. Selecione **Gerenciar Segredos do Usuário**.
3. Registre a connection string usando o nome definido pela API, sem colocar a senha em arquivos versionados.

Exemplo de estrutura do `secrets.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=limk;User=limk\_app;Password=SUA\_SENHA;"
  }
}
```

## Preparação pela linha de comando

Clone o repositório e entre na pasta criada:

```powershell
git clone <URL-DO-REPOSITORIO>
cd limk
```

Restaure as workloads e os pacotes:

```powershell
dotnet workload restore
dotnet restore Limk.sln
```

Compile a solução:

```powershell
dotnet build Limk.sln
```

## Como executar pela linha de comando

A Solution não é executada como um único programa. A API e o aplicativo MAUI devem ser iniciados separadamente, normalmente em dois terminais.

### 1\. Executar a API

No primeiro terminal:

```powershell
dotnet run --project .\\Limk.Api
```

O terminal mostrará os endereços e as portas nos quais a API está disponível.

### 2\. Executar o aplicativo no Windows

No segundo terminal:

```powershell
dotnet run --project .\\Limk.App\\Limk.App.csproj -f net10.0-windows10.0.19041.0
```

Se o destino Windows do projeto for alterado, utilize exatamente o valor iniciado por `net10.0-windows` presente em `Limk.App/Limk.App.csproj`.

### 3\. Executar o aplicativo no Android

Liste os emuladores criados:

```powershell
emulator -list-avds
```

Inicie um deles, substituindo o nome quando necessário:

```powershell
emulator -avd Pixel\_API\_36
```

Depois que o Android terminar de iniciar, liste os dispositivos disponíveis:

```powershell
adb devices
```

O resultado deverá mostrar um identificador semelhante a este:

```text
emulator-5554    device
```

Execute o projeto informando o identificador exibido:

```powershell
dotnet run --project .\\Limk.App\\Limk.App.csproj -f net10.0-android --device emulator-5554
```

Se houver apenas um emulador em execução, também é possível usar:

```powershell
dotnet run --project .\\Limk.App\\Limk.App.csproj -f net10.0-android -p:AdbTarget=-e
```

## Configuração manual do Android no Windows

Esta seção só é necessária quando o Android SDK e o JDK foram instalados em caminhos personalizados.

Exemplo de caminhos:

```text
C:\\work\\android-sdk
C:\\work\\jdk
```

Configure as variáveis permanentes do usuário no PowerShell:

```powershell
\[Environment]::SetEnvironmentVariable("ANDROID\_HOME", "C:\\work\\android-sdk", "User")
\[Environment]::SetEnvironmentVariable("JAVA\_HOME", "C:\\work\\jdk", "User")
\[Environment]::SetEnvironmentVariable("AndroidSdkDirectory", "C:\\work\\android-sdk", "User")
\[Environment]::SetEnvironmentVariable("JavaSdkDirectory", "C:\\work\\jdk", "User")
```

Feche e abra novamente o terminal e o editor após definir essas variáveis.

Para configurar também o terminal atual:

```powershell
$env:ANDROID\_HOME = "C:\\work\\android-sdk"
$env:JAVA\_HOME = "C:\\work\\jdk"
$env:AndroidSdkDirectory = "C:\\work\\android-sdk"
$env:JavaSdkDirectory = "C:\\work\\jdk"
```

Verifique a aceleração do emulador:

```powershell
\& "$env:AndroidSdkDirectory\\emulator\\emulator.exe" -accel-check
```

O resultado esperado no Windows é `WHPX is installed and usable`.

## Comunicação do Android com a API

Dentro do emulador, `localhost` representa o próprio Android virtual, não o computador que está executando a API. Para acessar uma API executada no computador, utilize o endereço especial:

```text
http://10.0.2.2:PORTA\_DA\_API
```

Substitua `PORTA\_DA\_API` pela porta apresentada ao executar o `Limk.Api`. A configuração de HTTP, HTTPS e certificados deve seguir o ambiente definido pelo projeto.

## Banco de dados e credenciais

As credenciais do MySQL devem ficar somente na API. Elas não devem ser colocadas no `Limk.App` nem enviadas ao Git.

Para preparar o armazenamento local de segredos da API:

```powershell
dotnet user-secrets init --project .\\Limk.Api
```

Quando a conexão com o banco estiver implementada, registre a connection string usando o nome definido pela API. Exemplo:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=limk;User=limk\_app;Password=SUA\_SENHA;" --project .\\Limk.Api
```

Nunca publique a senha real no README, no `appsettings.json` ou em qualquer arquivo versionado.

## Testes

Execute todos os testes da solução:

```powershell
dotnet test Limk.sln
```

Ou execute somente o projeto de testes:

```powershell
dotnet test .\\Limk.Tests
```

## Problemas comuns

### Android SDK não encontrado

Confira as variáveis e a existência dos arquivos:

```powershell
$env:AndroidSdkDirectory
Test-Path "$env:AndroidSdkDirectory\\platform-tools\\adb.exe"
```

Também é possível informar os caminhos diretamente durante a compilação:

```powershell
dotnet build .\\Limk.App\\Limk.App.csproj `
  -f net10.0-android `
  -p:AndroidSdkDirectory="C:\\work\\android-sdk" `
  -p:JavaSdkDirectory="C:\\work\\jdk"
```

### Nenhum dispositivo disponível

Confirme que o emulador terminou de iniciar ou que o aparelho físico está conectado:

```powershell
adb devices
```

Se necessário, reinicie o servidor ADB:

```powershell
adb kill-server
adb start-server
adb devices
```

### Aceleração da CPU indisponível

Habilite a virtualização Intel VT-x ou AMD-V/SVM na BIOS, ative a **Plataforma do Hipervisor do Windows** nos recursos do Windows e reinicie o computador.

## Status

O LIMK é um projeto acadêmico em desenvolvimento. Funcionalidades, comandos e configurações podem mudar durante as próximas sprints.

