using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer
{
    public CardPlayer(Player photonPlayer)
    {
        PhotonPlayer = photonPlayer;
    }

    public readonly Player PhotonPlayer;

    public List<Card> cards;

    public void AddCards(List<Card> _cards)
    {
        cards.AddRange(_cards);
    }
}