using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DirectumNetwork
{
    public class Network
    {
        #region Singletone
        private static Network _instance;

        public static Network GetInstance()
        {
            if (_instance != null) return _instance;
            return new Network();
        }

        #endregion

        private Random _random;
        private const int FilenameHashLength = 10;
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=<>?.,";

        private WebClient _webClient;
        private TaskCompletionSource<bool> _tcs;
        private Network()
        {
            _webClient = new WebClient();
            _random = new Random();
        }

        public async void Download(string uri, CancellationToken token)
        {
            await StartDownload(new Uri(uri));
            CompleteDownloading(token);
        }

        private Task<bool> StartDownload(Uri uri)
        {
            _tcs = new TaskCompletionSource<bool>();
            _webClient.DownloadFileAsync(uri, GetFileName());
            return _tcs.Task;
        }

        private void CompleteDownloading(CancellationToken token)
        {
            while (_webClient.IsBusy)
            {
                if (token.IsCancellationRequested)
                {
                    _tcs.TrySetResult(false);
                    return;
                }
            }

            _tcs.TrySetResult(true);
        }

        private string GetFileName()
        {
            string hash = new string(Enumerable.Repeat(_chars, FilenameHashLength).Select(s => s[_random.Next(s.Length)]).ToArray());
            return "file_" + hash;
        }

    }
}
