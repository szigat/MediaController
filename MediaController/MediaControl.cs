using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaController
{
    public static class MediaControl
    {
        public static void StartPause()
        {

            keybd_event(0xB3, 0x45, 0, IntPtr.Zero);

        }

        public static void Next()
        {

            //SendMessage(new IntPtr(spotifyMainWindow), 0x0319, new IntPtr(0), new IntPtr(720896));
            keybd_event(0xB0, 0x45, 0, IntPtr.Zero);

        }

        public static void Previous()
        {
            int spotifyMainWindow = FindWindow("SpotifyMainWindow", null);
            if (spotifyMainWindow > 0)
            {
                //SendMessage(new IntPtr(spotifyMainWindow), 0x0319, new IntPtr(0), new IntPtr(786432));
                keybd_event(0xB1, 0x45, 0, IntPtr.Zero);
            }
        }

        public static void Mute()
        {
            keybd_event(0xAD, 0x45, 0, IntPtr.Zero);
        }

        public static string GetSpotifyTrackInfo()
        {
            var proc = System.Diagnostics.Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            if (proc == null)
            {
                return "Spotify is not running!";
            }

            if (string.Equals(proc.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
            {
                return "No track is playing";
            }

            return proc.MainWindowTitle;
        }

        [DllImport("User32.Dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte vkCode, byte scanCode, int flags, IntPtr extraInfo);
    }
}
