namespace BL.Abstractions
{
    public class CompletionStateEventArgs
    {
        public CompletionState CompletionState { get; }
        
        public string FileName { get; }

        public CompletionStateEventArgs(CompletionState completionState, string fileName)
        {
            CompletionState = completionState;
            FileName = fileName;
        }
    }
}