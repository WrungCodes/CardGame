using System;

[Serializable]
public class ForgotPasswordPayload : Payloads
{
    public string email;

    public ForgotPasswordPayload(string _email)
    {
        this.email = _email;
    }
}
