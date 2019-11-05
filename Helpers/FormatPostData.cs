using UnityEngine;
using System.Collections;
using System;
using FullSerializer;
using Proyecto26;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Networking;

public static class FormatPostData
{

	private static fsSerializer serializer = new fsSerializer();

	public static string UserFieldModel(User user)
	{
		UserFields fields = new UserFields
		{
			email = new Email { stringValue = user.email },
			local_id = new LocalId { stringValue = user.local_id },
			phone_no = new PhoneNo { stringValue = user.local_id },
			username = new Username { stringValue = user.username },
			wallet_balance = new WalletBalance { doubleValue = user.wallet_balance }
		};

		UserRootObject rootObject = new UserRootObject
		{
			fields = fields
		};

		fsData data;
		serializer.TrySerialize(typeof(UserRootObject), rootObject, out data).AssertSuccessWithoutWarnings();

		string post_data = data.ToString();
		return post_data;
	}

	public static string DepositFieldModel(Deposit deposit)
	{
		DepositFields fields = new DepositFields
		{
            id = new TransactionId { stringValue = deposit.id },
            email = new Email { stringValue = deposit.user_email },
            local_id = new LocalId { stringValue = deposit.user_local },
            username = new Username { stringValue = deposit.user_name },
            amount = new Amount { doubleValue = deposit.amount },
            status = new Status { stringValue = deposit.status},
            transaction_id = new TransactionId { stringValue = deposit.transaction_id },
            reference = new TransactionId { stringValue = deposit.reference },
        };

		DepositRootObject rootObject = new DepositRootObject
		{
			fields = fields
		};

		fsData data;
		serializer.TrySerialize(typeof(DepositRootObject), rootObject, out data).AssertSuccessWithoutWarnings();

		string post_data = data.ToString();
		return post_data;
	}

    public static string WithdrawalFieldModel(Withdraw withdraw)
    {
        WithdrawalFields fields = new WithdrawalFields
        {
            id = new TransactionId { stringValue = withdraw.id },
            email = new Email { stringValue = withdraw.user_email },
            local_id = new LocalId { stringValue = withdraw.user_local },
            username = new Username { stringValue = withdraw.user_name },
            bank_name = new BankName { stringValue = withdraw.bank_name },
            bank_code = new BankCode { stringValue = withdraw.bank_code },
            account_number = new AccountNumber { stringValue = withdraw.account_number },
            amount = new Amount { doubleValue = withdraw.amount },
            status = new Status { stringValue = withdraw.status },
            transaction_id = new TransactionId { stringValue = withdraw.transaction_id },
            reference = new TransactionId { stringValue = withdraw.reference },
        };

        WithdrawalRootObject rootObject = new WithdrawalRootObject
        {
            fields = fields
        };

        fsData data;
        serializer.TrySerialize(typeof(WithdrawalRootObject), rootObject, out data).AssertSuccessWithoutWarnings();

        string post_data = data.ToString();
        return post_data;
    }

    public static string TransactionFieldModel(Transaction transaction)
    {
        TransactionFields fields = new TransactionFields
        {         
            local_id = new LocalId { stringValue = transaction.user_local },
            initial_amount = new Amount { doubleValue = transaction.initial_amount },
            final_amount = new Amount { doubleValue = transaction.final_amount },
            type = new TransactionType { stringValue = transaction.type },
            id = new TransactionId { stringValue = transaction.id },
            time = new Time { stringValue = transaction.time}
        };

        transactionRootObject rootObject = new transactionRootObject
        {
            fields = fields
        };

        fsData data;
        serializer.TrySerialize(typeof(transactionRootObject), rootObject, out data).AssertSuccessWithoutWarnings();

        string post_data = data.ToString();
        return post_data;
    }
}
