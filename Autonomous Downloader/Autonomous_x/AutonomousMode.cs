using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous_x
{
    public class AutonomousMode
    {
        public String Name { get; set; }
        public ObservableCollection<Command> Commands = new ObservableCollection<Command>();

        public AutonomousMode(String name)
        {
            Name = name;
        }
    }
}
