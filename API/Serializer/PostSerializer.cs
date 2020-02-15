using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

public class PostSerializer
{
    private static fsSerializer serializer = new fsSerializer();

    public static string SerializePostData(Payloads payload)
    {
        fsData data;
        serializer.TrySerialize(payload.GetType(), payload, out data).AssertSuccessWithoutWarnings();
        return data.ToString();
    }
}
