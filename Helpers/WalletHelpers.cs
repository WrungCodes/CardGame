using UnityEngine;
using System.Collections;

public static class WalletHelpers
{
    public static bool CheckIfUserBalanceIsEnough(User user, float amount)
    {
        if (user.wallet_balance > amount)
        {
            return true;
        }
        return false;
    }

    public static float DebitUserWallet(User user, float amount)
    {
        float new_balance = user.wallet_balance - amount;
        return new_balance;
    }

    public static float CurrentUserWallet(User user)
    {
        return user.wallet_balance;
    }
}