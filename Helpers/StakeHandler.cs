using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StakeHandler : MonoBehaviour
{
    public Text walletBalance;

    public GameObject form;
    public GameObject loading;
    //public GameObject success;
    public GameObject failed;

    public GameObject back_button;

    public Text twoplayer;
    public Text threeplayer;
    public Text fourplayer;
    public Text sixplayer;

    public int lobbySceneIndex;


    // Use this for initialization
    void Start()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    string DisplayText(int numberfplayers, float stake, float win)
    {
        return $"{numberfplayers} Players Stake N{stake} Win N{win}";
    }

    string GetValues(int numberfplayers)
    {
        float stake = StakeController.GetStakeAmount(numberfplayers);
        float win = StakeController.GetWinAmount(numberfplayers);

        return DisplayText(numberfplayers, stake, win);
    }

    public void Stake(int numberfplayers)
    {
        SetLoading(loading, form, back_button, true);
        User user = GlobalState.GetUser();
        string token = GlobalState.GetToken();

        StakeController.StakeForGame(numberfplayers, user, token,
            (response) => {
                walletBalance.text = $"WALLET BALANCE: N {response}";
                //SetLoading(loading, form, back_button, false);
                SceneManager.LoadScene(sceneBuildIndex: lobbySceneIndex);
            },
            (error) => {
                SetLoading(loading, failed, back_button, true);
                Debug.Log(error);
            }
        );
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


    public void ResetStake()
    {
        loading.gameObject.SetActive(false);
        failed.gameObject.SetActive(false);
        form.gameObject.SetActive(true);
    }
}
