using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Scripting;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : Conductor<Screen>.Collection.OneActive, IHaveDisplayName, IViewAware
    {
        public enum AutoTrainerModes
        {
            Controller,
            ActiveSession
        }

        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private readonly ControllerViewModel controller;
        private readonly IHostedScriptExecutor scriptExecutor;
        private readonly ITrainingSession trainingSession;
        private AutoTrainerModes autoTrainerMode;
        private string displayName = Constants.PersonalTrainerTitle;
        private string errorText;
        private SessionViewModel session;

        public AppViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            this.trainingSession = trainingSession;
            this.scriptExecutor = scriptExecutor;

            controller = new ControllerViewModel(trainingSession, scriptExecutor);
            SetControlPanelState();
        }

        public bool IsSessionActive => AutoTrainerMode == AutoTrainerModes.ActiveSession;

        public string ErrorText
        {
            get => errorText;
            set
            {
                errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        public AutoTrainerModes AutoTrainerMode
        {
            get => autoTrainerMode;
            set
            {
                autoTrainerMode = value;
                NotifyOfPropertyChange(() => AutoTrainerMode);
                NotifyOfPropertyChange(() => IsSessionActive);
            }
        }

        public override string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            ActivateItem(controller);
        }

        private void SetControlPanelState()
        {
            AutoTrainerMode = AutoTrainerModes.Controller;
        }

        private void SetSessionState()
        {
            AutoTrainerMode = AutoTrainerModes.ActiveSession;
            ErrorText = string.Empty;
        }

        public void UserAction()
        {
            switch (AutoTrainerMode)
            {
                case AutoTrainerModes.Controller:
                    logger.Info("User started session");
                    StartSession();
                    break;
                case AutoTrainerModes.ActiveSession:
                    // Button invisible at this point.
                    // todo Add a global "TAP OUT!" button.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartSession()
        {
            SetSessionState();

            session = new SessionViewModel(Application.Current.Dispatcher, trainingSession, scriptExecutor,
                controller.SelectedCollection.SelectedScript.ScriptFileName);
            session.ScriptCompleted += OnScriptCompleted;

            ActivateItem(session);
        }

        private void OnScriptCompleted(object sender, ScriptResultEventArgs args)
        {
            EndSession();

            if (args.Result.CompileExceptionInfo != null)
                ErrorText = args.Result.CompileExceptionInfo.SourceException.Message;
            else if (args.Result.ExecuteExceptionInfo != null)
                ErrorText = args.Result.ExecuteExceptionInfo.SourceException.Message;
        }

        private void EndSession()
        {
            session.ScriptCompleted -= OnScriptCompleted;

            if (session.IsActive)
                DeactivateItem(session, true);

            SetControlPanelState();
            ActivateItem(controller);
        }
    }
}