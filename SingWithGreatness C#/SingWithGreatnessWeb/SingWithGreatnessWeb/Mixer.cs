using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAudio;
using NAudio.Wave;

namespace SingWithGreatnessWeb
{
    // this class uses code adapted from samples of the NAudio library

    public class Mixer : IDisposable
    {
        private IWavePlayer playbackDevice;
        private WaveStream fileStream;

        public event EventHandler<FftEventArgs> FftCalculated;

        private List<WaveStream> fileStreamList = new List<WaveStream>();
        private string blankAudioFile = "BlankAudio.mp3";
        private WaveStream blankStream;

        public Mixer()
        {

        }

        protected virtual void OnFftCalculated(FftEventArgs e)
        {
            EventHandler<FftEventArgs> handler = FftCalculated;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<MixerEventArgs> MaximumCalculated;

        protected virtual void OnMaximumCalculated(MixerEventArgs e)
        {
            EventHandler<MixerEventArgs> handler = MaximumCalculated;
            if (handler != null) handler(this, e);
        }

        public void Load(string fileName)
        {
            Stop();
            CloseFile();
            EnsureDeviceCreated();
            OpenFile(fileName);
        }

        private void CloseFile()
        {
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                var inputStream = new AudioFileReader(fileName);
                fileStream = inputStream;
                var aggregator = new Aggregator(inputStream);
                aggregator.NotificationCount = inputStream.WaveFormat.SampleRate / 100;
                aggregator.PerformFFT = true;
                aggregator.FftCalculated += (s, a) => OnFftCalculated(a);
                aggregator.MaximumCalculated += (s, a) => OnMaximumCalculated(a);
                playbackDevice.Init(aggregator);
            }
            catch (Exception e)
            {
                //need to implement logging
                //MessageBox.Show(e.Message, "Problem opening file");
                CloseFile();
            }
        }

        private void EnsureDeviceCreated()
        {
            if (playbackDevice == null)
            {
                CreateDevice();
            }
        }

        private void CreateDevice()
        {
            playbackDevice = new WaveOut { DesiredLatency = 200 };
        }

        public void Play()
        {
            if (playbackDevice != null && fileStream != null && playbackDevice.PlaybackState != PlaybackState.Playing)
            {
                playbackDevice.Play();
            }
        }

        public void Pause()
        {
            if (playbackDevice != null)
            {
                playbackDevice.Pause();
            }
        }

        public void Stop()
        {
            if (playbackDevice != null)
            {
                playbackDevice.Stop();
            }
            if (fileStream != null)
            {
                fileStream.Position = 0;
            }
        }

        public void Dispose()
        {
            Stop();
            CloseFile();
            if (playbackDevice != null)
            {
                playbackDevice.Dispose();
                playbackDevice = null;
            }
        }
    }
}