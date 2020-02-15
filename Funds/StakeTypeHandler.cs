using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StakeTypeHandler : MonoBehaviour
{
    private List<GameObject> types = new List<GameObject>();

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    public GameObject loading;
    public GameObject failed;

    public GameObject mainPanel;
    public GameObject back_button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateStakeType(StakeTypeModel stakeTypeModel)
    {
        GameObject obj = Instantiate(scrollItemPrefab);
        obj.transform.SetParent(scrollContent.transform, false);

        obj.GetComponent<StakeProcess>().StakeType = stakeTypeModel;

        obj.transform.Find("Players").gameObject.GetComponent<Text>().text = $"{stakeTypeModel.number_of_players} Players" ;
        obj.transform.Find("Win").gameObject.GetComponent<Text>().text = $"Win N{stakeTypeModel.win_amount}";
        obj.transform.Find("Stake").gameObject.GetComponent<Text>().text = $"Stake  N{stakeTypeModel.stake_amount}";

        types.Add(obj);
    }

    public void GetStakeTypesForDisplay()
    {
        foreach (GameObject gobject in types)
        {
            Destroy(gobject);
        }

        loading.SetActive(true);
        GetStakeType.GetAllStakeType(
            (response) => {

                loading.SetActive(false);
                mainPanel.SetActive(true);

                StakeTypesResponse stakeTypesResponse = (StakeTypesResponse)response;

                foreach(StakeTypeModel stakeTypeModel in stakeTypesResponse.stake_type)
                {
                    GenerateStakeType(stakeTypeModel);
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


}
