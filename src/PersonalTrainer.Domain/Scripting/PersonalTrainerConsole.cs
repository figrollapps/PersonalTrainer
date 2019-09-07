using System;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public class PersonalTrainerConsole : IConsole
    {
        public void Write(string value)
        {
        }

        public void WriteLine()
        {
        }

        public void WriteLine(string value)
        {
        }

        public string ReadLine(string prompt)
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
        }

        public void Exit()
        {
        }

        public void ResetColor()
        {
        }

        public ConsoleColor ForegroundColor { get; set; }
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public int Width { get; }
    }
}