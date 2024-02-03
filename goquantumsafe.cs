#if TOOLS
using Godot;
using System;
using OpenQuantumSafe;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

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

	public void GetKeys(KEM kem, out byte[] publicKey, out byte[] privateKey)
    {
        kem.keypair(out publicKey, out privateKey);
    }

	public void Encapsulate(KEM kem, out byte[] cipherText, out byte[] sharedSecret, byte[] publicKey)
	{
		kem.encaps(out cipherText, out sharedSecret, publicKey);
    }

    public void Decapsulate(KEM kem, byte[] cipherText, out byte[] sharedSecret, byte[] privateKey)
    {
        kem.decaps(out sharedSecret, cipherText, privateKey);
    }

    // Convert an object to a byte array
    public byte[] ObjectToByteArray(Object obj)
    {
        string str = JsonConvert.SerializeObject(obj);
        return Encoding.ASCII.GetBytes(str);
    }

    // Convert a byte array to an Object
    public Object ByteArrayToObject(byte[] arrBytes)
    {
        string str = Encoding.ASCII.GetString(arrBytes);
        return JsonConvert.DeserializeObject<Object>(str);
    }

    public void PadToMultipleOf(ref byte[] src, int pad)
    {
        int len = (src.Length + pad - 1) / pad * pad;
        Array.Resize(ref src, len);
    }

    public void Unpad(ref byte[] src, int unpad)
    {
        Array.Resize(ref src, unpad);
    }
}
#endif
