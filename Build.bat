@SET BUILD_DIR=%~dp0Build\
@SET APP_NAME=HORNS Demo

@SET HORNS_CSPROJ=%HORNS_LIB_SRC_DIR%/HORNS/HORNS.csproj
@SET HORNS_LIB_DIR=%BUILD_DIR%HORNS\

@SET LINUX_BUILD_DIR=%BUILD_DIR%Linux
@SET WIN64_BUILD_DIR=%BUILD_DIR%Win64

@SET DEMO_PROJ_DIR=%~dp0
@SET DEMO_LIB_DIR=%DEMO_PROJ_DIR%Assets\Libraries\
@SET INSTALLER_DIR=%DEMO_PROJ_DIR%/Installer

@CALL :test_program Unity, Unity || @EXIT

@CALL :test_program iscc, "Inno Setup" || @EXIT

@IF EXIST %HORNS_CSPROJ% (
	@CALL :test_program dotnet, "dotnet runtime" || (@ECHO ERROR && @PAUSE && @EXIT)

	@ECHO Found HORNS library project file: "%HORNS_CSPROJ%"
	@ECHO Building library from source...
	dotnet publish "%HORNS_CSPROJ%" -c Release -o "%HORNS_LIB_DIR%"
	
	@COPY "%HORNS_LIB_DIR%*.dll" "%DEMO_LIB_DIR%" /Y
	@COPY "%HORNS_LIB_DIR%*.pdb" "%DEMO_LIB_DIR%" /Y
) ELSE (
	@ECHO Project file not found: "%HORNS_CSPROJ%"
	@ECHO Make sure that the environment variable HORNS_LIB_SRC_DIR is set
	@ECHO and points to a location containing HORNS source files
)

@ECHO Building for Windows 64...
@Unity "%WIN64_BUILD_DIR%\%APP_NAME%.exe" -quit -batchmode -projectPath "%DEMO_PROJ_DIR%" -executeMethod Build.BuildWin64 || (@ECHO ERROR && @PAUSE && @EXIT)
@ECHO Done
@ECHO Building for Linux...
@Unity "%LINUX_BUILD_DIR%\%APP_NAME%.x86_64" -quit -batchmode -projectPath "%DEMO_PROJ_DIR%" -executeMethod Build.BuildLinux || (@ECHO ERROR && @PAUSE && @EXIT)
@ECHO Done

@iscc /DInstallerDir="%INSTALLER_DIR%" /DOutputDir="%BUILD_DIR%" /DExeDir="%WIN64_BUILD_DIR%" "%INSTALLER_DIR%\Windows.iss"

@PAUSE
@EXIT

:test_program
@where /q %~1
@IF ERRORLEVEL 1 (
	@ECHO Could not find %~1 in PATH. Please make sure the %~2 is installed and available
	@PAUSE
	@EXIT /B 1
)
@EXIT /B 0