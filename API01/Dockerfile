FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["API01/API01.csproj", "API01/"]
RUN dotnet restore "API01/API01.csproj"
COPY . .
WORKDIR "/src/API01"
RUN dotnet build "API01.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API01.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API01.dll"]