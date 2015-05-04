using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SingWithGreatnessWeb
{
    public class Mixer : System.Web.UI.Page
    {

        private List<WaveFileReader> toMix;
        private String targetFile; 

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Stream MixAudio(Dictionary<String,int[]> songsXtimes)
        {
            toMix = new List<WaveFileReader>();
            WaveMixerStream32 combined = null;
            foreach (String s in songsXtimes.Keys){
                PrepareClip(s, songsXtimes[s]);
                //chop it up and add it to toMix
                combined = Combine();
            }

            return combined;
        }

        private void PrepareClip(String song, int[] times)
        {
            Mp3ToWav(song, song + "wav");
            WaveFileReader reader = new WaveFileReader(song+"wav");
            WaveFileWriter writer = new WaveFileWriter(song+"mix", reader.WaveFormat);
            for (int i = 0; i < times.Length;i+=2 )
            {
                TrimWavFile(reader, writer, new TimeSpan(0,0,times[i]), new TimeSpan(0,0,times[i + 1]));
                if (i + 2 < times.Length)
                {
                    double silence = (times[i+2] - times[i+1])*1000;

                    InsertSilence(writer, silence);
                }
            }
            writer.Close();
            reader.Close();
            


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


            FileStream output= new FileStream(targetFile,FileMode.OpenOrCreate);
            foreach (WaveFileReader reader in toMix )
            {

                wavMix.AddInputStream(reader);
                
            }
            return wavMix;
        }

        public static void TrimWavFile(WaveFileReader r, WaveFileWriter w, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            using (WaveFileReader reader = r)
            {
                using (WaveFileWriter writer = w)
                {
                    int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPos = (int)reader.Length - endBytes;

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
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
                        writer.WriteData(buffer, 0, bytesRead);
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
    }
}