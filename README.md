# ApiTransferencia
Manual para Levantar la Aplicación
1. Pre-requisitos

Antes de comenzar, asegúrate de tener lo siguiente instalado en tu sistema:

    .NET SDK 8.0
    PostgreSQL

2. Configurar la Base de Datos
2.1 Crear una Base de Datos

Abre psql o utiliza pgAdmin para crear una base de datos, por ejemplo, TransferenciasDb.
2.2 Actualizar la Cadena de Conexión

En el archivo appsettings.json, actualiza la cadena de conexión para que apunte a tu base de datos PostgreSQL. Asegúrate de reemplazar tu_contraseña con tu contraseña real.

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TransferenciasDb;Username=postgres;Password=tu_contraseña"
  }
}
3. Migraciones de Base de Datos

Si estás utilizando Entity Framework Core, aplica las migraciones para crear las tablas necesarias en la base de datos:

dotnet ef migrations add InitialCreate
dotnet ef database update

4. Ejecutar la Aplicación

Para ejecutar la aplicación, utiliza el siguiente comando en la terminal:

dotnet run

5. Documentación Swagger

Si tu aplicación está en modo de desarrollo, puedes acceder a la documentación Swagger en:

http://localhost:5154/swagger/index.html

-capturas de pruebas en la carpeta SwaggerImages

-imagen del diagrama de solucion en la carpeta diagrama
