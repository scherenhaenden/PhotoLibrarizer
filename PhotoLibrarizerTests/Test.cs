using NUnit.Framework;
using PhotoLibrarizerTests.Tools;
using System;
namespace PhotoLibrarizerTests
{
    [TestFixture()]
    public class Test
    {
        public Test()
        {
            new DownloadTestFile().Run();
        }


        [Test()]
        public void TestCase()
        {
        }
    }
}
