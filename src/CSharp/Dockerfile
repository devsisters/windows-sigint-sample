FROM mcr.microsoft.com/windows/servercore:1909
ENV WAIT_SECONDS 10

USER ContainerAdministrator

WORKDIR /App
ADD ./bin/Release/* ./

USER ContainerUser
CMD ["SignalHandlerTestManaged.exe"]
