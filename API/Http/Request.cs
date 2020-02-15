using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpClient;

public class Request
{
    public Method method { get; }

    public string uri { get; }

    public Payloads payload { get; }

    public Dictionary<string, string> pathParams { get; }

    public Request(Method _method, string _uri, Payloads _payload = null, Dictionary<string, string> _pathParams = null)
    {
        this.method = _method;
        this.uri = _uri;
        this.payload = _payload;
        this.pathParams = _pathParams;
    }

    public Request GetRequest()
    {
        return this;
    }

}
