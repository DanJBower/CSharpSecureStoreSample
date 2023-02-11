# Securely Store Data C# Sample

This project will "securely" store a piece of data completely offline and managed by the application.

**No locally stored secret is truly unhackable, even the data stored in this sample.**

## Security Steps Employed By Sample

This project shows a simple sample of how it could be made really hard to get the user's data even if the attacker has the files by adding several layers of defense:

* Isolated Storage - Obfuscate where the app stores its data.
* Microsoft's DPAPI - Encrypts a stream of data based the user running the process.
* User provided key - Add another layer of encryption using a key only the user knows.
* Compile time provided entropy - Keys provided at compile time to add further complexity to the data encryption.
  * This is designed to be injected by CI either with a secret value.
  * Would be even better to use an action like [Some Github Action](SomeAddress) and that way even you don't know the entropy values. *Disclaimer: I made that action.*

## Potential Points of Failure

As mentioned earlier, this solution is not infallible:

* Isolated storage
  * Only obfuscates where and how data is stored, can still be accessed by other processes.
  * `Isolated storage is not protected from highly trusted code, from unmanaged code, or from trusted users of the computer` - [Microsoft Documentation](https://learn.microsoft.com/en-us/dotnet/standard/io/isolated-storage#:~:text=isolated%20storage%20is%20not%20protected%20from%20highly%20trusted%20code%2C%20from%20unmanaged%20code%2C%20or%20from%20trusted%20users%20of%20the%20computer).
* Microsoft's DPAPI
  * Another process run by the same user can still decrypt the data.
* User provided key
  * User shares their own key with someone else
  * User keeps their own key insecurely.
* Compile time provided entropy
  * Application could be built locally and use the plain text defaults that are stored in the repo.
  * Repository CI secrets provider could be breached.
  * If using a CI action, it could be untrusted.
  * Application could be decompiled to find the keys provided for entropy.

The chances of all these steps failing though are very low.

## Potential Improvements

* Don't store secrets offline/locally. But you're trying to do that, that's why you're here.
* Add more layers of encryption to the data.
* Obfuscate compiled code.
