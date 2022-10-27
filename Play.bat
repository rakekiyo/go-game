@echo off

cd .\GoGame
dotnet build

if %errorlevel% neq 0 (
  exit /b %errorlevel%
)

cd ..
copy .\GoGame\bin\Debug\net6.0\GoGame.dll .\Play\References\GoGame.dll

cd .\Play
dotnet run