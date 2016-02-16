using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous_x
{
    public class RouteGroup
    {
        public ObservableCollection<AutonomousRoute> AutonomousModes = new ObservableCollection<AutonomousRoute>();

        public static RouteGroup Load(String filepath)
        {
            RouteGroup retval = null;

            using (StreamReader sr = new StreamReader(filepath))
            {
                String json;

                json = sr.ReadToEnd();
                retval = JsonConvert.DeserializeObject<RouteGroup>(json);
            }

            return retval;
        }


        public void Save(String filepath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    String json = JsonConvert.SerializeObject(this, Formatting.Indented);
                    sw.Write(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
