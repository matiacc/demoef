using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eventfabric.api;

namespace formularios
{
    public partial class Form1 : Form
    {
        Client client;
        Response response;
        public Form1()
        {
            InitializeComponent();
            client = new Client();
            response = client.Login("dariocingolani", "soyeldaro");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Domicilio domicilio = new Domicilio() { Calle = txtCalle.Text };
            Event evento = new Event("persona", new Persona() { 
                Domicilio = domicilio,
                Estado = txtEstado.Text,
                Nombre = txtNombre.Text 
            });

            client.SendEvent(evento, response.Cookies);
        }
    }
}
