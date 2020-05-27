FROM mcr.microsoft.com/windows/nanoserver:1909
ENV WAIT_SECONDS 10

USER ContainerAdministrator

WORKDIR /App
ADD ./x64/Release/* ./

USER ContainerUser
CMD ["SignalHandlerTest.exe"]
