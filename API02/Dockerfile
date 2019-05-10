FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["API02/API02.csproj", "API02/"]
RUN dotnet restore "API02/API02.csproj"
COPY . .
WORKDIR "/src/API02"
RUN dotnet build "API02.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API02.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API02.dll"]