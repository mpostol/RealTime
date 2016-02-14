@echo off
signcode -cn "CAS  CAS" RTLIb.dll
signcode -cn "CAS  CAS" "..\..\obj\release\RTLIb.dll

if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd