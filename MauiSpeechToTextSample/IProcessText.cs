namespace MauiSpeechToTextSample
{
    public interface IProcessText
    {
        Task<bool> ProcessText(string inputText);
    }
}
