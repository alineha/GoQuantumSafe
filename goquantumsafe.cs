#if TOOLS
using Godot;
using System;
using OpenQuantumSafe;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace quantum;

[Tool]
public partial class goquantumsafe : EditorPlugin
{
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}

	public KEM SetKEM(string kem)
	{
		return new KEM(kem);
	}

	public void GetKeys(KEM kem, out CryptoKey cryptoPublicKey, out CryptoKey cryptoPrivateKey)
    {
		byte[] publicKey;
		byte[] privateKey;
        kem.keypair(out publicKey, out privateKey);

        string stringPublicKey = System.Text.Encoding.UTF8.GetString(publicKey);
        cryptoPublicKey = new CryptoKey();
        cryptoPublicKey.LoadFromString(stringPublicKey);

        string stringPrivateKey = System.Text.Encoding.UTF8.GetString(privateKey);
        cryptoPrivateKey = new CryptoKey();
        cryptoPrivateKey.LoadFromString(stringPrivateKey);
    }

	public CryptoKey Encapsulate(KEM kem, out byte[] cipherText, out byte[] sharedSecret, byte[] publicKey)
	{
		kem.encaps(out cipherText, out sharedSecret, publicKey);
        string stringSharedSecret = System.Text.Encoding.UTF8.GetString(sharedSecret);
        CryptoKey cryptoKey = new CryptoKey();
        cryptoKey.LoadFromString(stringSharedSecret);
        return cryptoKey;
    }

    public CryptoKey Decapsulate(KEM kem, byte[] cipherText, out byte[] sharedSecret, byte[] secretKey)
    {
        kem.decaps(out sharedSecret, cipherText, secretKey);
        string stringSharedSecret = System.Text.Encoding.UTF8.GetString(sharedSecret);
		CryptoKey cryptoKey = new CryptoKey();
		cryptoKey.LoadFromString(stringSharedSecret);
		return cryptoKey;
    }
}
#endif
