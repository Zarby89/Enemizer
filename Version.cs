using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Enemizer
{
    class Version
    {
        public static string CurrentVersion = "5.6";
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
