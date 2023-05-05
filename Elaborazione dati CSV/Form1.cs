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
        public int campiIniziali;
        public int righeIniziali;
        public int righe;
        #endregion
        #region Funzioni evento
        public Form1()
        {
            InitializeComponent();
            path = @"rota.csv";
            pathTEMP = @"rotaTEMP.csv";
            campiIniziali = 9;
            righeIniziali = 16;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            Visualizza(path);
        }
        private void agg_Click(object sender, EventArgs e)
        {
            int n = NumeroCampi();
            if (n == campiIniziali)
                AggiungiCampoMVE();
            else
                MessageBox.Show("I campi 'Mio valore' e 'Cancellazione Logica' sono già presenti", "ERRORE!");
        }
        private void contacampi_Click(object sender, EventArgs e)
        {
            int n = NumeroCampi();
            MessageBox.Show($"In totale vi è/sono presente/i {n - 1} campo/i");
        }
        private void RecordLenght_Click(object sender, EventArgs e)
        {
            int lMaxRecord = LunghezzaMaxRecord();
            int[] lMaxCampi = LunghezzaMaxCampi();
            string valori = "";
            for (int i = 0; i < lMaxCampi.Length - 1; i++)
            {
                valori += $"{lMaxCampi[i]}, ";
            }
            MessageBox.Show($"La lunghezza massima del record è di {lMaxRecord} caratteri!");
            MessageBox.Show($"La lunghezza massima per ogni campo è di: {valori} caratteri");
        }
        #endregion
        #region Funzioni di Servizio
        // Aggiungere, in coda ad ogni record, un campo chiamato "miovalore", contenente un numero casuale compreso tra 10<=X<=20 ed un campo per marcare la cancellazione logica;
        public void AggiungiCampoMVE()
        {
            using (StreamReader sr = File.OpenText(path))
            {
                Random r = new Random();
                string linea;
                bool primaLinea = false;
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
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
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Contare il numero dei campi che compongono il record.
        public int NumeroCampi()
        {
            int nCampi;
            string linea;
            using (StreamReader sr = File.OpenText(path))
            {
                linea = sr.ReadLine();
                nCampi = linea.Split(';').Length;
            }
            return nCampi;
        }

        // Calcolare la lunghezza massima dei record presenti (avanzato: indicando anche la lunghezza massima di ogni campo);
        public int LunghezzaMaxRecord()
        {
            int[] recordL = new int[righeIniziali];
            int maxRecord = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                int i = 0;
                while ((linea = sr.ReadLine()) != null)
                {
                    recordL[i] = linea.Length;
                    i++;
                }
                maxRecord = recordL.Max();
            }
            return maxRecord;
        }
        public int[] LunghezzaMaxCampi()
        {
            int dim = NumeroCampi();
            int[] campiL = new int[dim];
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] campo = linea.Split(';');
                    for(int j = 0; j < campo.Length; j++)
                    {
                        if (campo[j].Length > campiL[j])
                            campiL[j] = campo[j].Length;
                    }
                }
            }
            return campiL;
        }
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
