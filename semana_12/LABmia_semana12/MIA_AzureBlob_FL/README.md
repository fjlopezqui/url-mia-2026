# MIA - Azure Blob Storage (Aplicación de Consola)

## 1. Objetivo de la aplicación
El objetivo de este proyecto es construir una aplicación de consola interactiva que permita conectarse a Microsoft Azure y administrar archivos en la nube. La aplicación funciona como un cliente sencillo para realizar las operaciones CRUD (subir, listar, descargar y eliminar) sobre un contenedor en Azure Blob Storage.

## 2. Tecnologías utilizadas
* Lenguaje: C# / .NET
* Entorno de ejecución: Consola / Terminal
* Librería de la nube: 'Azure.Storage.Blobs' (SDK oficial de Azure)
* Entorno de desarrollo: Visual Studio Code

## 3. Configuración de Azure
1. Se cuenta con un Storage Account llamado 'semana12storage'.
2. Se extrae la Connection String con su respectiva 'Account Key' desde el portal de Azure.
3. El programa se conecta automáticamente y crea el contenedor 'miaarchivos' en caso de que no exista previamente.

## 4. Arquitectura de la solución
El programa utiliza un ciclo 'while' y una estructura 'switch' para el menú principal. La conexión se gestiona en tres niveles del SDK:
* 'BlobServiceClient': Gestiona la autenticación global a la cuenta de almacenamiento mediante la cadena de conexión.
* 'BlobContainerClient': Administra el contenedor específico 'miaarchivos'.
* 'BlobClient': Maneja las acciones sobre los archivos individuales (blobs).

## 5. Descripción de las cuatro operaciones
* Subir archivo: Solicita la ruta local de un archivo y lo transfiere al contenedor con 'UploadAsync(ruta, true)'.
* Listar archivos: Utiliza 'GetBlobsAsync()' para iterar sobre todos los blobs e imprimir en consola su nombre y tamaño en bytes.
* Descargar archivo: Pide el nombre del archivo guardado en la nube y la carpeta local de destino, ejecutando 'DownloadToAsync()'.
* Eliminar archivo: Busca la referencia del archivo por nombre y lo remueve mediante 'DeleteIfExistsAsync()'.

## 6. Manejo de errores
* Validación de rutas: Comprueba mediante 'File.Exists()' la existencia de un archivo local antes de intentar subirlo.
* Creación de directorios: Si la carpeta de destino especificada para una descarga no existe, la crea dinámicamente con 'Directory.CreateDirectory()'.
* Verificación en la nube: Comprueba la existencia del blob con 'ExistsAsync()' antes de procesar una descarga o eliminación.

## 7. Configuración de la Connection String
Para fines del laboratorio, la cadena de conexión (Connection String) se definió directamente en la variable 'connectionString' dentro de 'Program.cs'. 

*Debido a que Github bloquea subir directamente el Conenection String por cuestiones de seguridad, la varible se colocó al inicio de documento PDF de evidencias de laboratorio 12*