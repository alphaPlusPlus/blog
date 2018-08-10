FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50036
EXPOSE 44385

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Blog.API/Blog.API.csproj Blog.API/
RUN dotnet restore Blog.API/Blog.API.csproj
COPY . .
WORKDIR /src/Blog.API
RUN dotnet build Blog.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Blog.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.API.dll"]
