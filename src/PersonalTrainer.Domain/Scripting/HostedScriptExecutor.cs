using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Beats;
using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Timer;
using Figroll.PersonalTrainer.Domain.Utilities;
using Figroll.PersonalTrainer.Domain.Voice;
using ScriptCs;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HostedScriptExecutor : IHostedScriptExecutor
    {
        private readonly ScriptCsHost script;

        public HostedScriptExecutor(ITrainingSession session)
        {
            script = new ScriptCsHost();

            script.Root.Executor.Initialize(new List<string>(), new List<IScriptPack> {new PersonalTrainerScriptPack(session)});
            script.Root.Executor.AddReferenceAndImportNamespaces(new[]
            {
                typeof(TrainingSession),
                typeof(Metronome),
                typeof(Sequencer),
                typeof(Trainer),
                typeof(SessionTimer),
                typeof(Picture),
                typeof(IContentViewer),
                typeof(IContentCollection),
                typeof(PictureInfo),
                typeof(ExtensionMethods)
            });
        }

        public ScriptResult Result { get; private set; }

        public void Execute(string scriptFile)
        {
            Result = script.Root.Executor.Execute(scriptFile);
        }

        public void ExecuteScript(string script)
        {
            Result = this.script.Root.Executor.ExecuteScript(script);
        }
    }
}