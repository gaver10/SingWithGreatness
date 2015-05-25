using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SingWithGreatnessWeb
{
    public partial class Mixer : System.Web.UI.Page
    {
        private List<WaveFileReader> toMix;
        private String targetFile = "C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\output";
        private AudioFileReader audio;
        private static WaveOut player = new WaveOut(WaveCallbackInfo.FunctionCallback());

        // key = track #, value = # of sections
        private Dictionary<int, int> trackCounts = new Dictionary<int, int>();


        protected void Page_Load(object sender, EventArgs e)
        {
            trackCounts.Add(1, 2);
            trackCounts.Add(2, 2);
        }

        public void MixAudio(Dictionary<String,int[]> songsXtimes)
        {
            toMix = new List<WaveFileReader>();
            WaveMixerStream32 combined = null;
            foreach (String s in songsXtimes.Keys){
                PrepareClip(s, songsXtimes[s]);
                //chop it up and add it to toMix
                
            }
            combined = Combine();

            
            Wave32To16Stream wavmixer = new Wave32To16Stream(combined);
            WaveFileWriter.CreateWaveFile(targetFile, wavmixer);
            wavmixer.Dispose();
            combined.Dispose();
            /*
            byte[] b = new byte[combined.Length];
            int len = (int)combined.Length;
            combined.Read(b,0,len);
            combined.Close();
            WaveFileWriter wv = new WaveFileWriter(targetFile, combined.WaveFormat);
            wv.Write(b, 0, (int)combined.Length);
             * */
            return;
        }

        private void PrepareClip(String song, int[] times)
        {
            Mp3ToWav(song, song + "wav");
            WaveFileReader reader = new WaveFileReader(song+"wav");
            WaveFileWriter writer = new WaveFileWriter(song+"mix", reader.WaveFormat);
            TimeSpan TotalTime = reader.TotalTime;
            int audlen = (int)TotalTime.TotalSeconds;

            for (int i = 0; i < times.Length;i+=2 )
            {
                if (i == 0 && times[i] > 0)
                {
                    double silence = times[i] * 1000;
                    InsertSilence(writer, silence);

                }
                TrimWavFile(reader, writer, new TimeSpan(0,0,times[i]), new TimeSpan(0,0,audlen-times[i+1]));
                if (i + 2 < times.Length)
                {
                    double silence = (times[i+2] - times[i+1])*1000;
                    

                    InsertSilence(writer, silence);
                }
            }
            writer.Close();
            reader.Close();
            toMix.Add(new WaveFileReader(song + "mix"));
            


        }

        private void InsertSilence(WaveFileWriter writer, Double milliseconds)
        {

            Double avgBytesPerMillisecond = (Double)writer.WaveFormat.AverageBytesPerSecond / 1000F;

            Int32 silenceSize = (Int32)(milliseconds * avgBytesPerMillisecond);
            //fileIn thing?
            silenceSize = silenceSize - silenceSize % writer.WaveFormat.BlockAlign;

            Byte[] silenceArray = new Byte[silenceSize];
            
            writer.Write(silenceArray, 0, silenceArray.Length);

        }


        public WaveMixerStream32 Combine()
        {

            WaveMixerStream32 wavMix = new WaveMixerStream32();
            wavMix.AutoStop = true;

            
            foreach (WaveFileReader reader in toMix )
            {
                WaveChannel32 wc3 = new WaveChannel32(reader);
                wavMix.AddInputStream(wc3);
                
                
            }
            wavMix.Position = 0;
            return wavMix;
        }

        public static void TrimWavFile(WaveFileReader r, WaveFileWriter w, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            WaveFileReader reader = r;

            WaveFileWriter writer = w;
                
            int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

            int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
            startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

            int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
            endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
            int endPos = (int)reader.Length - endBytes;

            TrimWavFile(reader, writer, startPos, endPos);
             
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public static void Mp3ToWav(string mp3File, string outputFile)
        {
            using (Mp3FileReader reader = new Mp3FileReader(mp3File))
            {
                WaveFileWriter.CreateWaveFile(outputFile, reader);
            }
        }

        protected void playButton_Click(object sender, EventArgs e)
        {

          
            
            audio = new AudioFileReader(targetFile);
            player.Init(audio);
            player.Play();
            
            
        }


        protected void stopButton_Click(object sender, EventArgs e)
        {
           
            player.Stop();
            player.Dispose();
        }

        protected void mixButton_Click(object sender, EventArgs e)
        {
            //completeLabel.Visible = false;
            
            Dictionary<String, int[]> dict = new Dictionary<String, int[]>();
            //dict.Add("C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\one", new int[] { Convert.ToInt32(track1Start1.Text), Convert.ToInt32(track1End1.Text), Convert.ToInt32(track1Start2.Text), Convert.ToInt32(track1End2.Text) });
            //dict.Add("C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\two", new int[] { Convert.ToInt32(track2Start1.Text), Convert.ToInt32(track2End1.Text), Convert.ToInt32(track2Start2.Text), Convert.ToInt32(track2End2.Text) });
            Mixer mix = new Mixer();
            mix.MixAudio(dict);

           

            //completeLabel.Visible = true;
        }

        protected void addTrack1Button_Click(object sender, EventArgs e)
        {
            AddSections(1);
        }

        protected void addTrack2Button_Click(object sender, EventArgs e)
        {
            AddSections(2);
        }

        protected void addTrackButton_Click(object sender, EventArgs e)
        {

            int trackNumber = trackCounts.Keys.Count + 1;
            trackCounts.Add(trackNumber, 2);

            Panel newTrackPanel = new Panel();
            newTrackPanel.ID = "track" + trackNumber.ToString() + "Panel";

            Label trackLabel = new Label();
            trackLabel.ID = "track" + trackNumber.ToString() + "Label";
            trackLabel.Text = "Track " + trackNumber.ToString();

            Label startLabel1 = new Label();
            startLabel1.Text = "Start: ";
            TextBox startTextbox1 = new TextBox();
            startTextbox1.ID = "track" + trackNumber.ToString() + "Start1Textbox";

            Label endLabel1 = new Label();
            endLabel1.Text = "End: ";
            TextBox endTextbox1 = new TextBox();
            endTextbox1.ID = "track" + trackNumber.ToString() + "End1Textbox";

            Label startLabel2 = new Label();
            startLabel2.Text = "Start: ";
            TextBox startTextbox2 = new TextBox();
            startTextbox2.ID = "track" + trackNumber.ToString() + "Start2Textbox";

            Label endLabel2 = new Label();
            endLabel2.Text = "End: ";
            TextBox endTextbox2 = new TextBox();
            endTextbox2.ID = "track" + trackNumber.ToString() + "End2Textbox";

            Button addSectionsButton = new Button();
            addSectionsButton.ID = "addTrack" + trackNumber.ToString() + "Button";
            addSectionsButton.Text = "Add";
            // set up button click later because its buggy and I don't know how


            newTrackPanel.Controls.Add(new LiteralControl("<br/><br/>")); 
            newTrackPanel.Controls.Add(trackLabel);
            newTrackPanel.Controls.Add(new LiteralControl("<br/><br/>"));
            newTrackPanel.Controls.Add(startLabel1);
            newTrackPanel.Controls.Add(new LiteralControl("&nbsp;"));
            newTrackPanel.Controls.Add(startTextbox1);
            newTrackPanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
            newTrackPanel.Controls.Add(endLabel1);
            newTrackPanel.Controls.Add(endTextbox1);
            newTrackPanel.Controls.Add(new LiteralControl("<br/><br/>"));
            newTrackPanel.Controls.Add(startLabel2);
            newTrackPanel.Controls.Add(new LiteralControl("&nbsp;"));
            newTrackPanel.Controls.Add(startTextbox2);
            newTrackPanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
            newTrackPanel.Controls.Add(endLabel2);
            newTrackPanel.Controls.Add(new LiteralControl("&nbsp;"));
            newTrackPanel.Controls.Add(endTextbox2);

            //trackPanel.Controls.Add(newTrackPanel);
            //trackPanel.Controls.Add(addSectionsButton);
        }

        protected void AddSections(int trackNumber)
        {
            int sectionNumber = trackCounts[trackNumber] + 1;
            trackCounts[trackNumber] = sectionNumber;
            
            Label startLabel = new Label();
            startLabel.Text = "Start: ";
            TextBox startTextbox = new TextBox();
            startTextbox.ID = "track" + trackNumber.ToString() + "Start" + sectionNumber.ToString() + "Textbox";

            Label endLabel = new Label();
            endLabel.Text = "End: ";
            TextBox endTextbox = new TextBox();
            endTextbox.ID = "track" + trackNumber.ToString() + "End" + sectionNumber.ToString() + "Textbox";

            //Panel panel = (Panel)trackPanel.FindControl("track" + trackNumber.ToString() + "Panel");
            //panel.Controls.Add(new LiteralControl("<br/><br/>"));
            //panel.Controls.Add(startLabel);
            //panel.Controls.Add(new LiteralControl("&nbsp;"));
            //panel.Controls.Add(startTextbox);
            //panel.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
            //panel.Controls.Add(endLabel);
            //panel.Controls.Add(new LiteralControl("&nbsp;"));
            //panel.Controls.Add(endTextbox);
        }
    }
}