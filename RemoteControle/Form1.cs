using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;

namespace RemoteControle
{
    public partial class Console : Form
    {
        Size _actual_for_size;
        int SessionUser;
        int NextContent;
        public Console()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
           
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 3:
                   // _player_is_playing = true;

                    break;
                case 8:
                    ;//SETTA IL CONTENUTO CONSECUTIVO
                    this.BeginInvoke(new Action(() => this.SetNewContent()));
                    this.BeginInvoke(new Action(() => this.mainCycle())); //SENZA QUESTA RIGA NON FUNZIONA. IN SOSTANZA EFFETTUA UNA CHIAMATA 
                                                                          // ASINCRONA. SENZA SI BLOCCA TUTTO, PERCHE' C'E' QUALCHE PROBLEMA CON
                                                                          // IL RILASCIO DELL'URL DEL VIDEO O IL RICARICAMENTO DEL NUOVO (O ANCHE VECCHIO)      
                    break;
                default:
                    
                    break;

            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            if (setUpConsole())
            {
                               
                mainCycle();
            }


        }
        private static List<T> _download_serialized_json_data<T>(string url) where T:new()
        {
            //using (System.Net.WebClient wc = new System.Net.WebClient())
            //{
                string json_dati = string.Empty;
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.ServicePoint.ConnectionLimit = 1000000000; //QUESTA RIGA FA FUNZIONARE TUTTO, ALTRIMENTI HTTP 1.1 PREVEDE AL MASSIMO 2 CONNESSIONI 
                                                             // E NON SI RIESCE A FARE UN DISPOSE   
                System.IO.StreamReader rawJson;
                try
                {
                  
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        using (rawJson = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            var rawJson_ = rawJson.ReadToEnd();
                            
                            if (rawJson_.Length > 0)
                            {
                                List<T> jsongood = JsonConvert.DeserializeObject<List<T>>(rawJson_);
                                rawJson_ = null;
                                return jsongood;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    //response.Close();
                    //response.Dispose();
                    
                }
                catch(System.Net.WebException wex)
                {
                   

                    System.Console.WriteLine("Exc msg : " + wex.Message);
                    return null;
                }
                
                
                
                //return !string.IsNullOrEmpty(rawJson) ? JsonConvert.DeserializeObject<T>(rawJson) : new T();

            //}
        }
        private bool setUpConsole()
        {
            try
            {
                
                _actual_for_size = this.Size;
                axWindowsMediaPlayer1.Height = _actual_for_size.Height - 100;
                axWindowsMediaPlayer1.Width = _actual_for_size.Width-20;
                pictureBox1.Image = Image.FromFile(@"c:\panorama-toscana_qrcode.png");
                pictureBox1.Height = pictureBox1.Image.Height;
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Location = new Point(_actual_for_size.Width/2 - pictureBox1.Width/2, _actual_for_size.Height/2 - pictureBox1.Width/2);
                SessionUserLbl.Location = new Point(SessionUserLbl.Location.X, axWindowsMediaPlayer1.Height + 10);
                PostazioneLbl.Location = new Point(PostazioneLbl.Location.X, axWindowsMediaPlayer1.Height + 10);
                timeSpanLabel.Location = new Point(timeSpanLabel.Location.X, axWindowsMediaPlayer1.Height + 10);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async void mainCycle()
        {
            
            RemoteControlData remotecontroldata = getRemoteData();

            if (remotecontroldata != null)
            {
                SessionUser = (remotecontroldata.SessionUser != null) ? Int32.Parse(remotecontroldata.SessionUser) : 0;
                string ActualContent = remotecontroldata.ActualContent;
                int SessionModeStatus = (remotecontroldata.SessionModeStatus != null) ? Int32.Parse(remotecontroldata.SessionModeStatus) : 0;
                int SessionCode = (remotecontroldata.SessionCode != null) ? Int32.Parse(remotecontroldata.SessionCode) : 0;
                int ContentType = (remotecontroldata.ContentType != null) ? Int32.Parse(remotecontroldata.ContentType) : 0;
                int ContentTime = (remotecontroldata.ContentTime != null) ? Int32.Parse(remotecontroldata.ContentTime) : 0;
                NextContent = (remotecontroldata.NextContent != null) ? Int32.Parse(remotecontroldata.NextContent) : 0;
                DateTime timestamp= remotecontroldata.Timestamp;
                string ContentUrl = remotecontroldata.ContentUrl;

                if (SessionModeStatus == 1) //LA SESSIONE E' ATTIVA
                {
                    if (SessionUser != 0) //LA SESSIONE HA UN PROPRIETARIO
                    {
                        SessionUserLbl.Text = "SESSION USER: " + SessionUser.ToString();
                        string _contentUrl = @"c:\" + ContentUrl;
                        //CONTROLLA SESSIONE SCADUTA. SE SI METTI QUESTA A ZERO
                        TimeSpan span = DateTime.Now - timestamp;
                        //TimeSpan retrocount = DateTime.
                        timeSpanLabel.Text = span.Minutes.ToString() + ":" + span.Seconds.ToString();
                        if ((span.Minutes*60*30)+span.Seconds>60)
                        {
                            TurnOffVisitSession(SessionUser);
                        }
                        if (ActualContent != null && File.Exists(_contentUrl))
                        {//SE ESISTE UN CONTENT E SE ESISTE IL FILE

                           switch (ContentType)
                            {
                                case 1:    //VIDEO
                                    pictureBox1.Visible = false;
                                    bool playingvideo = playVideo(_contentUrl);
                                    break;
                                case 2:     //IMMAGINE
                                    pictureBox1.Visible = true;
                                    pictureBox1.Image = Image.FromFile(_contentUrl);
                                    SetNewContent();
                                    await Task.Delay(TimeSpan.FromSeconds(ContentTime));
                                    
                                    mainCycle();
                                    
                                    break;

                            }

                            
                        }
                        else
                        {
                            await Task.Delay(TimeSpan.FromSeconds(0.5));
                            mainCycle();

                        }

                    }
                    else //LA SESSIONE NON HA UN PROPRIETARIO
                    {
                        SessionUserLbl.Text = "INSERISCI IL CODICE: " + SessionCode.ToString();
                        pictureBox1.Visible = true;
                        pictureBox1.Image = Image.FromFile(@"c:\panorama-toscana_qrcode.png");

                        await Task.Delay(TimeSpan.FromSeconds(0.5));
                        mainCycle();
                    }

                }
                else //quando incontra modestatus=0
                {
                    //GENERARE NUOVA RIGA DI SESSION
                    // CON codice nuovo ed utente = null
                    if (GenerateNewVisitSession())
                    {
                        mainCycle();
                    }
                    else
                    {
                        SessionUserLbl.Text = "ERRORE NELLA GENERAZIONE REMOTA DELLA SESSIONE";
                    }
                }
            }
            else
            { //QUANDO NON ESISTE PROPRIO LA RIGA
                if (GenerateNewVisitSession())
                {
                    mainCycle();
                }
                else
                {
                    SessionUserLbl.Text = "ERRORE NELLA GENERAZIONE REMOTA DELLA SESSIONE";
                }
            }
            
           
        }
            
        private bool playVideo( string url)
        {
            try
            {
                axWindowsMediaPlayer1.URL = url;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private RemoteControlData getRemoteData()
        {
            string url = @"http://demo.ggallery.it/GGARBACK/index.php?option=com_ggne&task=remotecontroller.getstatus&RemoteDeviceID=1";
            var remotecontroldata = _download_serialized_json_data<RemoteControlData>(url);
            if (remotecontroldata.Count>0)
            {
                return remotecontroldata[0];
            }
            else
            {
                return null;
            }
        }

        private bool GenerateNewVisitSession()
        {
            string url = @"http://demo.ggallery.it/GGARBACK/index.php?option=com_ggne&task=remotecontroller.generatenewvisitsession&RemoteDeviceID=1";
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            return (response != null) ? true : false;
        }

        private bool TurnOffVisitSession(int idutente)
        {
            string url = @"http://demo.ggallery.it/GGARBACK/index.php?option=com_ggne&task=remotecontroller.turnoffvisitsession&IdUtente="+ idutente.ToString()+"&RemoteDeviceID=1";
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            return (response != null) ? true : false;
        }

        private bool SetNewContent()//QUESTO E' IL METODO CHE SETTA IL CONTENUTO CONSECUTIVO
        {
            if (NextContent > 0) //SE NextContent è valorizzato, allora si cambia.
            {
                string url = @"http://demo.ggallery.it/GGARBACK/index.php?option=com_ggne&task=remotecontroller.updateactualcontent&RemoteDeviceID=1&IdUtente=" + SessionUser.ToString() + "&newContent=" + NextContent.ToString();
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                var response = (System.Net.HttpWebResponse)request.GetResponse();
                return (response != null) ? true : false;
            }
            else
            {
                return true;
            }
            
        }
    }


    public class RemoteControlData
    {
        public string SessionId;
        public string RemoteDeviceId;
        public string SessionModeStatus;
        public string SessionCode;
        public string SessionUser;
        public string ActualContent;
        public string ContentDescr;
        public string ContentType;
        public string ContentUrl;
        public string ContentTime;
        public string NextContent;
        public DateTime Timestamp;
    }
}
