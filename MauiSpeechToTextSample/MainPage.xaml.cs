using System.Globalization;

namespace MauiSpeechToTextSample;

public partial class MainPage : ContentPage
{
    private ISpeechToText speechToText;
    private CancellationTokenSource tokenSource = new CancellationTokenSource();

    public Command ListenCommand { get; set; }
    public Command ListenCancelCommand { get; set; }
    public string RecognitionText { get; set; }

    public MainPage(ISpeechToText speechToText)
	{
		InitializeComponent();

        this.speechToText = speechToText;

        ListenCommand = new Command(Listen);
		ListenCancelCommand = new Command(ListenCancel);
        BindingContext = this;
    }

    private async void Listen()
    {
        var isAuthorized = await speechToText.RequestPermissions();
        if (isAuthorized)
        {
            try
            {
                RecognitionText = await speechToText.Listen(CultureInfo.GetCultureInfo("en-us"),
                    new Progress<string>(partialText =>
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        RecognitionText = partialText;
                    }
                    else
                    {
                        RecognitionText += partialText + " ";
                    }

                    OnPropertyChanged(nameof(RecognitionText));
                }), tokenSource.Token);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            await DisplayAlert("Permission Error", "No microphone access", "OK");
        }
    }

    private void ListenCancel()
    {
        tokenSource?.Cancel();
    }
}

