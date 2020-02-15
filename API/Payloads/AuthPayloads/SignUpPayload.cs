using System;

[Serializable]
public class SignUpPayload : Payloads
{
    public string email;

    public string username;

    public string password;

    public SignUpPayload(string _username, string _email, string _password)
    {
        this.username = _username;
        this.email = _email;
        this.password = _password;
    }
}