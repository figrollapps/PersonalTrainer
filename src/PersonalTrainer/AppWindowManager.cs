using System.ComponentModel.Composition;
using Caliburn.Metro.Core;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace Figroll.PersonalTrainer
{
    [Export(typeof(IWindowManager))]
    public class AppWindowManager : MetroWindowManager
    {
        public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
        {
            if (windowIsView)
            {
                return view as PersonalTrainerWindow;
            }

            return new PersonalTrainerWindow
            {
                Content = view
            };
        }
    }
}