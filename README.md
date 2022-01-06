<p align="center">
  <img src="https://i.ibb.co/j5m21kJ/Screenshot-2021-12-11-at-17-27-51.png" alt="AirSnitch" />
</p>
<p align="center">
  <a href="https://github.com/bitwarden/server/actions/workflows/build.yml?query=branch:master" target="_blank">
    <img src="https://github.com/bitwarden/server/actions/workflows/build.yml/badge.svg?branch=master" alt="Github Workflow build on master" />
  </a>
  <a href="https://gitter.im/bitwarden/Lobby" target="_blank">
    <img src="https://badges.gitter.im/bitwarden/Lobby.svg" alt="gitter chat" />
  </a>
</p>

-------------------

The AirSnitch Server project contains the APIs, database, and other core infrastructure items needed for the "backend" of all AirSnitch client applications.

The server project is written in C# using .NET Core with ASP.NET Core. DataBase - mongoDb. The codebase can be developed, built, run, and deployed cross-platform on Windows, macOS, and Linux distributions.

## Run

```
cd src
docker-compose build --no-cache
MongoDbConnectionString=mongodb://root:example@mongo:27017/ MongoDbName=AirQ IsSeedData=true docker-compose up -V
```
visit http://localhost:5000/swagger/index.html

### Requirements

- [Docker](https://www.docker.com/products/docker-desktop)

*These dependencies are free to use.*

## Build

```
cd src
dotnet restore
dotnet build
dotnet run
```

visit http://localhost:5000/swagger/index.html
### Requirements

- [.NET 5.0 SDK](https://dotnet.microsoft.com/download)
- [MonogDB](https://www.mongodb.com/try/download/community)

*These dependencies are free to use.*

## Contribute

Code contributions are welcome! Visual Studio or VS Code is highly recommended if you are working on this project. Please commit any pull requests against the `main` branch. Please see [`CONTRIBUTING.md`](CONTRIBUTING.md) for more info (and feel free to contribute to that guide as well).
