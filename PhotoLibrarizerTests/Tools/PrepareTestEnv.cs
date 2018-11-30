using System;
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

        public void Download()
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(myStringWebResource, fileName);
        }
    
    }
}
