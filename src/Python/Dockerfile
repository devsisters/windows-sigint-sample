# 공식 이미지는 LTSC만 지원됩니다. (예: python:3.8-windowsservercore-1809)
# SAC 버전은 rkttu/python-nanoserver Docker Hub 리포지터리를 참고해주세요.

FROM rkttu/python-nanoserver:3.8.6_2004
WORKDIR /app

COPY main.py .
ENTRYPOINT ["python", "main.py"]
