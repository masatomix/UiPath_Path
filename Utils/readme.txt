> nuget.exe spec Utils.csproj
> nuget.exe pack Utils.csproj -Prop Configuration=Release
> dotnet nuget push kino.UiPath.Utils.Activities.0.1.23.nupkg -k xxxxx -s https://api.nuget.org/v3/index.json