using System;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(PinguinEducation.Droid.BridgeImplementation))]
namespace PinguinEducation.Droid
{
    class BridgeImplementation : IBridge
    {
        MainActivity act = new MainActivity();
        public void MuteMusic(bool mute)
        {
            act.Mute(mute);
        }
    }
}