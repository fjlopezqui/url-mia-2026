using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Lab2
{
   static void Main()
   {
       List<Estudiante> listaEstudiantes = new List<Estudiante>();
       string[] lineasCSVest = File.ReadAllLines("estudiantes.csv");
       for (int i = 1; i < lineasCSVest.Length; i++)
        {
            string[] datosEstudiante = lineasCSVest[i].Split(',');

            Estudiante nuevoEstudiante = new Estudiante 
            { 
                    Id = Convert.ToInt32(datosEstudiante[0]),
                    Nombre = datosEstudiante[1],
                    Carrera = datosEstudiante[2]
            };

            listaEstudiantes.Add(nuevoEstudiante);

        }

        foreach (Estudiante est in listaEstudiantes)
        {
            Console.WriteLine($"ID: {est.Id} | Nombre: {est.Nombre} | Carrera: {est.Carrera}");
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        string textoJSON = JsonSerializer.Serialize(listaEstudiantes, new JsonSerializerOptions {WriteIndented = true});

        File.WriteAllText("estudiantes.json", textoJSON);

   }
 
}