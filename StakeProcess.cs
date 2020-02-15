using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StakeProcess : MonoBehaviour
{
    public StakeTypeModel StakeType { get; set; }

    public GameObject MenuManager;

    GameObject StakePanel;

    MenuManager menuManager;

    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManager");
        StakePanel = GameObject.FindWithTag("StakePanel");
        menuManager = MenuManager.GetComponent<MenuManager>();
    }

    public void ValidateUserCanStake()
    {
        menuManager.SetLoading(StakePanel);
        ValidateStake.ValidateStakeAmount(
            new StakePayload(StakeType.uid),
            (response) => {
                ValidateStakeResponse validateStakeResponse = (ValidateStakeResponse)response;
                State.CurrentStake = StakeType;
                SceneManager.LoadScene(sceneBuildIndex: Scenes.CONNECTING_TO_NETWORK_SCENE);
            },
            (statusCode, error) => {
                menuManager.UnSetLoading(StakePanel);
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


}
