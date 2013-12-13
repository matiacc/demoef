using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using eventfabric.api;
using System.Diagnostics;
using System.Threading;
namespace MyDemoEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
           
            Client client = new Client();
            Response loginResponse = client.Login("matiasluna", "31356140");
                
            Process[] procs= Process.GetProcesses();
            progressBar1.Step = (int)procs.Length / 100;
            int i=0;
            label2.Text = procs.Length + " procesos en Ejecución";
            foreach (Process process in procs)
            {
                i++;
                Proceso proceso = new Proceso();
                proceso.Nombre = process.ProcessName;
                proceso.MemoriaUsada = process.PrivateMemorySize64/1024;

                Event evento = new Event("Procesos", proceso);
                client.SendEvent(evento, loginResponse.Cookies);

                progressBar1.PerformStep();
                label1.Text = (procs.Length - i).ToString() + " procesos pendientes...";
                Thread.Sleep(1000);
                Application.DoEvents();
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Procesos Procesados :)", "Fin de Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private float GetAvailableRam()
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return ramCounter.NextValue();
            //MessageBox.Show("Current CPU Usage: " + cpuCounter.NextValue() + "%" + "\nAvailable RAM: " + ramCounter.NextValue() + "MB");
        }
    }
}
