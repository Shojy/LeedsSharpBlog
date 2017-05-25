@ECHO OFF
SetLocal
set ASPNETCORE_ENVIRONMENT=Production

pushd .\LeedsSharpBlog
dotnet watch run
cd
exit /b