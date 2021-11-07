using AndreasReitberger.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace AndreasReitberger
{
#if NETFRAMEWORK
    //#if net472
    public static class Calculator3dExporter
    {
    #region Variables
        //static string _fileExtension = "";
        static readonly string _encryptionPhrase = "U4fRwU^K#.fA+$8y";
        //static readonly DESCryptoServiceProvider key = new();
    #endregion

        public static bool Save(Calculation3d calc, string path)
        {

            //string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            XmlSerializer x = new(typeof(Calculation3d));
            DirectoryInfo tempDir = new(path);
            Directory.CreateDirectory(tempDir.Parent.FullName);
            TextWriter writer = new StreamWriter(tempDir.FullName);
            x.Serialize(writer, calc);
            writer.Close();
            return true;
        }

        public static bool Save(Calculation3d[] calcs, string path)
        {

            //string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            XmlSerializer x = new(typeof(Calculation3d[]));
            DirectoryInfo tempDir = new(path);
            Directory.CreateDirectory(tempDir.Parent.FullName);
            TextWriter writer = new StreamWriter(tempDir.FullName);
            x.Serialize(writer, calcs);
            writer.Close();
            return true;

        }
        public static bool Load(string path, out Calculation3d calc)
        {
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer = new(typeof(Calculation3d));
            // To read the file, create a FileStream.  

            FileStream myFileStream = new(path, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            Calculation3d retval = (Calculation3d)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            calc = retval;
            return true;
        }
        public static bool Load(string path, out Calculation3d[] calcs)
        {

            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer = new(typeof(Calculation3d[]));
            // To read the file, create a FileStream.  

            FileStream myFileStream = new(path, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            Calculation3d[] retval = (Calculation3d[])mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            calcs = retval;
            return true;
        }

        // https://stackoverflow.com/questions/965042/c-sharp-serializing-deserializing-a-des-encrypted-file-from-a-stream
        public static bool EncryptAndSerialize(string filename, Calculation3d obj)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform e = key.CreateEncryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Create);
            using CryptoStream cs = new(fs, e, CryptoStreamMode.Write);

            XmlSerializer xmlser = new(typeof(Calculation3d));
            xmlser.Serialize(cs, obj);
            return true;
        }
        public static bool EncryptAndSerialize(string filename, Calculation3d[] objs)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform e = key.CreateEncryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Create);
            using CryptoStream cs = new(fs, e, CryptoStreamMode.Write);

            XmlSerializer xmlser = new(typeof(Calculation3d[]));
            xmlser.Serialize(cs, objs);
            return true;
        }

        public static Calculation3d DecryptAndDeserialize(string filename)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform d = key.CreateDecryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Open);
            using CryptoStream cs = new(fs, d, CryptoStreamMode.Read);

            XmlSerializer xmlser = new(typeof(Calculation3d));
            return (Calculation3d)xmlser.Deserialize(cs);
        }
        public static Calculation3d[] DecryptAndDeserializeArray(string filename)
        {
            DESCryptoServiceProvider key = new();
            ICryptoTransform d = key.CreateDecryptor(Encoding.ASCII.GetBytes("64bitPas"), Encoding.ASCII.GetBytes(_encryptionPhrase));

            using FileStream fs = File.Open(filename, FileMode.Open);
            using CryptoStream cs = new(fs, d, CryptoStreamMode.Read);

            XmlSerializer xmlser = new(typeof(Calculation3d[]));
            return (Calculation3d[])xmlser.Deserialize(cs);
        }
    }
#endif
}
