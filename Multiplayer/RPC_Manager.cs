using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPC_Manager
{
    DataManager dataManager;

    PhotonView photonView;

    public RPC_Manager(DataManager _dataManager, PhotonView _photonView)
    {
        dataManager = _dataManager;
        photonView = _photonView;
    }
    
}
