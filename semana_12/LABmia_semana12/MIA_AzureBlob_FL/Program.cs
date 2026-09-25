using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string? connectionString = "En README.md semana 12";

string containerName = "miaarchivos";
BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
await containerClient.CreateIfNotExistsAsync();

string? opcion = "";

while (opcion != "5")
{
    Console.WriteLine("\n=================================");
    Console.WriteLine("     MIA - AZURE BLOB STORAGE    ");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Subir archivo");
    Console.WriteLine("2. Listar archivos");
    Console.WriteLine("3. Descargar archivo");
    Console.WriteLine("4. Eliminar archivo");
    Console.WriteLine("5. Salir");
    Console.Write("Elige una opción: "); 

    opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Write("Ingrese la ruta del archivo: ");
            string? ruta = Console.ReadLine();

            if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta))
            {
                Console.WriteLine("Error: El archivo no existe o la ruta es inválida.");
                break;
            }

            string? nombreArchivo = Path.GetFileName(ruta);
            BlobClient blob1 = containerClient.GetBlobClient(nombreArchivo);

            Console.WriteLine("Subiendo archivo...");
            await blob1.UploadAsync(ruta, true); 
            Console.WriteLine($"Archivo {nombreArchivo} subido exitosamente.");
            break;

        case "2": 
            Console.WriteLine("\nNombre\t\t\tTamaño");
            Console.WriteLine("--------------------------------");
            await foreach (var archivo in containerClient.GetBlobsAsync())
            {
                Console.WriteLine($"{archivo.Name}\t\t{archivo.Properties.ContentLength} bytes");
            }
            break;
        
        case "3": 
            Console.WriteLine("\nArchivos disponibles:");
            Console.WriteLine("Nombre\t\t\tTamaño");
            Console.WriteLine("--------------------------------");
            await foreach (var archivo in containerClient.GetBlobsAsync())
            {
                Console.WriteLine($"{archivo.Name}\t\t{archivo.Properties.ContentLength} bytes");
            }

            Console.Write("\nIngrese el nombre del archivo en la nube: ");
            string? nombre = Console.ReadLine();

            Console.Write("Ingrese la ruta de la carpeta donde lo desea guardar: ");
            string? rutaCarpeta = Console.ReadLine();

            if (!Directory.Exists(rutaCarpeta) && !string.IsNullOrEmpty(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            string destino = Path.Combine(rutaCarpeta ?? "", nombre ?? "");
            BlobClient blob2 = containerClient.GetBlobClient(nombre);

            if (await blob2.ExistsAsync())
            {
                await blob2.DownloadToAsync(destino);
                Console.WriteLine($"¡Archivo {nombre} guardado en {destino}!");
            }
            else
            {
                Console.WriteLine("Error: El archivo especificado no existe en el contenedor.");
            }
            break;

        case "4":
            Console.Write("Nombre del archivo a eliminar: ");
            string? nombreEliminar = Console.ReadLine();
            
            BlobClient blob3 = containerClient.GetBlobClient(nombreEliminar);
            
            bool eliminado = await blob3.DeleteIfExistsAsync();
            if (eliminado)
            {
                Console.WriteLine("¡Archivo eliminado de la nube exitosamente!");
            }
            else
            {
                Console.WriteLine("El archivo no existía en el contenedor.");
            }
            break;

        case "5":
            Console.WriteLine("Gracias por usar el sistema :)");
            break;

        default:
            Console.WriteLine("Opción no válida");
            break;
    }
}