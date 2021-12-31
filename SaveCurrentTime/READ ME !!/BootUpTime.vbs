Set WshShell = CreateObject("WScript.Shell" ) 
WshShell.Run """E:\DOCUMENTI\Workspace Visual Studio\Csharp-Projects\SaveCurrentTime\bin\Release\netcoreapp3.1\SaveCurrentTime.exe""", 0 'Must quote command if it has spaces; must escape quotes
Set WshShell = Nothing