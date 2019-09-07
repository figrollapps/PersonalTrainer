using System;
using ScriptCs;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScriptFileSystem : FileSystem
    {
        public override string BinFolder => AppDomain.CurrentDomain.BaseDirectory;
    }
}