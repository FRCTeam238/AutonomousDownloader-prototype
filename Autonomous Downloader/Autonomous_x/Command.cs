using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous_x
{
    public class Command
    {
        public String Name { get; set; }
        public ObservableCollection<String> Parameters { get; set; }

        public Command(String name, int numberOfParameters)
        {
            Name = name;

            Parameters = new ObservableCollection<String>();
            for (int count = 0; count < numberOfParameters; count++)
            {
                Parameters.Add("");
            }
        }
    }
}
