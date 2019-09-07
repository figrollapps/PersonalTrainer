using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using NLog;

namespace Figroll.PersonalTrainer
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly Logger logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void UIOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Fatal("UI thread exception was unhandled and applicaton will close");
            HandleFatalException(e.Exception);

            MessageBox.Show("Your PERSONAL TRAINER session has been busted by the Feds on suspicion of performance enhancing drugs :-(" +
                            Environment.NewLine + "The indictment says " + e.Exception.Message, "Fatal Error");

            e.Handled = false;
        }

        private void HandleFatalException(Exception e)
        {
            logger.Fatal(e, e.StackTrace);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal("App domain exception was unhandled and applicaton will close");

            if (e.ExceptionObject is Exception exceptionObject)
            {
                HandleFatalException(exceptionObject);
            }
            else
            {
                // Non CLR exception.
                logger.Fatal("Exception object was null");
            }

            if (!e.IsTerminating)
            {
                Environment.Exit(1);
            }
        }
    }
}