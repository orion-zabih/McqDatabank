using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using KTS_Entity;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace KTS_Testing_System.Classes
{
    public class Utility
    {
        public static string getAppVersion()
        {
            // ############# FOR DESKTOP APPLICATIONS ###############
            //Type type = Type.GetType("System.Int32");
            //Assembly assembly = Assembly.GetCallingAssembly();
            //AssemblyName assemblyName = assembly.GetName();
            //Version version = assemblyName.Version;
            //int ver = version.Build;
            //return version.ToString();

            // ############# FOR ASP.NET MVC APPLICATIONS ###############
            return typeof(KTS_Testing_System.MvcApplication).Assembly.GetName().Version.ToString();
        }
        public static string GetFormattedNumber(long? argNumber)
        {
            if (argNumber == null)
                argNumber = 0;
            return argNumber.Value.ToString("0,0", System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string ToTitlecase(object name)
        {
            try
            {
                if (name != null && name != "")
                {
                    System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    string title = textInfo.ToTitleCase(name.ToString());
                    return title;
                }
                else
                    return "";
            }
            catch (Exception)
            {

                return "";
                // throw;
            }
        }

        public static string MD5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        //public static DateTime GetServerDateTime(ref Kts_dataEntities context)
        //{
        //    //var data = context.ExecuteScalar<DateTime>("select current_timestamp from dual", null); // oracle
        //    var data = context.ExecuteScalar<DateTime>("SELECT GETDATE() AS CurrentDateTime", null);//sql server
        //    if (data == null)
        //        throw new Exception("Server date time not found");
        //    return data;
        //}
        public static DateTime GetServerDateTime(Kts_dataEntities context)
        {
            //Entities context = new Entities();
            try
            {
                return context.Database.SqlQuery<DateTime>("SELECT CURRENT_TIMESTAMP").FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //if (context != null)
                //    context.Dispose();
            }


        }
        public static string DateFormat(DateTime arg, string format)
        {
            try
            {
                CultureFormatter formater = new CultureFormatter();
                return formater.Format(format, arg, formater);
            }
            catch (Exception)
            {
                return "";
            }
        }


    }
    public class CultureFormatter : IFormatProvider, ICustomFormatter
    {
        // always use dd-MM-yyyy for date
        private CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is DateTime)
            {
                if (string.IsNullOrEmpty(format))
                {
                    // by default, format date to dd-MM-yyyy
                    return string.Format(enUsCulture, "{dd/MM/yyyy}", arg);
                }
                else
                {
                    // if user supplied own format use it
                    return ((DateTime)arg).ToString(format, enUsCulture);
                }
            }
            // format everything else normally
            if (arg is IFormattable)
                return ((IFormattable)arg).ToString(format, formatProvider);
            else
                return arg.ToString();
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }


    }


    class CryptoHelper
    {
        private const String SECRET_KEY = "1234567890abcder";//16 char secret key      

        public static string Decrypt(string textToDecrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        //public static string Encrypt(string textToEncrypt)
        //{
        //    return Encrypt(textToEncrypt, SECRET_KEY);
        //}

        public static string Encrypt(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
    }


    public static class HttpPostedFileBaseExtensions
    {
        public const int ImageMinimumBytes = 512;

        public static bool IsImage(this HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return true;
        }
    }

}