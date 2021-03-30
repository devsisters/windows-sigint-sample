# .NET Core 3.1 기반 SIGINT 처리 예제

이 샘플 프로젝트는 .NET Core 3.1 기반 SIGINT를 처리하는 방법을 설명하는 예제 애플리케이션입니다.

## 프로젝트 빌드에 필요한 구성 요소

- Chocolatey Package Manager 및 추가 패키지
  - `windows-kill` 패키지
  - `vcredist2017` 패키지
- Visual Studio 2017
  - Desktop .NET 워크로드
- Docker Desktop for Windows

## 프로젝트 빌드 방법

1. Chocolatey Package Manager를 설치한 후 관리자 권한으로 명령 프롬프트를 실행한 후 아래 명령어를 실행합니다.

```powershell
choco install vcredist2017 windows-kill -y
```

1. .NET Core 3.1 SDK가 설치되어있는지 다음의 명령어로 먼저 확인합니다.

```powershell
dotnet.exe --list-sdks
```

1. 이 프로젝트가 있는 위치로 디렉터리를 이동합니다.

1. 다음과 같이 명령어를 실행하여 프로젝트를 빌드합니다.

```powershell
dotnet.exe build
```

## 프로젝트 테스트 방법

1. .\RunTest.ps1 PowerShell 스크립트를 실행하여 Docker 이미지 빌드와 테스트를 실행합니다.

1. `windows-kill.exe` 유틸리티를 사용하여 10초 간의 대기 후 자동으로 종료되는 모습이 나타나는지 확인합니다.
