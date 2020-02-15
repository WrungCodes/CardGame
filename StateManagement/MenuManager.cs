using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text wallet_balance;
    public Text username;

    public GameObject info_panel;
    public Text info_panel_text;

    public GameObject Loader;

    // Start is called before the first frame update
    void Start()
    {
        username.text = State.UserProfile.username;
    }

    public void SetWalletBalance(float amount)
    {
        wallet_balance.text = $"WALLET BALANCE: N {amount}";
    }

    // Update is called once per frame
    void Update()
    {
        wallet_balance.text = $"WALLET BALANCE: N {State.UserProfile.naira_balance}";
    }

    private void showPopUp(string message, string type)
    {
        info_panel.gameObject.SetActive(true);
        switch (type)
        {
            case "error":
                info_panel_text.color = Color.red;
                break;

            case "success":
                info_panel_text.color = Color.black;
                break;
        }
        info_panel_text.text = message;
    }

    public IEnumerator showPopUpT(string message, string type)
    {
        showPopUp(message, type);
        yield return new WaitForSeconds(5f);
        info_panel.gameObject.SetActive(false);
    }

    public void SetLoading(GameObject gameObject)
    {
        Loader.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UnSetLoading(GameObject gameObject)
    {
        Loader.SetActive(false);
        gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
