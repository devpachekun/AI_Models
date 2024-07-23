using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var gpt4 = new GPT4(configuration);

        string mensaje = "Hola, ¿cómo estás?";
        string respuesta = await gpt4.EnviarMensaje(mensaje);

        Console.WriteLine("Respuesta de GPT-4: " + respuesta);
    }
}
