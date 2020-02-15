using System;
using System.Collections.Generic;

[Serializable]
public class ValidationError : Errors
{
    public string message;

    public Dictionary<string, List<string>> errors;
}