using System;

[Serializable]
public class StakePayload : Payloads
{
    public string uid;

    public StakePayload(string _uid)
    {
        this.uid = _uid;
    }
}


