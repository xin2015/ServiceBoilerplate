@echo off
echo create
sc create ServiceBoilerplateService binpath= %~dp0ServiceBoilerplate.WindowsService.exe
echo start
net start ServiceBoilerplateService
echo config
sc config ServiceBoilerplateService start= AUTO
echo description
sc description ServiceBoilerplateService Ñù°å·þÎñ
echo done
pause