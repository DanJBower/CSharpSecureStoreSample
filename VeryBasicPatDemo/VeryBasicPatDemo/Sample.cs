using System.IO.IsolatedStorage;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace VeryBasicPatDemo;

public static class Sample
{
    private static readonly byte[] Entropy = "MyApplicationEntropy"u8.ToArray();

    public static SecureString RetrievePat()
    {
        // Get the PAT from storage
        var storageArea = IsolatedStorageFile.GetUserStoreForApplication();
        if (!storageArea.FileExists("MyApplicationPatStorage.secret"))
        {
            throw new FileNotFoundException("Not encrypted PAT data found");
        }
        using var patStorage = new IsolatedStorageFileStream("MyApplicationPatStorage.secret", FileMode.Open, storageArea);
        using var encryptedDataStream = new MemoryStream();
        patStorage.CopyTo(encryptedDataStream);
        var encryptedData = encryptedDataStream.ToArray();

        SecureString result = new();

        {
            // Decrypt it
            var patData = ProtectedData.Unprotect(encryptedData, Entropy, DataProtectionScope.CurrentUser);
            var plainTextPat = Encoding.UTF8.GetString(patData);

            foreach (var c in plainTextPat)
            {
                result.AppendChar(c);
            }
        }

        GC.Collect(); // Force remove unprotected string data from memory
        return result;
    }

    public static void StorePat(SecureString pat)
    {
        byte[] encryptedData;

        {
            // Get the PAT to encrypt
            var plainTextPat = new NetworkCredential("", pat).Password;
            var patData = Encoding.UTF8.GetBytes(plainTextPat);

            // Encrypt it
            encryptedData = ProtectedData.Protect(patData, Entropy, DataProtectionScope.CurrentUser);
        }

        GC.Collect(); // Force remove unprotected string data from memory

        // Store it
        var storageArea = IsolatedStorageFile.GetUserStoreForApplication();
        using var patStorage = new IsolatedStorageFileStream("MyApplicationPatStorage.secret", FileMode.Create, storageArea);
        patStorage.Write(encryptedData);
    }
}
