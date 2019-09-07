using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptCollectionViewModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private string collectionFolderName;

        private string collectionName;
        private ScriptViewModel selectedScript;


        public ScriptCollectionViewModel(string collectionName, string collectionFolderName)
        {
            this.collectionName = collectionName;
            this.collectionFolderName = collectionFolderName;

            LoadScripts();
            SelectedScript = Scripts.FirstOrDefault();
        }

        public ObservableCollection<ScriptViewModel> Scripts { get; private set; }

        public string CollectionName
        {
            get => collectionName;
            set
            {
                collectionName = value;
                NotifyOfPropertyChange(() => CollectionName);
            }
        }

        public string CollectionFolderName
        {
            get => collectionFolderName;
            set
            {
                collectionFolderName = value;
                NotifyOfPropertyChange(() => CollectionFolderName);
            }
        }

        public ScriptViewModel SelectedScript
        {
            get => selectedScript;
            set
            {
                selectedScript = value;
                NotifyOfPropertyChange(() => SelectedScript);
            }
        }

        private void LoadScripts()
        {
            Scripts = new ObservableCollection<ScriptViewModel>();

            try
            {
                var directories = Directory.GetFiles(collectionFolderName, "*.csx");
                directories.Each(f => Scripts.Add(new ScriptViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Exception thrown reading " + collectionFolderName);
            }
        }
    }
}