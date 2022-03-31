FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder  
 
WORKDIR /source
 
COPY *.csproj . 
RUN dotnet restore -r linux-arm

COPY . .
RUN dotnet publish -c release -o /app --self-contained -r linux-arm
COPY appsettings.json /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-arm32v7

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg && \
    wget -qO - http://archive.raspberrypi.org/debian/raspberrypi.gpg.key | apt-key add - && \
    echo "deb http://archive.raspberrypi.org/debian/ bullseye main" | tee -a /etc/apt/sources.list.d/raspi.list && \
    wget -O - http://archive.raspberrypi.org/debian/raspberrypi.gpg.key | apt-key add - && \
    apt-get update && \
    apt-get install -y libraspberrypi-bin

WORKDIR /app 
COPY --from=builder /app .
ENV PATH="/usr/share/dotnet:$PATH"

ENTRYPOINT ["dotnet", "./GlowMeterDataMonitor.dll"]  
