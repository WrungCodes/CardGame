using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text wallet_balance;
    public Text username;

    private User user;


    // Start is called before the first frame update
    void Start()
    {
        user = GlobalState.GetUser();
        wallet_balance.text = $"WALLET BALANCE: N {user.wallet_balance}";
        username.text = user.username;
        Debug.Log(user.wallet_balance);
        Debug.Log(user);

    }

    public void SetWalletBalance(float amount)
    {
        wallet_balance.text = $"WALLET BALANCE: N {amount}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
