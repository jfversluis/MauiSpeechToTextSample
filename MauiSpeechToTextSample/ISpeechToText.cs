using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSpeechToTextSample
{
    public interface ISpeechToText
    {
        Task<bool> RequestPermissions();

        Task<string> Listen(CultureInfo culture,
            IProgress<string> recognitionResult,
            CancellationToken cancellationToken);
    }
}
