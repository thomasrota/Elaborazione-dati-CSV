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
            Visualizza();
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
            string[] nomecampi = NomeCampi();
            string valori = "";
            for (int i = 0; i < lMaxCampi.Length - 1; i++)
            {
                valori += $"{lMaxCampi[i]} caratteri per {nomecampi[i]}, ";
            }
            MessageBox.Show($"La lunghezza massima del record è di {lMaxRecord} caratteri!");
            MessageBox.Show($"La lunghezza massima per ogni campo è di: {valori}");
        }
        private void Rcamp_Click(object sender, EventArgs e)
        {
            string ricerca = RicercaCampo(int.Parse(textBox1.Text));
            if (ricerca == "f")
            {
                MessageBox.Show("Elemento non trovato!, ERRORE");
            }
            else
                MessageBox.Show($"{ricerca}");
        }
        private void CancLogica_Click(object sender, EventArgs e)
        {
            int ric = Ricerca(int.Parse(textBox1.Text));
            if (ric == -1)
                MessageBox.Show("Elemento non trovato!", "ERRORE");
            else
            {
                CancellazioneLogica(ric);
            }
            listView1.Clear();
            Visualizza();
        }
        private void Racqu_Click(object sender, EventArgs e)
        {
            int ric = RicercaReacq(textBox1.Text);
            if (ric == -1)
                MessageBox.Show("Elemento non trovato!", "ERRORE");
            else
            {
                Reacquisizione(ric);
            }
            listView1.Clear();
            Visualizza();
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
                            linea += ";" + r.Next(10, 21) + ";" + "0;";
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
                linea = sr.ReadLine();
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] campo = linea.Split(';');
                    for (int j = 0; j < campo.Length; j++)
                    {
                        if (campo[j].Length > campiL[j])
                            campiL[j] = campo[j].Length;
                    }
                }
            }
            return campiL;
        }
        public string[] NomeCampi()
        {
            string[] nomi = new string[11];
            string linea;
            using (StreamReader sr = File.OpenText(path))
            {
                linea = sr.ReadLine();
                string[] campo = linea.Split(';');
                for (int i = 0; i < nomi.Length; i++)
                {
                    nomi[i] = campo[i];
                }
            }
            return nomi;
        }
        // Inserire in ogni record un numero di spazi necessari a rendere fissa la dimensione di tutti i record, senza perdere informazioni.
        public void AggiuntaSpazi()
        {

        }
        // Aggiungere un record in coda;
        // Ricercare un record per campo chiave a scelta (se esiste, utilizzare il campo che contiene dati univoci);
        public string RicercaCampo(int m)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                linea = sr.ReadLine();
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] campo = linea.Split(';');
                    int nric = int.Parse(campo[0]);
                    if (m == nric)
                    {
                        return linea;
                    }
                }
            }
            return "f";
        }
        // Modificare  un record;
        // Cancellare logicamente un record;
        public int Ricerca(int nome)
        {
            int pos = -1;
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                int riga = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    riga++;
                    string[] dati = s.Split(';');
                    if (dati[10] == "0")
                    {
                        if (int.Parse(dati[0]) == nome)
                        {
                            pos = riga;
                            break;
                        }
                    }
                }
            }
            return pos;
        }
        public void CancellazioneLogica(int posizione)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    int riga = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        riga++;
                        string[] dati = s.Split(';');
                        if (riga == posizione)
                        {
                            sw.WriteLine($"{dati[0]};{dati[1]};{dati[2]};{dati[3]};{dati[4]};{dati[5]};{dati[6]};{dati[7]};{dati[8]};{dati[9]};1;");
                        }
                        else
                        {
                            sw.WriteLine(s);   
                        }
                    }
                }
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        public int RicercaReacq(string nome)
        {
            int pos = -1;
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                int riga = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    riga++;
                    string[] dati = s.Split(';');
                    if (dati[0] == nome)
                    {
                        pos = riga;
                        break;
                    }
                }
            }
            return pos;
        }
        public void Reacquisizione(int posizione)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    int riga = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        riga++;
                        string[] dati = s.Split(';');
                        if (riga != posizione)
                        {
                            sw.WriteLine(s);
                        }
                        else
                        {
                            sw.WriteLine($"{dati[0]};{dati[1]};{dati[2]};{dati[3]};{dati[4]};{dati[5]};{dati[6]};{dati[7]};{dati[8]};{dati[9]};0;");
                        }
                    }
                }
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Visualizzare dei dati mostrando tre campi significativi a scelta;
        public void Visualizza()
        {
            string[] colonne = NomeCampi();
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                listView1.View = View.Details;
                for(int i = 0; i < colonne.Length - 1; i++)
                {
                    listView1.Columns.Add(colonne[i], 108, HorizontalAlignment.Center);
                }
                listView1.GridLines = true;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] dati = linea.Split(';');
                    if (dati[10] == "0")
                    {
                        ListViewItem newItem = new ListViewItem();
                        newItem.Text = dati[0];
                        for (int j = 1; j < colonne.Length - 1; j++)
                        {
                            newItem.SubItems.Add(dati[j]);
                        }
                        listView1.Items.Add(newItem);
                    }
                }
                sr.Close();
            }
        }
        #endregion
    }
}
