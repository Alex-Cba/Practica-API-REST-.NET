# Practica API REST .NET
 Un proyecto el cual es una práctica de desarrollo de APIs en .NET Core

 Este proyecto tiene como objetivo simular el desarrollo fullstack de una empresa "X",
 la cual necesita crear, actualizar y eliminar empleados en su base de datos, atrevés de un front-end.


Probar demostración de página en línea: https://empresa-generica.netlify.app/
 - La demostración es completamente funcional, con su front-end, y back-end(Api y base de datos) completamente en línea.

 - Se aplicó el patrón CQRS, permitiendo separar las Querys y los Commands.
 - El desarrollo de la base de datos por la técnica de programación CodeFirst, con su base de datos en PostgreSQL
 - Se aplicó el patrón Mediator, con la extensión MediaTR
 - A su vez también se encuentran un amplio manejo de los potenciales errores que pueda tener el usuario,
   o las discrepancias en la base de datos, con la extensión FluentValidations
 - El desarrollo de un front-end con bootstrap, y jquery, para que este sé más atractivo al usuario, y se le informe
   sobre si se logró crear, actualizar o eliminar el usuario

- El proyecto puede sufrir cambios, ya que estoy en constante aprendizaje y va a ser mejorado con los conocimientos que adquiera. 

 Aclaraciones: La migración no existe, así que ustedes pueden crear la migración si lo desea,
		para ello debe configurar la DefaultConnection en el archivo appsettings.json del proyecto.


![API-REST-Full](https://github.com/Gr1ll0/Practica-API-REST-.NET/assets/111154509/639ad4d1-36e8-4aae-9e9e-329ac68e3ea4)
