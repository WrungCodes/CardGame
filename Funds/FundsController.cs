using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FundsController : MonoBehaviour
{
    public Text wallet_balance_deposit;
    public Text username_deposit;

    public Text wallet_balance_withdrawal;
    public Text username_withdrawal;

    public Text main_wallet_balance;

    public InputField amount;
    public InputField cardNumber;
    public InputField cvv;
    public InputField month;
    public InputField year;
    public InputField pin;
    public InputField otp;

    public InputField accountNumber;
    public InputField withdrawal_amount;
    public Dropdown banksList;

    public GameObject deposit_form;
    public GameObject deposit_otp_form;
    public GameObject deposit_loading;
    public GameObject deposit_success;
    public GameObject deposit_failed;

    public GameObject deposit_back_button;

    public GameObject withdraw_form;
    public GameObject withdraw_loading;
    public GameObject withdraw_success;
    public GameObject withdraw_failed;

    public GameObject withdraw_back_button;



    public string tranRef;
    public float float_amount;

    private string bankCode;
    private string bankName;

    Dictionary<string, string> banks;

    //public GameObject mainMenu;



    // Start is called before the first frame update
    void Start()
    {
        User user = GlobalState.GetUser();

        wallet_balance_deposit.text = $"WALLET BALANCE: N {user.wallet_balance}";
        username_deposit.text = user.username;

        wallet_balance_withdrawal.text = $"WALLET BALANCE: N {user.wallet_balance}";
        username_withdrawal.text = user.username;

        tranRef = "";
        float_amount = 0;
        deposit_form.gameObject.SetActive(true);



        //Withdraw();
    }

    private void Awake()
    {
        banks = Banks.AllBanks();

        bankCode = banks.Values.First();
        bankName = banks.Keys.First();

        banksList.ClearOptions();
        banksList.AddOptions(banks.Keys.ToList());

        banksList.onValueChanged.AddListener(DropdownValueChanged);

    }

    void ClearFields()
    {
        amount.text = "";
        cardNumber.text = "";
        cvv.text = "";
        month.text = "";
        year.text = "";
        pin.text = "";
        accountNumber.text = "";
        withdrawal_amount.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateDeposit()
    {
        string[] fields = { amount.text, cardNumber.text, cvv.text, month.text, year.text, pin.text };

        Validate.CheckEmptyFields(fields,
            (message) => { Debug.Log("Empty Fields"); },
            () =>
            {
                SetLoading(deposit_loading, deposit_form, deposit_back_button, true);
                float_amount = float.Parse(amount.text);

                User user = GlobalState.GetUser();
                string token = GlobalState.GetToken();

                DepositController.DepositWithCard(amount.text, cardNumber.text, cvv.text, month.text, year.text, pin.text,
                    user, token,
                    (response) =>
                    {
                        GladePayDepositResponse pay = FormatGetData.GladePayDeposit(response);
                        tranRef = pay.txnRef;
                        SetLoading(deposit_loading, deposit_otp_form, deposit_back_button, false);
                        ClearFields();
                    },
                    (error) =>
                    {
                        ClearFields();
                        SetLoading(deposit_loading, deposit_failed, deposit_back_button, false);
                        Debug.Log(error);
                    }
                    );
            });
    }

    public void ValidateDeposit()
    {
        if (otp.text == "")
        {
            Debug.Log("Invalid Otp");
            return;
        }

        SetLoading(deposit_loading, deposit_otp_form, deposit_back_button, true);
        User user = GlobalState.GetUser();
        string token = GlobalState.GetToken();

        DepositController.DepositValidate(float_amount, otp.text, tranRef, user, token,
            (response, new_amount) =>
            {
                GlobalState.SetUserWalletBalance(new_amount);
                wallet_balance_deposit.text = $"WALLET BALANCE: N {new_amount}";
                main_wallet_balance.text = $"WALLET BALANCE: N {new_amount}";
                wallet_balance_withdrawal.text = $"WALLET BALANCE: N {new_amount}";

                tranRef = "";
                SetLoading(deposit_loading, deposit_success, deposit_back_button, false);
            },
            (error) =>
            {
                ClearFields();
                SetLoading(deposit_loading,deposit_failed, deposit_back_button, false);
                Debug.Log(error);
            }
            );
    }

    //    string account_number = "09237234234";

    public void Withdraw()
    {
        User user = GlobalState.GetUser();

        float withdrawalFee = Env.WITHDRAWAL_FEE;

        float amount_to_withdrawal = float.Parse(withdrawal_amount.text);

        if (!WithdrawController.CheckIfUserBalanceIsEnough(user, amount_to_withdrawal + withdrawalFee))
        {
            Debug.Log("Insufficient Funds");
            return;
        }

        string token = GlobalState.GetToken();

        SetLoading(withdraw_loading, withdraw_form, withdraw_back_button, true);

        WithdrawController.WithdrawToBank(user, token, amount_to_withdrawal, bankName, bankCode, accountNumber.text,
            (response, newAmount) => {
                Debug.Log(response);

                GlobalState.SetUserWalletBalance(newAmount);
                wallet_balance_deposit.text = $"WALLET BALANCE: N {newAmount}";
                main_wallet_balance.text = $"WALLET BALANCE: N {newAmount}";
                wallet_balance_withdrawal.text = $"WALLET BALANCE: N {newAmount}";

                SetLoading(withdraw_loading, withdraw_success, withdraw_back_button, false);
            },
            (error) => {
                ClearFields();
                Debug.Log(error);
                SetLoading(withdraw_loading, withdraw_failed, withdraw_back_button, false);
            }
        );
    }


    private void DropdownValueChanged(int newPosition)
    {
        bankCode = banks.Values.ElementAt(newPosition);
        bankName = banks.Keys.ElementAt(newPosition);
    }

    void SetLoading(GameObject loader, GameObject panel, GameObject button, bool status)
    {
        if (status == true)
        {
            loader.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);
            button.SetActive(false);
        }
        else
        {
            loader.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            button.SetActive(true);
        }
    }

    public void ResetDeposit()
    {
        deposit_form.gameObject.SetActive(true);
        deposit_failed.gameObject.SetActive(false);
        deposit_loading.gameObject.SetActive(false);
        deposit_otp_form.gameObject.SetActive(false);
        deposit_success.gameObject.SetActive(false);
        deposit_back_button.SetActive(true);
    }

    public void ResetWithdrawal()
    {
        withdraw_form.gameObject.SetActive(true);
        withdraw_failed.gameObject.SetActive(false);
        withdraw_loading.gameObject.SetActive(false);
        withdraw_success.gameObject.SetActive(false);
        withdraw_back_button.SetActive(true);
    }
}
