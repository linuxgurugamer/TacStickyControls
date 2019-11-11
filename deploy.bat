
@echo off

set H=%KSPDIR%
set GAMEDIR=TacStickyControls

echo %H%

copy /Y "%1%2" "GameData\%GAMEDIR%\Plugins"
rem copy /Y %GAMEDIR%.version GameData\%GAMEDIR%

mkdir "%H%\GameData\%GAMEDIR%"
xcopy /y /s GameData\%GAMEDIR% "%H%\GameData\%GAMEDIR%"
