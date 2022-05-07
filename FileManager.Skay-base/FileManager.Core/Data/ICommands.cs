using System;

namespace FileManager.Core.Data
{
    public interface ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }
        void Execute();
    }
}