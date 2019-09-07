using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Voice;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(SessionViewModel))]
    public sealed class SessionViewModel : Screen
    {
        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private readonly Dispatcher dispatcher;
        private readonly ITrainingSession trainingSession;
        private readonly IHostedScriptExecutor scriptExecutor;
        private readonly string scriptFile;
        private string imageLocation = string.Empty;
        private string subtitle;

        public SessionViewModel(Dispatcher dispatcher, ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor,
            string scriptFile)
        {
            this.dispatcher = dispatcher;
            this.trainingSession = trainingSession;
            this.scriptExecutor = scriptExecutor;
            this.scriptFile = scriptFile;

            this.trainingSession.Trainer.Spoke += TrainerOnSpoke;
            this.trainingSession.Viewer.PictureChanged += ViewerOnPictureChanged;
        }

        public string ImageLocation
        {
            get => imageLocation;
            private set
            {
                imageLocation = value;
                NotifyOfPropertyChange(() => ImageLocation);
            }
        }

        public string Subtitle
        {
            get => subtitle;
            set
            {
                subtitle = value;
                NotifyOfPropertyChange(() => Subtitle);
            }
        }

        private void ViewerOnPictureChanged(object sender, PictureEventArgs eventArgs)
        {
            dispatcher.Invoke(() => { ImageLocation = eventArgs.Picture.Fullpath; });
        }

        protected override void OnViewLoaded(object view)
        {
            Task.Factory.StartNew(() => scriptExecutor.Execute(scriptFile)).ContinueWith(_ => OnScriptCompleted());
        }

        private void TrainerOnSpoke(object sender, SpokeEventArgs spokeEventArgs)
        {
            dispatcher.Invoke(() => { Subtitle = spokeEventArgs.Words; });
        }

        public event EventHandler<ScriptResultEventArgs> ScriptCompleted;

        private void OnScriptCompleted()
        {
            trainingSession.Trainer.Spoke -= TrainerOnSpoke;
            trainingSession.Viewer.PictureChanged -= ViewerOnPictureChanged;

            ScriptCompleted?.Invoke(this, new ScriptResultEventArgs(scriptExecutor.Result));

            trainingSession.Dispose();
        }
    }
}