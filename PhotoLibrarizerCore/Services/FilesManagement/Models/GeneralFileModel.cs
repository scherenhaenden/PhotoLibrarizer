namespace PhotoLibrarizerCore.Services.FilesManagement.Models
{
    public class GeneralFileModel
    {
        private string _fullPathOfFile;
        private string _path;
        private string _fileName;
        private string _hash;
        private FileInfo _generalFileInformation;
        private DateTime _dateCreation;

        public DateTime DateCreation
        {
            get
            {
                if(_dateCreation == null || _dateCreation == DateTime.MinValue)
                    _dateCreation = GeneralFileInformation.CreationTime;
                    
                
                return _dateCreation;
            }
            set
            {
                _dateCreation=value;

            }
        }

        public string PathOfFile
        {
            get
            {
                return _path;
            }
            set
            {
                _path=value;

            }
        }

        public string FullPathOfFile
        {
            get
            {
                return _fullPathOfFile;
            }
            set
            {
                
                _fullPathOfFile=value;
                CheckValues ();
                CreateFileInfo ();

            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName=value;
            }
        }
        public string Hash
        {
            get
            {
                return _hash;
            }
            set
            {
                _hash=value;
            }
        }
        public FileInfo GeneralFileInformation
        {
            get
            {
                return _generalFileInformation;
            }
            set
            {
                _generalFileInformation=value;
            }
        }

        protected void CheckValues()
        {
            if (_fullPathOfFile != "") 
            {
                _fileName = Path.GetFileName (_fullPathOfFile);
                _path = Path.GetDirectoryName (_fullPathOfFile);
                CreateFileInfo ();
            }
        }

        protected void CreateFileInfo()
        {
            FileInfo fInfo = new FileInfo (_fullPathOfFile);
            _generalFileInformation = fInfo;



        }
        protected void GetHashValue()
        {
            
        }
    }
}