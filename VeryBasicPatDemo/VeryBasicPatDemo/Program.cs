using System.Net;
using System.Security;
using VeryBasicPatDemo;

// User would normally enter this data directly into the secure string to reduce time it is in memory.
// However, to keep this sample simple, this string will represent the user inputting data.
var dataToEncrypt = "MyTestPassword";

Console.WriteLine($"Encrypting \"{dataToEncrypt}\" and storing it");
using var secureString = new SecureString();
foreach (var c in dataToEncrypt)
{
    secureString.AppendChar(c);
}
Sample.StorePat(secureString);

Console.WriteLine("Decrypting PAT from storage");
using var retrievedValue = Sample.RetrievePat();
Console.WriteLine($"Decrypted PAT: {new NetworkCredential("", retrievedValue).Password}");
