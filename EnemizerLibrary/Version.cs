using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace EnemizerLibrary
{
    public class Version
    {
        public const int MajorVersion = 6;
        public const int MinorVersion = 0;
        public static string CurrentVersion = $"{MajorVersion}.{MinorVersion}";
        public static bool CheckUpdate()
        {
            
            string checkVersion = "";
            using (WebClient wc = new WebClient())
            {
                checkVersion = wc.DownloadString("https://zarby89.github.io/Enimizer/version.txt");
            }
            if (!checkVersion.Contains(Version.CurrentVersion))
            {
                return true;
            }
            return false;
        }

    }
}
