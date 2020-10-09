using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;


namespace TSD
{
    class PlaySound
    {
        //public void PlaySound()
        //{
        //}

       // public string file_name = "";

        private enum Flags
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_MEMORY = 0x0004,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_ALIAS = 0x00010000,
            SND_ALIAS_ID = 0x00110000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int MobilePlaySound(string szSound, IntPtr hMod, int flags);

        public void PlaySound_WAV(string file_name)
        {
            if (File.Exists(file_name))
            {
                //MobilePlaySound(file_name, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
                MobilePlaySound(file_name, IntPtr.Zero, (int)(Flags.SND_SYNC | Flags.SND_FILENAME));
            }
        }
    }
}
