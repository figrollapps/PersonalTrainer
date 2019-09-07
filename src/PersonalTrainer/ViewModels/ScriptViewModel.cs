using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptViewModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private string scriptDescription;
        private string scriptFileName;
        private string scriptName;

        public ScriptViewModel(string scriptName, string scriptFileName)
        {
            this.scriptName = scriptName;
            this.scriptFileName = scriptFileName;

            try
            {
                File.ReadAllLines(scriptFileName)
                    .SkipWhile(line => line.StartsWith("#load") || string.IsNullOrWhiteSpace(line))
                    .TakeWhile(line => line.StartsWith("//"))
                    .Each(x => { scriptDescription += x.Remove(0, 2).Trim() + Environment.NewLine; });

                if (!string.IsNullOrEmpty(scriptDescription))
                    scriptDescription = scriptDescription.Remove(scriptDescription.Length - 2, 2);
            }
            catch (Exception e)
            {
                scriptDescription = string.Empty;
                logger.Fatal(e, "Exception thrown reading " + scriptFileName);
            }
        }

        public string ScriptName
        {
            get => scriptName;
            set
            {
                scriptName = value;
                NotifyOfPropertyChange(() => ScriptName);
            }
        }

        public string ScriptFileName
        {
            get => scriptFileName;
            set
            {
                scriptFileName = value;
                NotifyOfPropertyChange(() => ScriptFileName);
            }
        }

        public string ScriptDescription
        {
            get => scriptDescription;
            set
            {
                scriptDescription = value;
                NotifyOfPropertyChange(() => ScriptDescription);
            }
        }
    }
}