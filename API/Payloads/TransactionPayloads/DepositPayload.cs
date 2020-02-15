using System;

[Serializable]
public class DepositPayload : Payloads
{
    public float amount;

    public DepositPayload(float _amount)
    {
        this.amount = _amount;
    }
}


