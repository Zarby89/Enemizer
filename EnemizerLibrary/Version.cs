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
        public const int BuildNumber = 20; // max 99 to show up in rom
        public static string CurrentVersion = $"{MajorVersion}.{MinorVersion}.{BuildNumber.ToString("D2")}";
        public static bool CheckUpdate()
        {
            
            string checkVersion = "";
            using (WebClient wc = new WebClient())
            {
                checkVersion = wc.DownloadString("https://zarby89.github.io/Enemizer/version.txt");
            }
            var numbers = checkVersion.Replace("\r", "").Replace("\n", "").Trim().Split('.');
            if(Int32.Parse(numbers[0]) >= MajorVersion)
            {
                if(Int32.Parse(numbers[1]) >= MinorVersion)
                {
                    if(Int32.Parse(numbers[2]) > BuildNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
