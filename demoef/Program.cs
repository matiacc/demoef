using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eventfabric.api;
using System.Threading;

namespace demoef
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Response response = client.Login("dariocingolani", "soyeldaro");
            
            Random r = new Random();

            for (var i = 0; i < 100; i++)
            {
                Event evento = new Event("medicion", new Medicion() { Valor = r.Next(0, 100), Tipo = "cpu" });
                client.SendEvent(evento, response.Cookies);

                Event eventoMem = new Event("medicion", new Medicion() { Valor = r.Next(0, 100), Tipo = "mem" });
                client.SendEvent(eventoMem, response.Cookies);
                Thread.Sleep(100);
            }
        }
    }
}
