using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRequest
{
    public Request request;

    public HttpClient.Callback callback;

    public HttpClient.Fallback fallback;

    public FullRequest(Request _request, HttpClient.Callback _callback, HttpClient.Fallback _fallback)
    {
        this.request = _request;
        this.callback = _callback;
        this.fallback = _fallback;
    }
}
