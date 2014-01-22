using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Composite.WindowsAzure.Tools.CompositeEncryptionCmd
{
    class Program
    {

        
            
        /// <summary>
        /// Before runnign this tool you need to create pfx file.
        /// 
        /// http://msdn.microsoft.com/en-us/library/windowsazure/gg551722.aspx
        /// c:\dev>makecert -sky exchange -r -n "CN=Composite.WindowsAzure.OpenCMS.Certificate" -pe -a sha1 -len 2048 -ss My "Composite.WindowsAzure.OpenCMS.Certificate.cer"
        /// 
        /// export the pfx file with private key and upload to management portal.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string thumbprint = args[0];
            
           
            Console.WriteLine();

            Console.WriteLine(args[1]);
            Console.WriteLine();

            X509Certificate2 cert = LoadCertificate(
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser, thumbprint);
            
            byte[] encoded = System.Text.UTF8Encoding.UTF8.GetBytes(args[1]);
            var content = new ContentInfo(encoded);
            var env = new EnvelopedCms(content);
            env.Encrypt(new CmsRecipient(cert));

            string encrypted64 = Convert.ToBase64String(env.Encode());
            System.Console.Out.WriteLine(encrypted64);
            Console.WriteLine();

            Console.ReadKey();
        }
        public static X509Certificate2 LoadCertificate(StoreName storeName,
           StoreLocation storeLocation, string thumbprint)
        {
            X509Store store = new X509Store(storeName, storeLocation);

            try
            {
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certificateCollection =
                     store.Certificates.Find(X509FindType.FindByThumbprint,
                                            thumbprint, false);

                if (certificateCollection.Count > 0)
                {
                    //  We ignore if there is more than one matching cert, 
                    //  we just return the first one.
                    return certificateCollection[0];
                }
                else
                {
                    throw new ArgumentException("Certificate not found");
                }
            }
            finally
            {
                store.Close();
            }
        }
        public static byte[] Encrypt(byte[] plainData, bool fOAEP,
                                   X509Certificate2 certificate)
        {
            if (plainData == null)
            {
                throw new ArgumentNullException("plainData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
            {
                // Note that we use the public key to encrypt
                provider.FromXmlString(GetPublicKey(certificate));
                return provider.Encrypt(plainData, fOAEP);
            }
        }
        public static string GetPublicKey(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            return certificate.PublicKey.Key.ToXmlString(false);
        }

        public static byte[] Decrypt(byte[] encryptedData, bool fOAEP,
                               X509Certificate2 certificate)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException("encryptedData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
            {
                // Note that we use the private key to decrypt
                provider.FromXmlString(GetXmlKeyPair(certificate));

                return provider.Decrypt(encryptedData, fOAEP);
            }
        }
        public static string GetXmlKeyPair(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            if (!certificate.HasPrivateKey)
            {
                throw new ArgumentException("certificate does not have a private key");
            }
            else
            {
                return certificate.PrivateKey.ToXmlString(true);
            }
        }
    }
}