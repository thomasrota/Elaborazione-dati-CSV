using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elaborazione_dati_CSV
{
    public partial class Form1 : Form
    {
        #region Dichiarazioni
        public string path, pathTEMP;
        int campiIniziali;
        #endregion
        #region Funzioni evento
        public Form1()
        {
            InitializeComponent();
            path = @"rota.csv";
            pathTEMP = @"rotaTEMP.csv";
            campiIniziali = 9;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            Visualizza(path);
        }
        private void agg_Click(object sender, EventArgs e)
        {
            int n = NumeroCampi(path);
            if (n == campiIniziali)
                AggiungiCampoMVE(path, pathTEMP);
            else
                MessageBox.Show("I campi 'Mio valore' e 'Cancellazione Logica' sono già presenti", "ERRORE!");
        }
        private void contacampi_Click(object sender, EventArgs e)
        {
            int n = NumeroCampi(path);
            MessageBox.Show($"In totale vi è/sono presente/i {n - 1} campo/i");
        }
        #endregion
        #region Funzioni di Servizio
        // Aggiungere, in coda ad ogni record, un campo chiamato "miovalore", contenente un numero casuale compreso tra 10<=X<=20 ed un campo per marcare la cancellazione logica;
        public void AggiungiCampoMVE(string file, string fileTEMP)
        {
            using (StreamReader sr = File.OpenText(file))
            {
                Random r = new Random();
                string linea;
                bool primaLinea = false;
                using (StreamWriter sw = new StreamWriter(fileTEMP, append: true))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (!primaLinea)
                        {
                            linea += ";Mio valore;Cancellazione logica;";
                            primaLinea = !primaLinea;
                        }
                        else
                            linea += ";" + r.Next(10, 21) + ";" + "0" + ";";
                        sw.WriteLine(linea);
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(file);
            File.Move(fileTEMP, file);
            File.Delete(fileTEMP);
        }
        // Contare il numero dei campi che compongono il record.
        public int NumeroCampi(string file)
        {
            int nCampi;
            string linea;
            using (StreamReader sr = File.OpenText(file))
            {
                linea = sr.ReadLine();
                nCampi = linea.Split(';').Length;
            }
            return nCampi;
        }

        // Calcolare la lunghezza massima dei record presenti (avanzato: indicando anche la lunghezza massima di ogni campo);
        // Inserire in ogni record un numero di spazi necessari a rendere fissa la dimensione di tutti i record, senza perdere informazioni. 
        // Aggiungere un record in coda;
        // Ricercare un record per campo chiave a scelta (se esiste, utilizzare il campo che contiene dati univoci);
        // Modificare  un record;
        // Cancellare logicamente un record;
        // Visualizzare dei dati mostrando tre campi significativi a scelta;
        public void Visualizza(string filePath)
        {
            /*
            using (StreamReader sr = File.OpenText(filePath))
            {
                string linea;
                Lista.View = View.Details;
                Lista.Columns.Add("Nome", 108, HorizontalAlignment.Left);
                Lista.Columns.Add("Prezzo", 108, HorizontalAlignment.Left);
                Lista.Columns.Add("Quantità", 108, HorizontalAlignment.Left);
                Lista.GridLines = true;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] dati = linea.Split(';');
                    if (dati[3] == "0")
                    {
                        float prezzo = float.Parse(dati[1]);
                        ListViewItem newItem = new ListViewItem();
                        newItem.Text = dati[0];
                        newItem.SubItems.Add(prezzo.ToString("0.00") + "€");
                        newItem.SubItems.Add(dati[2]);
                        Lista.Items.Add(newItem);
                    }
                }
                sr.Close();
            }
            */
        }
        #endregion
    }
}
