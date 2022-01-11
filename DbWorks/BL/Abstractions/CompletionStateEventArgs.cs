using System;

namespace BL.Abstractions
{
    public class CompletionStateEventArgs
    {
        public CompletionState CompletionState { get; }
        
        public string FileName { get; }

        public CompletionStateEventArgs(CompletionState completionState, string fileName)
        {
            Verify(fileName);

            CompletionState = completionState;
            FileName = fileName;
        }

        private static void Verify(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
        }
    }
}