using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Threading;
using eventfabric.api;

namespace RSSReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Obtiene la lista de RSS de diferentes diarios
            var listaRSS = FileReader.ObtenerRSS();

            Client client = new Client();
            Response response = client.Login("dariocingolani", "soyeldaro");

            
                //Recorre la lista de RSS y obtiene el contenido de las noticias
                foreach (var url in listaRSS)
                {
                    try
                    {

                        XmlReader reader = XmlReader.Create(url);
                        SyndicationFeed feed = SyndicationFeed.Load(reader);
                        FeedData feedData = new FeedData();

                        foreach (SyndicationItem item in feed.Items)
                        {
                            feedData.Titulo = item.Title.Text;
                            feedData.Link = item.Links[0].Uri;
                            feedData.Descripcion = item.Summary.Text;

                            Event evento = new Event("RSSReader", feedData);
                            client.SendEvent(evento, response.Cookies);

                            //Para que los eventos se visualicen mejor en pantalla
                            Thread.Sleep(2000);
                        }
                        reader.Close();

                    }
                    catch (Exception)
                    {
                        //Si hay un error en el RSS sigo con la siguiente
                        continue;
                    }
                }  
        }
    }
}
