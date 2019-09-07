using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(ControllerViewModel))]
    public class ControllerViewModel : Screen
    {
        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private readonly IHostedScriptExecutor scriptExecutor;

        private readonly ITrainingSession trainingSession;

        private string backgroundImage = string.Empty;

        private ScriptCollectionViewModel selectedCollection;

        public ControllerViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            this.trainingSession = trainingSession;
            this.scriptExecutor = scriptExecutor;

            BackgroundImage = @"../Resources/autotrainer.jpg";

            LoadScripts();
            SelectedCollection = ScriptCollections.FirstOrDefault();
        }

        public ObservableCollection<ScriptCollectionViewModel> ScriptCollections { get; private set; }

        public string BackgroundImage
        {
            get => backgroundImage;
            private set
            {
                backgroundImage = value;
                NotifyOfPropertyChange(() => BackgroundImage);
            }
        }

        public ScriptCollectionViewModel SelectedCollection
        {
            get => selectedCollection;
            set
            {
                selectedCollection = value;
                NotifyOfPropertyChange(() => SelectedCollection);
            }
        }


        private void LoadScripts()
        {
            // todo put this in settings
            var scriptDirectory = @"./content";
            ScriptCollections = new ObservableCollection<ScriptCollectionViewModel>();

            try
            {
                var directories = Directory.GetDirectories(scriptDirectory);
                directories.Each(f => ScriptCollections.Add(new ScriptCollectionViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Exception thrown reading " + scriptDirectory);
            }
        }
    }
}