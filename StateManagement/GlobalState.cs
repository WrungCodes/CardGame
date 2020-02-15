using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FullSerializer;
using Proyecto26;
using System;
public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance;

    private static User signedInUser;

    private static string idToken;

    private static fsSerializer serializer = new fsSerializer();

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        InvokeRepeating("RefreshToken", 1200, 1200);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetUser(User user)
    {
        signedInUser = user;
        //FireBase.CreateUser( GetToken());
    }

    public static User GetUser()
    {
        return signedInUser;
    }

    public static void SetToken(string token)
    {
        idToken = token;
    }

    public static string GetToken()
    {
        return idToken;
    }

    public static void SetUserWalletBalance(float amount)
    {
        signedInUser.wallet_balance = amount;
    }

    public void RefreshToken()
    {
        //string token = PlayerPrefs.GetString("refresh_token");

        //RefreshController.Refresh(token,
        //            (refresh_token, id_token) =>
        //            {
        //                SetToken(id_token);
        //                SetRefreshToken(refresh_token);
        //            },
        //            (error) =>
        //            {
        //                Debug.Log(error);
        //            }
        //        );
    }

    private static void SetRefreshToken(string token)
    {
        PlayerPrefs.SetString("refresh_token", token);
    }
}