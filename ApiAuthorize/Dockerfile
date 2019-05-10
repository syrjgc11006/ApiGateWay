FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ApiAuthorize/ApiAuthorize.csproj", "ApiAuthorize/"]
RUN dotnet restore "ApiAuthorize/ApiAuthorize.csproj"
COPY . .
WORKDIR "/src/ApiAuthorize"
RUN dotnet build "ApiAuthorize.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApiAuthorize.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiAuthorize.dll"]