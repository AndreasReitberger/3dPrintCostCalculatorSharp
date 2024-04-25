using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
#if NETFRAMEWORK || NET6_0_OR_GREATER
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

        // https://stackoverflow.com/questions/965042/c-sharp-serializing-deserializing-a-des-encrypted-file-from-a-stream
        [Obsolete("Will be deleted")]
        internal static bool EncryptAndSerialize(string filename, Calculation3dEnhanced obj)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform e = key.CreateEncryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Create);
            using CryptoStream cs = new(fs, e, CryptoStreamMode.Write);

            XmlSerializer xmlser = new(typeof(Calculation3dEnhanced));
            xmlser.Serialize(cs, obj);
            return true;
        }
        [Obsolete("Will be deleted")]
        internal static bool EncryptAndSerialize(string filename, Calculation3dEnhanced[] objs)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform e = key.CreateEncryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Create);
            using CryptoStream cs = new(fs, e, CryptoStreamMode.Write);

            XmlSerializer xmlser = new(typeof(Calculation3dEnhanced[]));
            xmlser.Serialize(cs, objs);
            return true;
        }

        [Obsolete("Will be deleted")]
        internal static Calculation3dEnhanced? DecryptAndDeserialize(string filename)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform d = key.CreateDecryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Open);
            using CryptoStream cs = new(fs, d, CryptoStreamMode.Read);

            XmlSerializer xmlser = new(typeof(Calculation3dEnhanced));
            return (Calculation3dEnhanced?)xmlser.Deserialize(cs);
        }

        [Obsolete("Will be deleted")]
        internal static Calculation3dEnhanced[]? DecryptAndDeserializeArray(string filename)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform d = key.CreateDecryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Open);
            using CryptoStream cs = new(fs, d, CryptoStreamMode.Read);

            XmlSerializer xmlser = new(typeof(Calculation3dEnhanced[]));
            return (Calculation3dEnhanced[]?)xmlser.Deserialize(cs);
        }
    }
#endif
}
