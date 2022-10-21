@echo off

cd .\GoGame
dotnet build

cd ..
copy .\GoGame\bin\Debug\net6.0\GoGame.dll .\Play\References\GoGame.dll

cd .\Play
dotnet run