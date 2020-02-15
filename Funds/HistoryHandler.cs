using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class HistoryHandler : MonoBehaviour
{

    private List<GameObject> trans = new List<GameObject>();

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    public GameObject loading;
    public GameObject failed;

    public GameObject back_button;

    //public List<Transaction> transactions;
    // Use this for initialization
    void Start()
    {
        //scrollView
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayTransaction()
    {
        foreach (GameObject gobject in trans)
        {
            Destroy(gobject);
        }

        loading.SetActive(true);
        GetHistory.GetUserHistory(
            (response) => {

                loading.SetActive(false);

                HistoryResponse historyResponse = (HistoryResponse)response;

                foreach (HistroyModel histroy in historyResponse.history)
                {
                    GenerateHistroyItem(histroy);
                }

            },
            (statusCode, error) => {

                loading.SetActive(false);
                failed.SetActive(true);

                if (statusCode == StatusCodes.CODE_VALIDATION_ERROR)
                {
                    ValidationError validationError = (ValidationError)error;
                }
                else
                {
                    GenericError genericError = (GenericError)error;
                }
            }
        );
    }

    private void GenerateHistroyItem(HistroyModel histroy)
    {
        Debug.Log(histroy.type);
        GameObject obj = Instantiate(scrollItemPrefab);
        obj.transform.SetParent(scrollContent.transform, false);
        obj.transform.Find("TransType").gameObject.GetComponent<Text>().text = histroy.type.ToUpper();
        obj.transform.Find("TransAmount").gameObject.GetComponent<Text>().text = ChoseSign(histroy.type) + " NGN " + String.Format("{0}", histroy.amount);

        obj.transform.Find("TransAmount").gameObject.GetComponent<Text>().color = ChoseColor(histroy.type);
        obj.transform.Find("TransTime").gameObject.GetComponent<Text>().text = histroy.created_at;
        //transaction.time

        trans.Add(obj);
    }

    public void DisplayTransaction11()
    {
        User user = GlobalState.GetUser();
        string token = GlobalState.GetToken();
        loading.SetActive(true);

        back_button.SetActive(false);

        HistoryController.GetAllTransactions(user, token,
            (response) =>
            {
                foreach (var a in response)
                {
                    GenerateTransactionItem(a);
                }
                scrollView.verticalNormalizedPosition = 1;
                loading.SetActive(false);
                back_button.SetActive(true);
            },
            (error) =>
            {
                Debug.Log(error.StackTrace);
                loading.SetActive(false);
                failed.SetActive(true);
                back_button.SetActive(true);
            }
        );
    }

    void GenerateTransactionItem(Transaction transaction)
    {
        GameObject obj = Instantiate(scrollItemPrefab);
        obj.transform.SetParent(scrollContent.transform, false);
        obj.transform.Find("TransType").gameObject.GetComponent<Text>().text = transaction.type.ToUpper();
        obj.transform.Find("TransAmount").gameObject.GetComponent<Text>().text = ChoseSign(transaction.type) + " NGN " + CalculateAmount(transaction);
        obj.transform.Find("TransID").gameObject.GetComponent<Text>().text = transaction.id;

        obj.transform.Find("TransAmount").gameObject.GetComponent<Text>().color = ChoseColor(transaction.type);
        obj.transform.Find("TransTime").gameObject.GetComponent<Text>().text = transaction.time;
        //transaction.time

        trans.Add(obj);
    }

    string ChoseSign(string type)
    {
        switch (type)
        {
            case "deposit":
                return "+";
            case "win":
                return "+";
            case "refund":
                return "+";
            case "withdrawal":
                return "-";
            case "stake":
                return "-";
            default:
                return "";
        }
    }

    Color ChoseColor(string type)
    {
        switch (type)
        {
            case "deposit":
                return Color.green;
            case "win":
                return Color.green;
            case "refund":
                return Color.green;
            case "withdrawal":
                return Color.red;
            case "stake":
                return Color.red;
            default:
                return Color.black;
        }
    }

    float CalculateAmount(Transaction transaction)
    {
        switch (transaction.type)
        {
            case "deposit":
                return transaction.final_amount - transaction.initial_amount;
            case "withdrawal":
                return transaction.initial_amount - transaction.final_amount;
            case "stake":
                return transaction.initial_amount - transaction.final_amount;
            default:
                return 0;
        }
    }

    public void DeleteEntries()
    {
        //trans.L;
        foreach (GameObject transItem in trans)
        {
            Destroy(transItem);
        }
        trans.Clear();
    }

    //void RestartGame(){
    ////gets all elements of the parent 
    //InputField [] childElements = letterPanel.GetComponentsInChildren<InputField> ();
    //Debug.Log (childElements.Count());
    //foreach (vInputField item in childElements) {
    //    if(item.name.Contains("InputField")){
    //        Destroy (item.gameObject);
    //    }
    //}

}
