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
        public string path;
        #endregion
        #region Funzioni evento
        public Form1()
        {
            InitializeComponent();
            path = @"rota.csv";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            Visualizza(path);
        }
        #endregion
        #region Funzioni di Servizio
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
