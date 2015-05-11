using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SingWithGreatnessWeb
{
    public partial class Mixer : System.Web.UI.Page
    {
        private List<WaveFileReader> toMix;
        private String targetFile = "C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\output";
        private AudioFileReader audio;
        private static WaveOut player = new WaveOut(WaveCallbackInfo.FunctionCallback());


        protected void Page_Load(object sender, EventArgs e)
        {
           
            
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
            completeLabel.Visible = false;
            
            Dictionary<String, int[]> dict = new Dictionary<String, int[]>();
            dict.Add("C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\one", new int[] { Convert.ToInt32(track1Start1.Text), Convert.ToInt32(track1End1.Text), Convert.ToInt32(track1Start2.Text), Convert.ToInt32(track1End2.Text) });
            dict.Add("C:\\Users\\Gavin\\Documents\\GitHub\\SingWithGreatness\\SingWithGreatness C#\\SingWithGreatnessWeb\\SingWithGreatnessWeb\\two", new int[] { Convert.ToInt32(track2Start1.Text), Convert.ToInt32(track2End1.Text), Convert.ToInt32(track2Start2.Text), Convert.ToInt32(track2End2.Text) });
            Mixer mix = new Mixer();
            mix.MixAudio(dict);

           

            completeLabel.Visible = true;
        }
    }
}