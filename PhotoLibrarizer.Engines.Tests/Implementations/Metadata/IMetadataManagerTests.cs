using System.Diagnostics;
using System.Reflection;
using ExifLibrary;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Tests.Arrange.Images;
using ExifTag = ImageMagick.ExifTag;

namespace PhotoLibrarizer.Engines.Tests.Implementations.Metadata;

    
    
    public class IMetadataManagerTests
    {
        public IMetadataManagerTests()
        {
            
        }
        public void ExampleMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            // Get calling method name - 1 for current method, 2 for caller
            if (stackFrames != null && stackFrames.Length > 2)
            {
                var callingMethod = stackFrames[2].GetMethod();
                string callingMethodName = callingMethod.Name;
                Console.WriteLine("Calling Method Name: " + callingMethodName);
            }
        }
        
        
        [Test]
        public void Test1_GetCameraCreationDatel()
        {
            ImageCreator imageCreator = new ImageCreator();
            
            // Get the current method name
            string methodName = MethodBase.GetCurrentMethod().Name;

            // Get the current class name
            string className = this.GetType().Name;

            string currentImage = "./" + className + "_" + methodName + ".jpg";

            imageCreator.CreateTestImage("./" + className + "_" + methodName + ".jpg");
            
            // Create or load an image file
            var datetimeNow = DateTime.Now;
            
            
            ImageFile image = ImageFile.FromFile(currentImage);
            
            // Create an EXIF tag (e.g., DateTimeOriginal)
            var exifTag = ExifTag.DateTimeOriginal;
            ExifDateTime dateTimeTag = new ExifDateTime(ExifLibrary.ExifTag.DateTimeOriginal, datetimeNow);

            image.Save(currentImage);

            
            
            IMetadataManager metadataManager = new MetadataManager(currentImage);
            //var camera = metadataManager.GetModelOfCamera();
            var creationDate = metadataManager.GetDateOfMediaCreation();

            var result = metadataManager.GetDateOfMediaCreation();

            Assert.IsNotNull(result);
        }
        
        
        [Test]
        public void Test21_GetCameraModel()
        {
            IMetadataManager metadataManager = new MetadataManager("/Users/edwardflores/Pictures/organized/2022/11/10/2022_11_10_17_21_17_30114429.nef");
            var camera = metadataManager.GetModelOfCamera();
            var creationDate = metadataManager.GetDateOfMediaCreation();

            Assert.Pass();
        }
    }
