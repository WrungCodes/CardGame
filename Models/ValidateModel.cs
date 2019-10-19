using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ValidateModel
{
    public bool status;
    public string message;

    public ValidateModel(bool _status, string _message)
    {
        status = _status;
        message = _message;

    }
}
