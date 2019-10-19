using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;

public static class DatabaseHandler
{
    private const string project_id = Env.PROJECT_ID;
    private static readonly string database_url = $"https://{project_id}.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);

    public delegate void GetWithdrawalsCallback(Dictionary<string, Withdraw> withdrawals);

    public delegate void GetDepositsCallback(Dictionary<string, Deposit> deposits);

    // Post user to the firebase Realtime Database
    public static void PostUser(User user, string user_id, PostUserCallback callback, string idToken)
    {
        RestClient.Put<User>($"{database_url}users/{user_id}.json?auth={idToken}", user)
            .Then(response => { callback(); });
    }

    public static void GetUser(string user_id, string idToken, GetUserCallback callback)
    {
        RestClient.Get<User>($"{database_url}users/{user_id}.json?auth={idToken}")
            .Then(user => { callback(user); });
    }

    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{database_url}users.json")
            .Then(response => {

                var responseJson = response.Text;
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

                var users = deserialized as Dictionary<string, User>;
                callback(users);

            });
    }

    public static void PostWithdrawal(Withdraw withdraw_data, PostUserCallback callback, string idToken)
    {
        RestClient.Put<Withdraw>($"{database_url}withdrawals/{withdraw_data.user_local}/{withdraw_data.transaction_id}.json?auth={idToken}", withdraw_data)
            .Then(response => { callback(); });
    }

    public static void GetUserWithdrawals(string user_id, string idToken, GetWithdrawalsCallback callback)
    {
        RestClient.Get($"{database_url}withdrawals/{user_id}.json?auth={idToken}")
            .Then(response => {

                var responseJson = response.Text;
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, Withdraw>), ref deserialized);

                var withdrawals = deserialized as Dictionary<string, Withdraw>;
                callback(withdrawals); });
    }


    public static void PostDeposits(Deposit deposit_data, PostUserCallback callback, string idToken)
    {
        RestClient.Put<Deposit>($"{database_url}deposits/{deposit_data.user_local}/{deposit_data.transaction_id}.json?auth={idToken}", deposit_data)
            .Then(response => { callback(); });
    }

    public static void GetUserDeposit(string user_id, string idToken, GetDepositsCallback callback)
    {
        RestClient.Get($"{database_url}deposits/{user_id}.json?auth={idToken}")
            .Then(response => {

                var responseJson = response.Text;
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, Deposit>), ref deserialized);

                var deposits = deserialized as Dictionary<string, Deposit>;
                callback(deposits);
            });
    }

    public static void UpdateUserWallet(User user, float amount, PostUserCallback callback, string idToken)
    {
        RestClient.Put<float>($"{database_url}users/{user.local_id}/wallet_balance.json?auth={idToken}", user)
            .Then(response => { callback(); });
    }

    public static void GetUserWallet(User user, float amount, PostUserCallback callback, string idToken)
    {
        RestClient.Put<float>($"{database_url}users/{user.local_id}/wallet_balance.json?auth={idToken}", user)
            .Then(response => { callback(); });
    }
}
