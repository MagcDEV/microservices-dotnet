### CoponDB docker
```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Akira2990" -p 1433:1433 -v mangodb:/var/opt/mssql -d --rm --name mangodb mcr.microsoft.com/mssql/server:2022-latest
```
#### IN a Linux machine use postgressql
```basch
docker volume create mangodb_data
docker run --name mangodb_container -e POSTGRES_PASSWORD=mysecretpassword -d -p 5432:5432 -v mangodb_data:/var/lib/postgresql/data postgres
```
