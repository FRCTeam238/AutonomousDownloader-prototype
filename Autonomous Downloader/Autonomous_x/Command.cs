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
        private CommandTemplate mTemplate = null;

        [Newtonsoft.Json.JsonIgnore]
        public String NameExtended
        {
            get
            {
                String result = Name;

                for (int parameterIndex = 0; parameterIndex < Parameters.Count; parameterIndex++)
                {
                    if (String.IsNullOrEmpty(Parameters[parameterIndex]))
                    {
                        result += String.Format(" {0}", mTemplate.GetParameterName(parameterIndex));
                    }
                    else
                    {
                        result += String.Format(" '{0}'", Parameters[parameterIndex]);
                    }
                }

                    return result;
            }
        }

        public String Name 
        { 
            get
            {
                return mTemplate.CommandName;
            }
            /* set; */
        }

        public ObservableCollection<String> Parameters { get; set; }

        public Command(CommandTemplate baseTemplate)
        {
            mTemplate = baseTemplate;

            int numberOfParameters = baseTemplate.NumberOfParameters;
            Parameters = new ObservableCollection<String>();
            for (int count = 0; count < numberOfParameters; count++)
            {
                Parameters.Add("");
            }
        }
    }
}
