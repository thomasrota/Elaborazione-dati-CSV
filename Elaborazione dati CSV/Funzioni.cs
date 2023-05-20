using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elaborazione_dati_CSV
{
    public class Funzioni
    {
        // Aggiungere, in coda ad ogni record, un campo chiamato "miovalore", contenente un numero casuale compreso tra 10<=X<=20 ed un campo per marcare la cancellazione logica;
        public void AggiungiCampoMVE(string path, string pathTEMP)
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
                            linea += "Mio valore;Cancellazione logica;";
                            primaLinea = !primaLinea;
                        }
                        else
                            linea += r.Next(10, 21) + ";" + "0;";
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
        public int NumeroCampi(string path)
        {
            int nCampi;
            string linea;
            using (StreamReader sr = File.OpenText(path))
            {
                linea = sr.ReadLine();
                nCampi = linea.Split(';').Length - 1;
                sr.Close();
            }
            return nCampi;
        }
        // Calcolare la lunghezza massima dei record presenti (avanzato: indicando anche la lunghezza massima di ogni campo);
        public int LunghezzaMaxRecord(string path, int righeIniziali)
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
                sr.Close();
            }
            return maxRecord;
        }
        public int[] LunghezzaMaxCampi(string path)
        {
            int dim = NumeroCampi(path);
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
                sr.Close();
            }
            return campiL;
        }
        public string[] NomeCampi(string path)
        {
            int n = NumeroCampi(path);
            string[] nomi = new string[n];
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
        public void AggiuntaSpazi(string path, string pathTEMP)
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
                        if (linea.Contains("##"))
                            sw.WriteLine(linea);
                        else
                            sw.WriteLine(linea.PadRight(500) + "##");
                    }
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Ricercare un record per campo chiave a scelta (se esiste, utilizzare il campo che contiene dati univoci);
        public string RicercaCampo(int m, string path)
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
                sr.Close();
            }
            return "f";
        }
        // Cancellare logicamente un record;
        public int Ricerca(int nome, string path)
        {
            int n = NumeroCampi(path);
            int pos = -1;
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                int riga = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    riga++;
                    string[] dati = s.Split(';');
                    if (n == 11)
                    {
                        if (dati[10] == "0")
                        {
                            if (int.Parse(dati[0]) == nome)
                            {
                                pos = riga;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dati[0] == nome.ToString())
                        {
                            pos = riga;
                            break;
                        }
                    }
                }
                sr.Close();
            }
            return pos;
        }
        public void CancellazioneLogica(int posizione, string path, string pathTEMP)
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
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        public int RicercaReacq(string nome, string path)
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
                sr.Close();
            }
            return pos;
        }
        public void Reacquisizione(int posizione, string path, string pathTEMP)
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
                    sw.Close();
                }
                sr.Close();
            }
            File.Delete(path);
            File.Move(pathTEMP, path);
            File.Delete(pathTEMP);
        }
        // Aggiungere un record in coda;
        public void AggiuntaRecordCoda(int mun, string znurb, string rione, string quartiere, string suburb, string znagro, string borgo, string exmun, string etichetta, string path, string pathTEMP)
        {
            int n = NumeroCampi(path);
            Random r = new Random();
            File.Copy(path, pathTEMP);
            using (StreamReader sr = File.OpenText(path))
            {
                string linea;
                linea = sr.ReadLine();
                linea = sr.ReadLine();
                string valoreagg;
                using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                {
                    if (linea.Contains("##"))
                    {
                        if (n == 11)
                        {
                            valoreagg = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};{r.Next(10, 21)};0;";
                            sw.WriteLine(valoreagg.PadRight(500) + "##");
                        }
                        else
                        {
                            valoreagg = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};";
                            sw.WriteLine(valoreagg.PadRight(500) + "##");
                        }   
                    }
                    else
                    {
                        if (n == 11)
                        {
                            valoreagg = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};{r.Next(10, 21)};0;";
                            sw.WriteLine(valoreagg);
                        }
                        else
                        {
                            valoreagg = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};";
                            sw.WriteLine(valoreagg);
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
        // Modificare un record;
        public void Modifica(int nome, int mun, string znurb, string rione, string quartiere, string suburb, string znagro, string borgo, string exmun, string etichetta, string path, string pathTEMP)
        {
            int n = NumeroCampi(path);
            string stringains;
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
                        if (n == 11)
                        {
                            stringains = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};{dati[9]};0;";
                            if (linea.Contains("##"))
                            {
                                if (dati[10] == "0")
                                {
                                    if (int.Parse(dati[0]) == nome)
                                        sw.WriteLine(stringains.PadRight(500) + "##");
                                    else
                                        sw.WriteLine(linea);
                                }
                            }
                            else
                            {
                                if (dati[10] == "0")
                                {
                                    if (int.Parse(dati[0]) == nome)
                                        sw.WriteLine(stringains);
                                    else
                                        sw.WriteLine(linea);
                                }
                            }
                        }
                        else
                        {
                            stringains = $"{mun};{znurb};{rione};{quartiere};{suburb};{znagro};{borgo};{exmun};{etichetta};";
                            if (linea.Contains("##"))
                            {
                                if (int.Parse(dati[0]) == nome)
                                    sw.WriteLine(stringains.PadRight(500) + "##");
                                else
                                    sw.WriteLine(linea.PadRight(500) + "##");
                            }
                            else
                            {
                                if (int.Parse(dati[0]) == nome)
                                    sw.WriteLine(stringains);
                                else
                                    sw.WriteLine(linea);
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
        public void Fnz(string path, string pathTEMP)
        {
            int n = NumeroCampi(path);
            if (n < 9)
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string linea;
                    bool primaLinea = false;
                    using (StreamWriter sw = new StreamWriter(pathTEMP, append: true))
                    {
                        while ((linea = sr.ReadLine()) != null)
                        {
                            if (!primaLinea)
                            {
                                linea += ";";
                                primaLinea = !primaLinea;
                            }
                            else
                                linea += ";";
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
        }
    }
}
