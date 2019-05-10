FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ApiGateWay/ApiGateWay.csproj", "ApiGateWay/"]
RUN dotnet restore "ApiGateWay/ApiGateWay.csproj"
COPY . .
WORKDIR "/src/ApiGateWay"
RUN dotnet build "ApiGateWay.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApiGateWay.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiGateWay.dll"]