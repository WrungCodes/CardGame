using System;

[Serializable]
public class ResendEmailPayload : Payloads
{
    public string email;

    public ResendEmailPayload(string _email)
    {
        this.email = _email;
    }
}

