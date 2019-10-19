using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FireBaseUser
{
    public Dictionary<string, User> fields;
}

[Serializable]
public class User1
{
    public Dictionary<string, User> fields;
}

[Serializable]
public class Email
{
    public string stringValue { get; set; }
}

[Serializable]
public class LocalId
{
    public string stringValue { get; set; }
}

[Serializable]
public class PhoneNo
{
    public string stringValue { get; set; }
}

[Serializable]
public class Username
{
    public string stringValue { get; set; }
}

[Serializable]
public class WalletBalance
{
    public float doubleValue { get; set; }
}

[Serializable]
public class UserFields
{
    public Email email { get; set; }
    public LocalId local_id { get; set; }
    public PhoneNo phone_no { get; set; }
    public Username username { get; set; }
    public WalletBalance wallet_balance { get; set; }

    public User returnUser()
    {
        User user = new User(
            username.stringValue,
            phone_no.stringValue,
            email.stringValue,
            wallet_balance.doubleValue,
            local_id.stringValue
        );
        return user;
    }
}

[Serializable]
public class UserRootObject
{
    public UserFields fields { get; set; }
}

[Serializable]
public class TransactionId
{
    public string stringValue { get; set; }
}

[Serializable]
public class BankName
{
    public string stringValue { get; set; }
}

[Serializable]
public class AccountNumber
{
    public string stringValue { get; set; }
}

[Serializable]
public class Amount
{
    public float doubleValue { get; set; }
}

[Serializable]
public class Status
{
    public string stringValue { get; set; }
}

[Serializable]
public class TransactionType
{
    public string stringValue { get; set; }
}

[Serializable]
public class Time
{
    public string timeStamp { get; set; }
}

[Serializable]
public class DepositFields
{
    public TransactionId id { get; set; }
    public Email email { get; set; }
    public LocalId local_id { get; set; }
    public Username username { get; set; }
    public Time time { get; set; }
    public TransactionId transaction_id { get; set; }
    public AccountNumber account_number { get; set; }
    public BankName bank_name { get; set; }
    public Amount amount { get; set; }
    public Status status { get; set; }
    public TransactionId reference { get; set; }

    public Deposit returnDeposit()
    {
        Deposit deposit = new Deposit(
            id.stringValue,
            transaction_id.stringValue,
            bank_name.stringValue,
            account_number.stringValue,
            amount.doubleValue,
            reference.stringValue,
            status.stringValue,
            local_id.stringValue,
            username.stringValue,
            email.stringValue
        );
        return deposit;
    }
}

[Serializable]
public class DepositRootObject
{
    public DepositFields fields { get; set; }
}

[Serializable]
public class WithdrawalFields
{
    public TransactionId id { get; set; }
    public Email email { get; set; }
    public LocalId local_id { get; set; }
    public Username username { get; set; }
    public Time time { get; set; }
    public TransactionId transaction_id { get; set; }
    public AccountNumber account_number { get; set; }
    public BankName bank_name { get; set; }
    public Amount amount { get; set; }
    public Status status { get; set; }
    public TransactionId reference { get; set; }

    public Withdraw returnWithdrawal()
    {
        Withdraw withdraw = new Withdraw(
            id.stringValue,
            transaction_id.stringValue,
            bank_name.stringValue,
            account_number.stringValue,
            amount.doubleValue,
            reference.stringValue,
            status.stringValue,
            local_id.stringValue,
            username.stringValue,
            email.stringValue
        );
        return withdraw;
    }
}

[Serializable]
public class WithdrawalRootObject
{
    public WithdrawalFields fields { get; set; }
}

[Serializable]
public class TransactionFields
{
    public TransactionId id { get; set; }
    public LocalId local_id { get; set; }
    public TransactionType type { get; set; }
    public Time time { get; set; }
    public Amount initial_amount { get; set; }
    public Amount final_amount { get; set; }
}

[Serializable]
public class transactionRootObject
{
    public TransactionFields fields { get; set; }
}

[Serializable]
public class UserDocument
{
    public string name { get; set; }
    public UserFields fields { get; set; }
    public DateTime createTime { get; set; }
    public DateTime updateTime { get; set; }

    public User returnUser()
    {
        User user = new User(
            fields.username.stringValue,
            fields.phone_no.stringValue,
            fields.email.stringValue,
            fields.wallet_balance.doubleValue,
            fields.local_id.stringValue
        );
        return user;
    }
}

[Serializable]
public class SingleUserRootObject
{
    public UserDocument document { get; set; }
}

[Serializable]
public class AllUserRootObject
{
    public List<UserDocument> documents { get; set; }
}

[Serializable]
public class TransactionDocument
{
    public string name { get; set; }
    public TransactionFields fields { get; set; }
    public DateTime createTime { get; set; }
    public DateTime updateTime { get; set; }

    public Transaction returnTransaction()
    {
        Transaction transaction = new Transaction(        
            fields.id.stringValue,
            fields.type.stringValue,
            fields.initial_amount.doubleValue,
            fields.final_amount.doubleValue,
            fields.local_id.stringValue
        );
        return transaction;
    }
}

[Serializable]
public class AllTransactionRootObject
{
    public List<TransactionDocument> documents { get; set; }
}

[Serializable]
public class SingleTransactionRootObject
{
    public TransactionDocument document { get; set; }
}