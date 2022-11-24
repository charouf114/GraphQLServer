When Updating the DB
Run in Package manager Console Please 

dotnet ef migrations add NameOfTheMigration 
dotnet ef database update

Regex For The Name of the Query (To be improved)
(?<=((m|M)utation | (q|Q)uery))(.*)(?=\{.*)
