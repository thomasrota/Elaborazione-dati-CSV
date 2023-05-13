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
        private void Modifica_Click(object sender, EventArgs e)
        {

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
            }
        }
        // Modificare  un record;
        #endregion
    }
}
