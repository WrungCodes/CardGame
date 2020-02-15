using System;

[Serializable]
public class ProfileResponse : IDeserilizable, IResponse
{
    public ProfileModel profile { get; set; }
}
