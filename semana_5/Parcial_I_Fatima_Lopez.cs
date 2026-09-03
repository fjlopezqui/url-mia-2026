using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Usuario: ");
        string usuario = Console.ReadLine();

        string usuarioNomArchivo = usuario.Replace(" ", "_");

        Console.Write("Ruta del archivo .txt: ");
        string ruta = Console.ReadLine().Trim('"');

        if (!File.Exists(ruta))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }

        string contenido = File.ReadAllText(ruta);
        string[] lineas = File.ReadAllLines(ruta);

        int totalLineas = lineas.Length;
        int totalChar = contenido.Length;
        
        char separador = ' ';
        int totalPalabras = contenido.Split(separador, StringSplitOptions.RemoveEmptyEntries).Length;

        string csv = $"Fatima_Lopez,Lineas,Palabras,Caracteres\n{totalLineas},{totalPalabras},{totalChar}";


        Console.WriteLine($"Usuario: {usuario}");
        Console.WriteLine($"Ruta: {ruta}");
        Console.WriteLine("---- Resultado ----");
        Console.WriteLine($"Total de lineas: {totalLineas}");
        Console.WriteLine($"Total de palabras: {totalPalabras}");
        Console.WriteLine($"Total de caracteres: {totalChar}");

        Console.WriteLine("\nSalida CSV:");
        Console.WriteLine(csv);


        File.WriteAllText($"resultados_{usuarioNomArchivo}.csv", csv);
        Console.WriteLine($"\nGuardado en 'resultados_{usuarioNomArchivo}.csv'");
    }
}