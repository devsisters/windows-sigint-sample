# Python 3 기반 SIGINT 처리 예제

이 샘플 프로젝트는 Python 3 기반 SIGINT를 처리하는 방법을 설명하는 예제 애플리케이션입니다.

## 프로젝트 빌드에 필요한 구성 요소

- Chocolatey Package Manager 및 추가 패키지
  - `windows-kill` 패키지
  - `vcredist2017` 패키지
- Docker Desktop for Windows

## 프로젝트 테스트 방법

1. Chocolatey Package Manager를 설치한 후 관리자 권한으로 명령 프롬프트를 실행한 후 아래 명령어를 실행합니다.

```powershell
choco install vcredist2017 windows-kill -y
```

1. .\RunTest.ps1 PowerShell 스크립트를 실행하여 Docker 이미지 빌드와 테스트를 실행합니다.

1. `windows-kill.exe` 유틸리티를 사용하여 10초 간의 대기 후 자동으로 종료되는 모습이 나타나는지 확인합니다.

## 제한 사항

Python 공식 이미지의 경우 윈도 이미지를 모든 SAC 채널에 대해 제공하지는 않고 있습니다.

상황에 따라 직접 커스텀 파이썬 이미지를 빌드해야 할 수 있습니다.

[rkttu/python-nanoserver](https://hub.docker.com/r/rkttu/python-nanoserver)와 같은 커뮤니티 이미지를 참고하여 작업할 수 있습니다.
