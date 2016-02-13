using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous
{
    public class CommandStateTemplate
    {
        public static List<CommandStateTemplate> SupportedCommands = new List<CommandStateTemplate>()
        {
            new CommandStateTemplate("LoadBin", "org.usfirst.frc.team238.autonomousStates.StateLoadBin"),
            new CommandStateTemplate("DriveForward", "org.usfirst.frc.team238.autonomousStates.StateDriveForward"),
            new CommandStateTemplate("Finished", "org.usfirst.frc.team238.autonomousStates.Finished")
        };

        public String Name { get; set; }
        public String JavaClassName { get; set; }

        public CommandStateTemplate(String name, String className)
        {
            Initialize(name, className);
        }

        private void Initialize(String name, String className)
        {
            Name = name;
            JavaClassName = className;
        }

        public Command CreateCommandInstance()
        {
            return new Command(this);
        }
    }
}
