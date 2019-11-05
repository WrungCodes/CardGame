using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GladePayModel
{

}

[Serializable]
public class GladePayDepositResponse
{
    public string status;
    public string txnRef;
    public string apply_auth;
    public string validate;
    public string message;
}

[Serializable]
public class GladePayOtpResponse
{
    public string status;
    public string message;
    public string cardToken;
    public string txnRef;
}

[Serializable]
public class GladePayWithdrawalResponse
{
    public string status;
    public string txnRef;
    public string message;
}
