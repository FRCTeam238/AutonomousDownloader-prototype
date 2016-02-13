using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autonomous_Downloader.Autonomous
{
    public class Command
    {
        private CommandStateTemplate mCommandTemplate;

        private String mName = "";
        private String mJavaClassName = "";

        public String Name
        {
            get
            {
                //return mCommandTemplate.Name;
                return mName;
            }

            set
            {
                mName = value;
            }
        }

        public String JavaClassName
        {
            get
            {
                //return mCommandTemplate.JavaClassName;
                return mJavaClassName;
            }

            set
            {
                mJavaClassName = value;
            }
        }

        public int Parameter
        {
            get;
            set;
        }

        public Command()
        {
            // do nothing
        }

        public Command(CommandStateTemplate commandTemplate)
        {
            mCommandTemplate = commandTemplate;
            mName = mCommandTemplate.Name;
            mJavaClassName = mCommandTemplate.JavaClassName;
        }
    }
}
