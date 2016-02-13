using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous
{
    public class Program
    {
        private String mName = "unnamed";
        private ObservableCollection<Command> mCommands = new ObservableCollection<Command>();

        [JsonProperty("Name")]
        public String Name
        {
            get
            {
                return mName;
            }
            set 
            {
                mName = value;
            }
        }

        public ObservableCollection<Command> Commands
        {
            get
            {
                return mCommands;
            }
            set
            {
                if (value == null)
                {
                    Commands = new ObservableCollection<Command>();
                }
                else
                {
                    mCommands = value;
                }
            }
        }

        public Program()
        {
            // do nothing
        }

        public Program(String name)
        {
            Name = name;
        }

        public void Add(Command command)
        {
            Commands.Add(command);
        }

        public Command Get(int index)
        {
            return Commands[index];
        }
    }
}
