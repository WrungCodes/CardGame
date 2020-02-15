using System;

[Serializable]
public class ValidateStakeResponse : IDeserilizable, IResponse
{
    public bool status { get; set; }
}

