

set KSP_Test=R:\KSP_1.3.0_dev

@if "%KSP_TEST%"=="" (
	echo KSP_TEST has not been set!
	pause
	exit 1
)

xcopy /s /f /y GameData %KSP_TEST%\GameData\
pause
