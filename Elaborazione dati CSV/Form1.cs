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
using System.Drawing.Text;

namespace Elaborazione_dati_CSV
{
    public partial class Form1 : Form
    {
        #region Dichiarazioni
        public string path, pathTEMP;
        public int campiIniziali;
        public int righeIniziali;
        public int righe;
        Funzioni f;
        #endregion
        #region Funzioni evento
        public Form1()
        {
            InitializeComponent();
            path = @"rota.csv";
            pathTEMP = @"rotaTEMP.csv";
            campiIniziali = 9;
            righeIniziali = 16;
            f = new Funzioni();
            f.Fnz(path, pathTEMP);
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            Visualizza();
        }
        private void agg_Click(object sender, EventArgs e)
        {
            int n = f.NumeroCampi(path);
            if (n == campiIniziali)
            {
                f.AggiungiCampoMVE(path, pathTEMP);
                MessageBox.Show("Campi aggiunti correttamente!");
                listView1.Clear();
                Visualizza();
            }     
            else
                MessageBox.Show("I campi 'Mio valore' e 'Cancellazione Logica' sono già presenti", "ERRORE!");
        }
        private void contacampi_Click(object sender, EventArgs e)
        {
            int n = f.NumeroCampi(path);
            MessageBox.Show($"In totale vi è/sono presente/i {n} campo/i");
        }
        private void RecordLenght_Click(object sender, EventArgs e)
        {
            int lMaxRecord = f.LunghezzaMaxRecord(path, righeIniziali);
            int[] lMaxCampi = f.LunghezzaMaxCampi(path);
            string[] nomecampi = f.NomeCampi(path);
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
            string ricerca = f.RicercaCampo(int.Parse(textBox1.Text), path);
            if (ricerca == "f")
                MessageBox.Show("Elemento non trovato!, ERRORE");
            else
                MessageBox.Show($"{ricerca}");
        }
        private void CancLogica_Click(object sender, EventArgs e)
        {
            int n = f.NumeroCampi(path);
            int ric = f.Ricerca(int.Parse(textBox1.Text), path);
            if (ric == -1)
                MessageBox.Show("Elemento non trovato!", "ERRORE");
            if (n == campiIniziali)
                MessageBox.Show("Cliccare prima sul tasto 'Aggiungi Mio valore e Cancellazione logica!'", "ERRORE");
            else
            {
                f.CancellazioneLogica(ric, path, pathTEMP);
                listView1.Clear();
                Visualizza();
            }
        }
        private void Racqu_Click(object sender, EventArgs e)
        {
            int n = f.NumeroCampi(path);
            int ric = f.Ricerca(int.Parse(textBox1.Text), path);
            if (ric == -1)
                MessageBox.Show("Elemento non trovato!", "ERRORE");
            if (n == campiIniziali)
                MessageBox.Show("Cliccare prima sul tasto 'Aggiungi Mio valore e Cancellazione logica!'", "ERRORE");
            else
            {
                f.Reacquisizione(ric, path, pathTEMP);
                listView1.Clear();
                Visualizza();
            }
        }
        private void PadRight_Click(object sender, EventArgs e)
        {
            f.AggiuntaSpazi(path, pathTEMP);
        }
        private void AggCoda_Click(object sender, EventArgs e)
        {
            Form2 FormAggMod = new Form2();
            FormAggMod.Show();
            FormAggMod.FormClosed += new FormClosedEventHandler(Form2_FormClosed);          // Registriamo la chiusura del Form2 e lo associamo al gestore di eventi
        }
        #endregion
        #region Funzioni di Servizio
        // Funzione che richiama le funzioni desiderate per gestire la chiusura del Form2
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            listView1.Clear();
            Visualizza();
        }
        // Visualizzare dei dati mostrando tre campi significativi a scelta;
        public void Visualizza()
        {
            int n = f.NumeroCampi(path);
            string[] colonne = f.NomeCampi(path);
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                listView1.View = View.Details;
                if (n == 11)
                {
                    for (int i = 0; i < colonne.Length - 1; i++)
                    {
                        listView1.Columns.Add(colonne[i], 108, HorizontalAlignment.Center);
                    }
                }
                else
                {
                    for (int i = 0; i < colonne.Length; i++)
                    {
                        listView1.Columns.Add(colonne[i], 108, HorizontalAlignment.Center);
                    }
                }
                listView1.GridLines = true;
                linea = sr.ReadLine();
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] dati = linea.Split(';');
                    if (n == 11)
                    {
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
                    else
                    {
                        ListViewItem newItem = new ListViewItem();
                        newItem.Text = dati[0];
                        for (int j = 1; j < colonne.Length; j++)
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
