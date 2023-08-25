### CoponDB docker
```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Akira2990" -p 1433:1433 -v mangodb:/var/opt/mssql -d --rm --name mangodb mcr.microsoft.com/mssql/server:2022-latest
```