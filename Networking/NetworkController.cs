using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName =State.UserProfile.username;
        //PlayerNetwork.Instance.PlayerName;
        Debug.Log(PhotonNetwork.NickName + " Connected to " + PhotonNetwork.CloudRegion + " Server");
        PhotonPeer.RegisterType(typeof(CardSerializer), (byte)'M', CardSerializer.Serialize, CardSerializer.Deserialize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
