using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SingWithGreatnessWeb
{
    public class MixerEventArgs
    {
        [DebuggerStepThrough]
        public MixerEventArgs(float minValue, float maxValue)
        {
            this.Max = maxValue;
            this.Min = minValue;
        }
        public float Max { get; private set; }
        public float Min { get; private set; }
    }
}