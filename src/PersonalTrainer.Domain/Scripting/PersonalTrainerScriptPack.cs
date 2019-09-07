using Figroll.PersonalTrainer.Domain.API;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PersonalTrainerScriptPack : IScriptPack
    {
        private readonly ITrainingSession session;

        public PersonalTrainerScriptPack(ITrainingSession session)
        {
            this.session = session;
        }

        public void Initialize(IScriptPackSession scriptPackSession)
        {
        }

        public IScriptPackContext GetContext()
        {
            return session;
        }

        public void Terminate()
        {
            session.Dispose();
        }
    }
}