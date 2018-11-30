using System;
using System.Net;

namespace PhotoLibrarizerTests.Tools
{
    public class DownloadTestFile
    {
        public DownloadTestFile()
        {
        }

        public void Run()
        {

        }

        private void Download(string url, string filename)
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, filename);
        }
    
    }
}
