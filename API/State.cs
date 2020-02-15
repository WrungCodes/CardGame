using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public static State Instance { get; set; }

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

        monoBehaviour = GetComponent<MonoBehaviour>();

    }



    public static MonoBehaviour monoBehaviour { get; set; }

    public static bool IsLoading { get; set; }

    public static UserModel User { get; set; }

    public static ProfileModel UserProfile { get; set; }

    public static StakeTypeModel CurrentStake { get; set; }

    public static Request CurrentRequest { get; set; }

    public static FullRequest PendingFullRequestWhileRefreshingToken { get; set; }

    public static GameModel GameModel { get; set; }

    //public static string Token { get; set; }

}
