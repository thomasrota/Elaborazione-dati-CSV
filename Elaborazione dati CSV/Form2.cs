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
        Funzioni f;
        #endregion
        #region Funzioni evento
        public Form2()
        {
            InitializeComponent();
            path = @"rota.csv";
            pathTEMP = @"rotaTEMP.csv";
            f = new Funzioni();
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
        private void Aggiungi_Click(object sender, EventArgs e)
        {
            f.AggiuntaRecordCoda(int.Parse(MunAgg.Text), string.IsNullOrEmpty(ZnUrbAgg.Text) ? "-" : ZnUrbAgg.Text, string.IsNullOrEmpty(RioneAgg.Text) ? "-" : RioneAgg.Text, string.IsNullOrEmpty(QuartAgg.Text) ? "-" : QuartAgg.Text, string.IsNullOrEmpty(SubUrbAgg.Text) ? "-" : SubUrbAgg.Text, string.IsNullOrEmpty(ZoneAgrAgg.Text) ? "-" : ZoneAgrAgg.Text, string.IsNullOrEmpty(BorgAgg.Text) ? "-" : BorgAgg.Text, string.IsNullOrEmpty(ExMunAgg.Text) ? "-" : ExMunAgg.Text, string.IsNullOrEmpty(EtcAgg.Text) ? "-" : EtcAgg.Text, path, pathTEMP);
            MessageBox.Show("Elemento inserito correttamente!");
        }
        private void Mod_Click(object sender, EventArgs e)
        {
            int ricerca = f.Ricerca(int.Parse(CampoRicerc.Text), path);
            if (ricerca == -1)
                MessageBox.Show("Elemento non trovato!", "ERRORE");
            else
            {
                f.Modifica(int.Parse(CampoRicerc.Text), int.Parse(MunMod.Text), string.IsNullOrEmpty(ZnUrbMod.Text) ? "-" : ZnUrbMod.Text, string.IsNullOrEmpty(RioneMod.Text) ? "-" : RioneMod.Text, string.IsNullOrEmpty(QuartMod.Text) ? "-" : QuartMod.Text, string.IsNullOrEmpty(SubUrbMod.Text) ? "-" : SubUrbMod.Text, string.IsNullOrEmpty(ZnAgrMod.Text) ? "-" : ZnAgrMod.Text, string.IsNullOrEmpty(BorgMod.Text) ? "-" : BorgMod.Text, string.IsNullOrEmpty(ExMunMod.Text) ? "-" : ExMunMod.Text, string.IsNullOrEmpty(EtcMod.Text) ? "-" : EtcMod.Text, path, pathTEMP);
                MessageBox.Show("Elemento modificato correttamente!");
            }
        }
        #endregion
    }
}