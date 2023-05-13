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
    public partial class Form2 : Form
    {
        #region Dichiarazioni
        public string path, pathTEMP;
        #endregion
        #region Funzioni evento
        public Form2()
        {
            InitializeComponent();
            path = @"rota.csv";
            pathTEMP = @"rotaTEMP.csv";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
        private void Aggiungi_Click(object sender, EventArgs e)
        {
            AggiuntaRecordCoda(int.Parse(MunAgg.Text), ZnUrbAgg.Text, RioneAgg.Text, QuartAgg.Text, SubUrbAgg.Text, ZoneAgrAgg.Text, BorgAgg.Text, ExMunAgg.Text, EtcAgg.Text);
            MessageBox.Show("Elemento inserito correttamente!");
        }
        private void Mod_Click(object sender, EventArgs e)
        {
            Modifica(int.Parse(CampoRicerc.Text), int.Parse(MunMod.Text), ZnUrbMod.Text, RioneMod.Text, QuartMod.Text, SubUrbMod.Text, ZnAgrMod.Text, BorgMod.Text, ExMunMod.Text, EtcMod.Text);
            MessageBox.Show("Elemento modificato correttamente!");
        }
        #endregion
        #region Funzioni di Servizio
        // Aggiungere un record in coda;
        public void AggiuntaRecordCoda(int mun, string znurb, string rione, string quartiere, string suburb, string znagro, string borgo, string exmun, string etichetta)
        {
            Random r = new Random();
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLine($"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};{r.Next(10, 21)};0;");
                sw.Close();
            }
        }
        // Modificare un record;
        public void Modifica(int nome, int mun, string znurb, string rione, string quartiere, string suburb, string znagro, string borgo, string exmun, string etichetta)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                linea = sr.ReadLine();
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    sw.WriteLine(linea);
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] dati = linea.Split(';');
                        string stringains = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};{dati[9]};0;";
                        if (dati[10] == "0")
                        {
                            if (int.Parse(dati[0]) == nome)
                            { 
                                if (linea.Contains("##"))
                                    sw.WriteLine(stringains.PadRight(500) + "##");
                                else
                                    sw.WriteLine(stringains);
                            }
                            else
                            {
                                if (linea.Contains("##"))
                                    sw.WriteLine(linea);
                                else
                                    sw.WriteLine(linea.PadRight(500) + "##");
                            }
                        }
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        #endregion
    }
}
