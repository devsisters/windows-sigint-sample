FROM mcr.microsoft.com/dotnet/framework/runtime:3.5-windowsservercore-1909
ENV WAIT_SECONDS 10

USER ContainerAdministrator

WORKDIR /App
ADD ./bin/Release/* ./

USER ContainerUser
CMD ["SignalHandlerTestManaged.exe"]
