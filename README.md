# Securely Store Data C# Sample

This project will "securely" store a piece of data completely offline and managed by the application.

**No locally stored secret is truly unhackable, even the data stored in this sample.**

However this project shows a simple sample of how it could be made really hard to get the user's data even if the attacker has the files by adding several layers of defense:

* Isolated Storage - Obfuscate where the app stores its data
* Microsoft's DPAPI - Encrypts a stream of data based the user running the process
* User provided key - Add another layer of encryption using a key only the user knows
* Compile time entropy - Keys provided at compile time to add further complexity to the data encryption. This is designed to be injected by CI either with a secret value or even by an action like [Some Github Action](SomeAddress)
