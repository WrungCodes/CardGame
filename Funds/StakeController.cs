using UnityEngine;
using System.Collections;
using System;

public static class StakeController
{
    public delegate void PostSuccessCallback(float newAmount);
    public delegate void PostFailedCallback(Exception error);

    public static void StakeForGame(int number_of_players, User user, string token, PostSuccessCallback callback, PostFailedCallback fallback)
    {
        float stake = GetStakeAmount(number_of_players);

        float current_amount = WalletHelpers.CurrentUserWallet(user);
        float new_amount = WalletHelpers.DebitUserWallet(user, stake);

        string tranx_id = Generate.UniqueUuid();
        Transaction transaction = new Transaction(tranx_id, "stake", current_amount, new_amount, user.local_id);
        string trans_data = FormatPostData.TransactionFieldModel(transaction);

        user.wallet_balance = new_amount;
        string new_user_data = FormatPostData.UserFieldModel(user);
        //FireBase.Patch(
        //    new_user_data, "users/" + user.local_id, token,
        //    (response3) =>
        //    {
        //        FireBase.Post(
        //            trans_data, "/transactions/?documentId=" + tranx_id, token,
        //            (response1) =>
        //                {
        //                    callback(new_amount);
        //                },
        //            (error) =>
        //                {
        //                    fallback(error);
        //                }
        //            );
        //    },
        //    (error) => { fallback(error); }
        //);

    }

    public static float GetStakeAmount(int number_of_players)
    {
        switch (number_of_players)
        {
            case 2:
                return Env.TWO_PLAYERS_STAKE;
            case 3:
                return Env.THREE_PLAYERS_STAKE;
            case 4:
                return Env.FOUR_PLAYERS_STAKE;
            case 6:
                return Env.SIX_PLAYERS_STAKE;
            default:
                return 0;
        }
    }

    public static float GetWinAmount(int number_of_players)
    {
        switch (number_of_players)
        {
            case 2:
                return Env.TWO_PLAYERS_WIN;
            case 3:
                return Env.THREE_PLAYERS_WIN;
            case 4:
                return Env.FOUR_PLAYERS_WIN;
            case 6:
                return Env.SIX_PLAYERS_WIN;
            default:
                return 0;
        }
    }
}
