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
    private string bankUid;

    private List<BankModel> banks;

    private BankModel currentBank;

    //public GameObject mainMenu;

    public GameObject MenuManager;

    public GameObject DepositPanel;
    public GameObject LogoPanel;


    MenuManager menuManager;

    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManager");

        menuManager = MenuManager.GetComponent<MenuManager>();

        GetBanksList();


        banksList.onValueChanged.AddListener(delegate {
            DropdownValueChanged(banksList.value);
        });

    }

    private void Awake()
    {

    }

    void ClearFields()
    {
        amount.text = "";
    }

    public void InitiateDeposit()
    {
        string[] fields = { amount.text };

        Validate.CheckEmptyFields(fields,
            (message) => { menuManager.StartCoroutine(menuManager.showPopUpT(message, "error")); },
            () =>
            {
                SetLoading(deposit_loading, deposit_form, deposit_back_button, true);
                float_amount = float.Parse(amount.text);

                DepositFunds.DepositUserFunds(
                    new DepositPayload(float_amount),
                    (response) => {
                        DepositResponse depositResponse = (DepositResponse)response;
                        Application.OpenURL(depositResponse.url);

                        SetLoading(deposit_loading, deposit_form, deposit_back_button, false);
                        ResetDeposit();

                        DepositPanel.SetActive(false);
                        LogoPanel.SetActive(true);
                    },
                    (statusCode, error) => {
                        SetLoading(deposit_loading, deposit_form, deposit_back_button, false);
                        if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                        {
                            ValidationError validationError = (ValidationError)error;
                            menuManager.StartCoroutine(menuManager.showPopUpT(validationError.errors.First().Value[0], "error"));
                        }
                        else
                        {
                            GenericError genericError = (GenericError)error;
                            menuManager.StartCoroutine(menuManager.showPopUpT(genericError.message, "error"));
                        }
                    }
                );
            });
    }

    public void Withdraw()
    {
        string[] fields = { withdrawal_amount.text, accountNumber.text};

        if (currentBank == null)
        {
            menuManager.StartCoroutine(menuManager.showPopUpT("Select a bank", "error"));
            return;
        }

        Validate.CheckEmptyFields(
            fields,
            (message) => { menuManager.StartCoroutine(menuManager.showPopUpT(message, "error"));
                //ResetWithdrawal();
            },
            () =>
            {
                SetLoading(withdraw_loading, withdraw_form, withdraw_back_button, true);

                InitiateWithdrawal.Withdraw(
                    new WithdrawalPayload(float.Parse(withdrawal_amount.text), accountNumber.text, currentBank.uid),

                    (response) => {
                        BalanceResponse depositResponse = (BalanceResponse)response;

                        withdraw_form.SetActive(false);

                        Debug.Log(depositResponse.balance);

                        State.UserProfile.naira_balance = depositResponse.balance;

                        SetLoading(withdraw_loading, withdraw_success, withdraw_back_button, false);
                    },

                    (statusCode, error) => {
                        SetLoading(withdraw_loading, withdraw_failed, withdraw_back_button, false);
                        if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                        {
                            ValidationError validationError = (ValidationError)error;
                            menuManager.StartCoroutine(menuManager.showPopUpT(validationError.errors.First().Value[0], "error"));
                        }
                        else
                        {
                            GenericError genericError = (GenericError)error;
                            menuManager.StartCoroutine(menuManager.showPopUpT(genericError.message, "error"));
                        }
                    }
                    );


            });
    }

    private void GetBanksList()
    {
        GetAllBanks.AllBanks(

            (response) => {
                BanksResponse depositResponse = (BanksResponse)response;

                banks = depositResponse.banks;

                banksList.options.Clear();
                //fill the dropdown menu OptionData with all COM's Name in ports[]
                foreach (BankModel bm in banks)
                {
                    banksList.options.Add(new Dropdown.OptionData() { text = bm.name });
                }
            },

            (statusCode, error) => {
                if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                {
                    ValidationError validationError = (ValidationError)error;
                    menuManager.StartCoroutine(menuManager.showPopUpT(validationError.errors.First().Value[0], "error"));
                }
                else
                {
                    GenericError genericError = (GenericError)error;
                    menuManager.StartCoroutine(menuManager.showPopUpT(genericError.message, "error"));
                }
            }

        );
    } 

    private void DropdownValueChanged(int newPosition)
    {
        currentBank = banks.ElementAt(newPosition);
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
        ClearFields();
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
