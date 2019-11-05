using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TransactionController
{
    public static void CreateTransaction(float amount, User user, string id_token, string type)
    {
        string id = Generate.UniqueUuid();
        Transaction transaction = new Transaction(id, type, 20f, 10f, user.local_id);
    }
}
