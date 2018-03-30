@echo off
echo stop
net stop ServiceBoilerplateService
echo delete
sc delete ServiceBoilerplateService
echo done
pause