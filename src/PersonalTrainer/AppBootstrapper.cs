using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Metro;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Beats;
using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Timer;
using Figroll.PersonalTrainer.Domain.Utilities;
using Figroll.PersonalTrainer.Domain.Voice;
using Figroll.PersonalTrainer.ViewModels;
using ScriptCs.Contracts;
using StructureMap;

namespace Figroll.PersonalTrainer
{
    public class AppBootstrapper : CaliburnMetroCompositionBootstrapper<AppViewModel>
    {
        private IContainer container;
        private UserSettings userSettings;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            LoadUserSettings();

            container = new Container(x =>
            {
                x.For<IWindowManager>().Singleton().Use<AppWindowManager>();

                x.For<ITrainingSession>().Singleton().Use<TrainingSession>();
                x.Forward<ITrainingSession, IScriptPackContext>();

                x.For<IUserSettings>().Singleton().Use(userSettings);
                x.For<ITrainer>().Use<Trainer>();
                x.For<ITimer>().Use<SessionTimer>();
                x.For<IRandomNumberGenerator>().Use<RandomNumberGenerator>();
                x.For<IMetronome>().Use<Metronome>();
                x.For<ISequencer>().Use<Sequencer>();
                x.For<IContentCollection>().Singleton().Use<ContentCollection>();
                x.For<IContentViewer>().Singleton().Use<ContentPlayer>();

                x.For<IScriptPack>().Singleton().Use<PersonalTrainerScriptPack>();
                x.For<IHostedScriptExecutor>().Use<HostedScriptExecutor>();
            });
        }

        private void LoadUserSettings()
        {
            var simpleYamlSerialiser = new SimpleYamlSerialiser<UserSettings>();
            userSettings = simpleYamlSerialiser.Load(Constants.SettingsFileName);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key)
                ? container.GetInstance(serviceType)
                : container.GetInstance(serviceType ?? typeof(object), key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}