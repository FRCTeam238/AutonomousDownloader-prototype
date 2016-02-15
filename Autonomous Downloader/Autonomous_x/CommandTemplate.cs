using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous_x
{
    class CommandTemplate
    {
        public String CommandName { get; set; }
        public int NumberOfParameters { get; set; }

        public CommandTemplate(String name, int numParameters)
        {
            CommandName = name;
            NumberOfParameters = numParameters;
        }

        public static CommandTemplate[] LoadCommandSet(String filepath)
        {
            CommandTemplate[] retval = null;

            using (StreamReader sr = new StreamReader(filepath))
            {
                String json;

                json = sr.ReadToEnd();
                retval = JsonConvert.DeserializeObject<CommandTemplate[]>(json);
            }

            return retval;
        }

        public static void SaveCommandSet(CommandTemplate[] commandSet, String filepath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    String json = JsonConvert.SerializeObject(commandSet, Formatting.Indented);
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
