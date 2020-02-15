using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

public class Delegates
{
    public delegate void ResponseCallback(IResponse response);

    public delegate void ResponseFallback(long statusCode, Errors error);

    public static fsSerializer serializer = new fsSerializer();
}
