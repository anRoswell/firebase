@echo off

:: Establecer colores para los títulos
color 0E &:: Amarillo sobre fondo negro

title FIREBASE BCK Op360°

:: OBTENIENDO GIT
@echo git pull
git pull
git add .
git commit -m "Commit automatico realizado por .bat"
@echo git finalizado exitosamente!!!

:: BUILD
@echo Ejecutando docker build --build-arg AppEnv=production -t firebase_push_notificacion .
docker build --build-arg AppEnv=production -t firebase_push_notificacion .

:: CHANGE NAME
color 0A &:: Verde sobre fondo negro
@echo CHANGE TAG
docker image tag firebase_push_notificacion 73197546/firebase_push_notificacion:1.0.1
color 0E &:: Amarillo sobre fondo negro
@echo Cambio de Tag finalizado

:: PUSH
color 0B &:: Cyan sobre fondo negro
@echo Ejecutando docker push 73197546/firebase_push_notificacion:1.0.1
docker push 73197546/firebase_push_notificacion:1.0.1

color 0F &:: Blanco sobre fondo negro
@echo Finalizado
pause
