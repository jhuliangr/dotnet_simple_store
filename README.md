# Simple Store

## Setting the connection string in the secret manager
```powershell
$password = "Password-Goes-Here"
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "server=localhost;user=jhulian;password=$password;database=store"
``` 
## Running the migrations
```powershell
dotnet ef database update
```