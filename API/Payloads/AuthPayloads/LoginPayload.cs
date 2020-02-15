using System;

[Serializable]
public class LoginPayload : Payloads
{
    public string email;

    public string password;

    public LoginPayload(string _email, string _password)
    {
        this.email = _email;
        this.password = _password;
    }
}