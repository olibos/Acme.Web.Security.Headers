@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

%nuget% restore
"%MsBuildExe%" /t:pack /p:Configuration=Release
%nuget% pack Acme.Web.Security.Headers\Acme.Web.Security.Headers.csproj -symbols