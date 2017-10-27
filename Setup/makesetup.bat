REM c:\windows\syswow64\iexpress /N Setup.sed

del setup.7z
"c:\program files\7-zip\7z.exe" a -t7z setup.7z Release\setup.msi Release\setup.exe
copy /b 7zS.sfx + config.txt + setup.7z "Enemizer Setup.exe"
pause