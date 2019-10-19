using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FundsController : MonoBehaviour
{
    public Text wallet_balance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Withdraw()
    {
        float amount = 200000000;
        string bank_name = "Zenith Bank";
        string account_number = "09237234234";

        User current_user = GlobalState.GetUser();
        if (WithdrawController.CheckIfUserBalanceIsEnough(current_user, amount))
        {
            wallet_balance.text = $"WALLET BALANCE: N {WithdrawController.debitUserWallet(current_user, amount)}";
            GlobalState.SetUserWalletBalance(WithdrawController.debitUserWallet(current_user, amount));
            WithdrawController.CreateWithdrawal(bank_name, account_number, amount, current_user, GlobalState.GetToken(),
             () => { }
           );
        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
        //WithdrawController.CheckIfUserBalanceIsEnough(current_user, amount);
    }

    public void Deposit()
    {
        float amount = 2000000000;
        string  bank_name = "Zenith Bank";
        string  account_number = "09237234234";

        User current_user = GlobalState.GetUser();
        wallet_balance.text = $"WALLET BALANCE: N {DepositController.CreditUserWallet(current_user, amount)}";
        GlobalState.SetUserWalletBalance(DepositController.CreditUserWallet(current_user, amount));
        //GlobalState.SetUserWalletBalance(WithdrawController.debitUserWallet(current_user, amount));
        DepositController.CreateDeposit( bank_name, account_number, amount, current_user, GlobalState.GetToken());

    }
}
