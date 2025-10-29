#  Pokemon Backend API

Una API RESTful sencilla construida con .NET 8 que sirve datos de Pokémon, optimizada con caché para ser rápida y eficiente.

Este proyecto es el backend que alimenta a una aplicación de Pokemoni. Su única misión es obtener información de la PokéAPI oficial y servirla de forma optimizada a un frontend.

---

## ¿Qué hace esta API?

*   **Búsqueda y Paginación:** Permite buscar un Pokémon por su nombre y navegar a través de páginas de resultados.
*   **Caché Inteligente:** Guarda las respuestas de la PokéAPI en la memoria del servidor. Esto evita hacer peticiones repetidas y hace que las respuestas sean casi instantáneas.
*   **Lista para Frontend:** Está configurada con CORS para que cualquier aplicación web (como mi frontend de React) pueda consumirla sin problemas de seguridad.
*   **Fácil de Desplegar:** Incluye un `Dockerfile` para que se pueda subir a servicios en la nube como Render o Azure en cuestión de minutos.

---

## Tecnologías Utilizadas

*   **.NET 8** - La última versión del framework de Microsoft.
*   **ASP.NET Core Web API** - Para construir la API.
*   **C#** - El lenguaje de programación.
*   `IMemoryCache` - Para implementar el caché en memoria.
*   `HttpClient` - Para comunicarse con la PokéAPI.

---

## ¿Cómo ejecutar este proyecto?

Si quieres probarlo en tu máquina local, sigue estos pasos:

1.  **Clona este repositorio:**
    ```bash
    git clone https://github.com/TU_USUARIO/pokemon-backend-api.git
    cd pokemon-backend-api
    ```

2.  **Asegúrate de tener el SDK de .NET 8 instalado.**

3.  **Restaura las dependencias:**
    ```bash
    dotnet restore
    ```

4.  **Inicia la API:**
    ```bash
    dotnet run
    ```

5.  ¡Listo! La API estará corriendo. Abre tu navegador y ve a `http://localhost:5000/swagger` (o la URL que te indique la terminal). Allí podrás ver la documentación interactiva y probar los endpoints.

---

## Estructura del Proyecto

He organizado el código de forma lógica para que sea fácil de entender:

*   `Controllers/`: Aquí vive el `PokemonController`, que es el que recibe las peticiones HTTP (como `GET /api/pokemon`).
*   `Services/`: La lógica principal está en `PokemonApiService`. Esta clase se encarga de hablar con la PokéAPI, gestionar el caché y filtrar los datos.
*   `Models/`: Contiene los DTOs (Data Transfer Objects) como `PokemonDto`. Son clases simples para enviar solo la información necesaria al frontend, sin datos extra.

¡Y eso es todo! Con esta API funcionando, el frontend hecho en React, tiene todo lo que necesita para crear una experiencia de usuario genial.
