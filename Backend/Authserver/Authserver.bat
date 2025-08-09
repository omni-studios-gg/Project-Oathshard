@echo off
cd /d "%~dp0"

echo Installing dependencies...
call npm install

echo Starting server...
call .\src\redist\node_bin\node.exe .\src\server.js

pause
