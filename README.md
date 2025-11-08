# InventoryManagement API

Un sistema robusto de **Gestión de Inventario** construido con **.NET
8**, siguiendo los principios de **Arquitectura Limpia (Clean
Architecture)**.\
La solución proporciona una API modular y escalable para administrar
**clientes**, **productos**, **ventas** y **usuarios**, protegida
mediante **autenticación JWT**.

------------------------------------------------------------------------

## Estructura del Proyecto

    InventoryManagement.sln
    │
    ├── src
    │   ├── InventoryManagement.Domain          # Capa de dominio - Entidades, interfaces y lógica central
    │   ├── InventoryManagement.Application     # Capa de aplicación - DTOs, casos de uso y servicios
    │   ├── InventoryManagement.Infrastructure  # Capa de infraestructura - EF Core, repositorios y contexto de base de datos
    │   └── InventoryManagement.API             # Capa de presentación - Controladores, configuración y Swagger
    │
    └── tests                                  # (Opcional) Pruebas unitarias e integradas

------------------------------------------------------------------------

## 🧩 Arquitectura

El proyecto sigue los principios de **Arquitectura Limpia**, separando
las responsabilidades en capas bien definidas:

  -----------------------------------------------------------------------
  Capa                  Descripción
  --------------------- -------------------------------------------------
  **Domain**            Contiene las entidades del negocio y las
                        interfaces de repositorio.

  **Application**       Contiene los DTOs, mapeos y la lógica de negocio.

  **Infrastructure**    Gestiona el acceso a datos mediante **Entity
                        Framework Core** y el patrón **Repositorio +
                        Unidad de Trabajo (Unit of Work)**.

  **API                 Expone los endpoints REST, maneja la
  (Presentation)**      autenticación JWT y documenta los servicios con
                        **Swagger**.
  -----------------------------------------------------------------------

------------------------------------------------------------------------

## Configuración de Base de Datos

### LocalDB

El proyecto utiliza **SQL Server 2022 LocalDB** para desarrollo local.

Crea una instancia y una base de datos con los siguientes comandos:

``` bash
sqllocaldb create "InventoryInstance"
sqllocaldb start "InventoryInstance"

sqlcmd -S "(localdb)\InventoryInstance" -Q "CREATE DATABASE InventoryDB"
```

### Cadena de Conexión

Define tu cadena de conexión en el archivo `appsettings.json` dentro del
proyecto **InventoryManagement.API**:

``` json
{
  "ConnectionStrings": {
    "Database": "Server=(localdb)\InventoryInstance;Database=InventoryDB;User Id=InventoryUser;Password=TuContraseña123;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "EstaEsUnaClaveSecretaParaJWT",
    "Issuer": "InventoryAPI",
    "Audience": "InventoryAPIUsers"
  }
}
```

------------------------------------------------------------------------

## Entity Framework Core

Para generar y aplicar migraciones:

``` bash
cd src/InventoryManagement.Infrastructure
dotnet ef migrations add InitialCreate -s ../InventoryManagement.API
dotnet ef database update -s ../InventoryManagement.API
```

------------------------------------------------------------------------

## Autenticación JWT

La API utiliza **tokens JWT** para proteger los endpoints.\
Cada usuario puede iniciar sesión y obtener un token que se debe enviar
en el encabezado `Authorization`:

    Authorization: Bearer <tu_token_jwt>

### Ejemplo de solicitud de inicio de sesión

``` http
POST /api/Auth/login
Content-Type: application/json

{
  "email": "admin@example.com",
  "password": "Password123!"
}
```

------------------------------------------------------------------------

## 🚀 Ejecución del Proyecto

1.  **Clonar el repositorio**

    ``` bash
    git clone https://github.com/stevenfloriano/InventoryManagementBack.git
    cd InventoryManagementBack/src/InventoryManagement.API
    ```

2.  **Restaurar dependencias**

    ``` bash
    dotnet restore
    ```

3.  **Aplicar migraciones**

    ``` bash
    dotnet ef database update
    ```

4.  **Ejecutar la API**

    ``` bash
    dotnet run
    ```

5.  **Abrir Swagger UI**

        https://localhost:5001/swagger

------------------------------------------------------------------------

## Endpoints Disponibles

### Autenticación

  -----------------------------------------------------------------------
  Método            Endpoint                 Descripción
  ----------------- ------------------------ ----------------------------
  POST              `/api/Auth/login`        Autentica al usuario 

  -----------------------------------------------------------------------

------------------------------------------------------------------------

### Clientes

  Método   Endpoint                Descripción
  -------- ----------------------- --------------------------------
  GET      `/api/Customers`        Obtiene todos los clientes
  GET      `/api/Customers/{id}`   Obtiene un cliente por ID
  POST     `/api/Customers`        Crea un nuevo cliente
  PUT      `/api/Customers/{id}`   Actualiza un cliente existente
  DELETE   `/api/Customers/{id}`   Elimina un cliente

------------------------------------------------------------------------

### Productos

  Método   Endpoint               Descripción
  -------- ---------------------- -----------------------------
  GET      `/api/Products`        Obtiene todos los productos
  GET      `/api/Products/{id}`   Obtiene un producto por ID
  POST     `/api/Products`        Crea un nuevo producto
  PUT      `/api/Products/{id}`   Actualiza un producto
  DELETE   `/api/Products/{id}`   Elimina un producto

------------------------------------------------------------------------

### Ventas

  Método   Endpoint            Descripción
  -------- ------------------- -------------------------------
  GET      `/api/Sales`        Obtiene todas las ventas
  GET      `/api/Sales/{id}`   Obtiene una venta por ID
  POST     `/api/Sales`        Registra una nueva venta
  PUT      `/api/Sales/{id}`   Actualiza una venta existente
  DELETE   `/api/Sales/{id}`   Elimina una venta

------------------------------------------------------------------------

### Usuarios

  Método   Endpoint            Descripción
  -------- ------------------- -----------------------------------------
  GET      `/api/Users`        Obtiene todos los usuarios (solo admin)
  GET      `/api/Users/{id}`   Obtiene un usuario por ID
  POST     `/api/Users`        Crea un nuevo usuario
  PUT      `/api/Users/{id}`   Actualiza los datos de un usuario
  DELETE   `/api/Users/{id}`   Elimina un usuario (solo admin)

------------------------------------------------------------------------

## Tecnologías Utilizadas

-   **.NET 8**
-   **C#**
-   **Entity Framework Core**
-   **SQL Server 2022 **
-   **ASP.NET Core Web API**
-   **JWT Authentication**
-   **Swagger / OpenAPI**
-   **Inyección de Dependencias**
-   **Patrón Repositorio y Unidad de Trabajo (Unit of Work)**

------------------------------------------------------------------------

## ⚙Notas de Desarrollo

-   El **UnitOfWork** centraliza el control de transacciones.\
-   Los **repositorios** abstraen la lógica de acceso a datos.\
-   Los **DTOs** separan las entidades del dominio de las respuestas de
    la API.\
-   El **middleware JWT** protege los endpoints contra accesos no
    autorizados.\
-   **Swagger** genera automáticamente la documentación de los
    servicios.

------------------------------------------------------------------------

## Autor

Desarrollado por David Floriano
Proyecto orientado a la modernización de soluciones ERP de escritorio hacia plataformas web escalables.

------------------------------------------------------------------------

## Licencia

Puedes usarlo, modificarlo y distribuirlo libremente, mencionando la fuente original.
