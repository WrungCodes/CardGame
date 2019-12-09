using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System;

[System.Serializable]
public class CardSerializer
{
	public string Rank;
	public string Suit;

	public CardSerializer()
	{

    }

	public CardSerializer(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

	public static byte[] Serialize(object obj)
	{
		CardSerializer data = (CardSerializer)obj;

		//RANK.
		byte[] RankBytes = Encoding.ASCII.GetBytes(data.Rank);
		if (BitConverter.IsLittleEndian)
			Array.Reverse(RankBytes);

		//SUIT.
		byte[] SuitBytes = Encoding.ASCII.GetBytes(data.Suit);
		if (BitConverter.IsLittleEndian)
			Array.Reverse(SuitBytes);

		return JoinBytes(RankBytes, SuitBytes);
	}

	public static object Deserialize(byte[] bytes)
	{
		CardSerializer data = new CardSerializer();

		//RANK.
		byte[] RankBytes = new byte[4];

        Array.Copy(bytes, 0, RankBytes, 0, RankBytes.Length);
		if (BitConverter.IsLittleEndian)
				Array.Reverse(RankBytes);
		data.Rank = Encoding.UTF8.GetString(RankBytes);


		//SUIT.
		byte[] SuitBytes = new byte[bytes.Length - 4];

		if (SuitBytes.Length > 0)
		{
			Array.Copy(bytes, 4, SuitBytes, 0, SuitBytes.Length);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(SuitBytes);
			data.Suit = Encoding.UTF8.GetString(SuitBytes);
		}
		else
		{
			data.Suit = string.Empty;
		}

		return data;
	}


	private static byte[] JoinBytes(params byte[][] arrays)
	{
		byte[] rv = new byte[arrays.Sum(a => a.Length)];
		int offset = 0;
		foreach (byte[] array in arrays)
		{
			System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
			offset += array.Length;
		}
		return rv;
	}
}
