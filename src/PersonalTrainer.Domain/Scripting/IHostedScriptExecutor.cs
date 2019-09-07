using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public interface IHostedScriptExecutor
    {
        ScriptResult Result { get; }
        void Execute(string scriptFile);
        void ExecuteScript(string script);
    }
}