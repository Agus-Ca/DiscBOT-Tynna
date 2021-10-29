using System;

//importamos las librerias necesarias para crear el bot
using System.Threading.Tasks;


namespace Tynna
{
    class Program
    {
        public static async Task Main(string[] args)
            => await Startup.RunAsync(args);
    }
}