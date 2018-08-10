FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 62667
EXPOSE 44355

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Blog/Blog.csproj Blog/
RUN dotnet restore Blog/Blog.csproj
COPY . .
WORKDIR /src/Blog
RUN dotnet build Blog.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Blog.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.dll"]
