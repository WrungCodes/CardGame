using UnityEngine;
using System.Collections;
using System;

public static class Generate
{
    // Use this for initialization
    public static string UniqueUuid()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
        return FormNumber.ToString("X");
    }
}
