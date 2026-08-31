# LIMK VolleyBall

Aplicação acadêmica para criação, edição e apresentação de táticas de vôlei de quadra 6x6. O produto será uma prancheta digital para treinadores, com suporte a Windows e Android, interação por mouse ou toque e persistência direta em MySQL.

> Projeto em desenvolvimento.

## Objetivo do MVP

O MVP deverá permitir que um treinador:

- cadastre equipes e atletas;
- posicione e mova seis jogadores e uma bola na quadra;
- crie e personalize um rodízio 5x1, com R1 a R6 e formações de saque e recepção;
- crie uma tática como uma sequência ordenada de quadros;
- duplique, exclua, selecione e reordene quadros;
- adicione, edite, mova e exclua setas e textos explicativos;
- apresente a tática manualmente no Windows e no Android;
- salve e recupere equipes, atletas, rodízios e táticas diretamente no MySQL;
- mantenha o trabalho atual em memória e receba um aviso quando o banco estiver indisponível ou houver alterações não salvas.

A prancheta é o núcleo do sistema. Equipes, rodízios, táticas e persistência serão construídos a partir dela.

## Arquitetura

```text
Aplicativo .NET MAUI
Windows + Android
        │
        │ conexão direta
        ▼
MySQL local
```

- `Limk.App`: interface MAUI, prancheta, interações por mouse e toque, rodízios, táticas e apresentação.
- `Limk.Core`: modelos e regras de negócio compartilhadas.
- `Limk.Tests`: testes automatizados e roteiros de verificação.
- camada de persistência no aplicativo: abertura de conexões, validação de dados, consultas e gravações no MySQL.
- MySQL: equipes, atletas, rodízios, formações, táticas, quadros e elementos dos quadros.

O SQL e o gerenciamento de conexões deverão ficar separados das páginas e ViewModels.


## Plataformas e tecnologias

| Tecnologia | Versão adotada | Uso |
| --- | --- | --- |
| C# | fornecida pelo .NET 10 | linguagem principal |
| .NET SDK | 10.0.x | compilação da solução |
| .NET MAUI | 10.0 | aplicativo para Windows e Android |
| Android SDK Platform | API 36 | compilação e execução no Android |
| Microsoft OpenJDK | 21 | ferramentas de compilação do Android |
| MySQL Server | 8.x | persistência dos dados |
| xUnit | 2.9.3 | testes automatizados |
| Git | 2.x ou superior | versionamento e colaboração |

## Estrutura da solução

A estrutura-alvo é:

```text
Limk.slnx
├── Limk.App     Aplicativo .NET MAUI
├── Limk.Core    Modelos e regras compartilhadas
├── Limk.Tests   Testes
└── MySQL        Scripts e documentação do banco
```

## Preparação do ambiente

### Requisitos

Para compilar e testar a solução:

- SDK do .NET 10;
- workload do .NET MAUI;
- Git;
- Visual Studio com suporte a .NET 10 e MAUI ou outra configuração compatível.

Para executar no Android:

- Android SDK e Android Emulator, ou um aparelho com depuração USB;
- Microsoft OpenJDK 21;
- virtualização do processador e Plataforma do Hipervisor do Windows habilitadas, quando for usado o emulador.

Para a prova técnica e as funcionalidades de persistência, instale também o MySQL Server 8.x.

Confira as ferramentas instaladas:

```powershell
dotnet --version
dotnet workload list
java -version
mysql --version
git --version
```

Instale a workload do MAUI, se necessário:

```powershell
dotnet workload install maui
```

No Visual Studio Installer, a carga de trabalho correspondente é **Desenvolvimento de interface do usuário de aplicativos multiplataforma .NET**. Mantenha selecionados os componentes de Android SDK, Android Emulator e Microsoft OpenJDK.

### Clonar e restaurar

```powershell
git clone <URL-DO-REPOSITORIO>
cd limk
dotnet workload restore
dotnet restore .\Limk.slnx
```

### Compilar

```powershell
dotnet build .\Limk.slnx
```

No Visual Studio, abra `Limk.slnx` e use **Compilar > Compilar Solução** (`Ctrl + Shift + B`).

## Executar o aplicativo

### Windows

Pela linha de comando:

```powershell
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-windows10.0.19041.0
```

No Visual Studio, defina `Limk.App` como projeto de inicialização, selecione **Windows Machine** e pressione `F5`.

### Android

Crie e inicie um dispositivo no Android Device Manager ou conecte um aparelho físico. Em seguida, confirme que ele foi reconhecido:

```powershell
adb devices
```

Execute informando o identificador listado:

```powershell
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-android --device emulator-5554
```

Quando houver somente um emulador ativo, também é possível usar:

```powershell
dotnet run --project .\Limk.App\Limk.App.csproj -f net10.0-android -p:AdbTarget=-e
```

No Visual Studio, inicie o emulador pelo Android Device Manager, selecione-o como destino de `Limk.App` e pressione `F5`.

## Testes

Execute os testes automatizados:

```powershell
dotnet test .\Limk.Tests\Limk.Tests.csproj
```
