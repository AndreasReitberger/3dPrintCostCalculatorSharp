
#if (NETFRAMEWORK || NET6_0_OR_GREATER) && OBSOLETE
using System.Security.Cryptography;
using System.Text;
#endif

namespace AndreasReitberger.Print3d.SQLite
{
#if (NETFRAMEWORK || NET6_0_OR_GREATER) && OBSOLETE
    //#if net472
    public static class Calculator3dExporter
    {
        #region Variables
        //static string _fileExtension = "";
        static readonly string _encryptionPhrase = "U4fRwU^K#.fA+$8y";
        //static readonly DESCryptoServiceProvider key = new();
        #endregion

        public static bool Save(Calculation3dEnhanced calc, string path)
        {

            //string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            XmlSerializer x = new(typeof(Calculation3dEnhanced));
            DirectoryInfo tempDir = new(path);
            if (tempDir.Parent is not null)
            {
                Directory.CreateDirectory(tempDir.Parent.FullName);
                TextWriter writer = new StreamWriter(tempDir.FullName);
                x.Serialize(writer, calc);
                writer.Close();
                return true;
            }
            return false;
        }

        public static bool Save(Calculation3dEnhanced[] calcs, string path)
        {

            //string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            XmlSerializer x = new(typeof(Calculation3dEnhanced[]));
            DirectoryInfo tempDir = new(path);
            if (tempDir.Parent is not null)
            {
                Directory.CreateDirectory(tempDir.Parent.FullName);
                TextWriter writer = new StreamWriter(tempDir.FullName);
                x.Serialize(writer, calcs);
                writer.Close();
                return true;
            }
            return false;
        }
        public static bool Load(string path, out Calculation3dEnhanced? calc)
        {
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer = new(typeof(Calculation3dEnhanced));
            // To read the file, create a FileStream.  

            FileStream myFileStream = new(path, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            Calculation3dEnhanced? retval = (Calculation3dEnhanced?)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            calc = retval;
            return true;
        }
        public static bool Load(string path, out Calculation3dEnhanced[]? calcs)
        {

            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer = new(typeof(Calculation3dEnhanced[]));
            // To read the file, create a FileStream.  

            FileStream myFileStream = new(path, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            Calculation3dEnhanced[]? retval = (Calculation3dEnhanced[]?)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            calcs = retval;
            return true;
        }
    }
#endif
}
