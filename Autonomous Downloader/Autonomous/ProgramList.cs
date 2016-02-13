using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous
{
    public class ProgramList : ObservableCollection<Program>
    {
        public ProgramList()
        {
            // do nothing
        }

        public Program NewProgram(String name)
        {
            return new Program(name);
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

        public static ProgramList Load(String filepath)
        {
            ProgramList retval = null;

            using (StreamReader sr = new StreamReader(filepath))
            {
                String json;
                    
                json = sr.ReadToEnd();
                retval = JsonConvert.DeserializeObject<ProgramList>(json);
            }

            return retval;
        }

    }
}
