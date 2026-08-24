# LIMK

Aplicação acadêmica para criação, edição e apresentação de táticas de vôlei. O sistema permitirá que treinadores organizem equipes e atletas, posicionem jogadores e outros elementos em uma quadra e representem uma jogada como uma sequência de momentos.

> Projeto em desenvolvimento. A estrutura técnica já foi preparada, mas as funcionalidades descritas podem ainda não estar disponíveis.

## Funcionalidades planejadas

- cadastrar equipes e atletas;
- criar, consultar, editar e excluir táticas;
- posicionar e mover jogadores e a bola na quadra;
- criar rodízios e formações;
- representar uma jogada como uma sequência de momentos;
- adicionar setas e textos à prancheta;
- apresentar as táticas durante treinos;
- salvar os dados em um banco MySQL.

## Tecnologias

- C# e .NET 10;
- .NET MAUI para Windows e Android;
- ASP.NET Core Web API;
- MySQL;
- xUnit para testes automatizados;
- Git e GitHub para versionamento e colaboração.

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

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0);
- workload do .NET MAUI;
- MySQL Server, quando a persistência estiver implementada;
- Git.

Para executar no Android, também são necessários:

- Android SDK;
- Microsoft OpenJDK 21;
- Android Emulator e uma imagem de sistema, ou um aparelho Android com depuração USB;
- virtualização do processador habilitada;
- Plataforma do Hipervisor do Windows habilitada no Windows.

Verifique o SDK do .NET instalado:

```powershell
dotnet --version
```

Instale a workload do MAUI:

```powershell
dotnet workload install maui
```

## Preparação do projeto

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

## Como executar

A Solution não é executada como um único programa. A API e o aplicativo MAUI devem ser iniciados separadamente, normalmente em dois terminais.

### 1. Executar a API

No primeiro terminal:

```powershell
dotnet run --project .\Limk.Api
```

O terminal mostrará os endereços e as portas nos quais a API está disponível.

### 2. Executar o aplicativo no Windows

No segundo terminal:

```powershell
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-windows10.0.19041.0
```

Se o destino Windows do projeto for alterado, utilize exatamente o valor iniciado por `net10.0-windows` presente em `Limk.App/Limk.App.csproj`.

### 3. Executar o aplicativo no Android

Liste os emuladores criados:

```powershell
emulator -list-avds
```

Inicie um deles, substituindo o nome quando necessário:

```powershell
emulator -avd Pixel_API_36
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
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-android --device emulator-5554
```

Se houver apenas um emulador em execução, também é possível usar:

```powershell
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-android -p:AdbTarget=-e
```

## Configuração manual do Android no Windows

Esta seção só é necessária quando o Android SDK e o JDK foram instalados em caminhos personalizados.

Exemplo de caminhos:

```text
C:\work\android-sdk
C:\work\jdk
```

Configure as variáveis permanentes do usuário no PowerShell:

```powershell
[Environment]::SetEnvironmentVariable("ANDROID_HOME", "C:\work\android-sdk", "User")
[Environment]::SetEnvironmentVariable("JAVA_HOME", "C:\work\jdk", "User")
[Environment]::SetEnvironmentVariable("AndroidSdkDirectory", "C:\work\android-sdk", "User")
[Environment]::SetEnvironmentVariable("JavaSdkDirectory", "C:\work\jdk", "User")
```

Feche e abra novamente o terminal e o editor após definir essas variáveis.

Para configurar também o terminal atual:

```powershell
$env:ANDROID_HOME = "C:\work\android-sdk"
$env:JAVA_HOME = "C:\work\jdk"
$env:AndroidSdkDirectory = "C:\work\android-sdk"
$env:JavaSdkDirectory = "C:\work\jdk"
```

Verifique a aceleração do emulador:

```powershell
& "$env:AndroidSdkDirectory\emulator\emulator.exe" -accel-check
```

O resultado esperado no Windows é `WHPX is installed and usable`.

## Comunicação do Android com a API

Dentro do emulador, `localhost` representa o próprio Android virtual, não o computador que está executando a API. Para acessar uma API executada no computador, utilize o endereço especial:

```text
http://10.0.2.2:PORTA_DA_API
```

Substitua `PORTA_DA_API` pela porta apresentada ao executar o `Limk.Api`. A configuração de HTTP, HTTPS e certificados deve seguir o ambiente definido pelo projeto.

## Banco de dados e credenciais

As credenciais do MySQL devem ficar somente na API. Elas não devem ser colocadas no `Limk.App` nem enviadas ao Git.

Para preparar o armazenamento local de segredos da API:

```powershell
dotnet user-secrets init --project .\Limk.Api
```

Quando a conexão com o banco estiver implementada, registre a connection string usando o nome definido pela API. Exemplo:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=limk;User=limk_app;Password=SUA_SENHA;" --project .\Limk.Api
```

Nunca publique a senha real no README, no `appsettings.json` ou em qualquer arquivo versionado.

## Testes

Execute todos os testes da solução:

```powershell
dotnet test Limk.sln
```

Ou execute somente o projeto de testes:

```powershell
dotnet test .\Limk.Tests
```

## Problemas comuns

### Android SDK não encontrado

Confira as variáveis e a existência dos arquivos:

```powershell
$env:AndroidSdkDirectory
Test-Path "$env:AndroidSdkDirectory\platform-tools\adb.exe"
```

Também é possível informar os caminhos diretamente durante a compilação:

```powershell
dotnet build .\Limk.App\Limk.App.csproj `
  -f net10.0-android `
  -p:AndroidSdkDirectory="C:\work\android-sdk" `
  -p:JavaSdkDirectory="C:\work\jdk"
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
