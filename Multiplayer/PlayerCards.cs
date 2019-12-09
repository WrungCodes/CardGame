using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerCards
{
    public List<Card> cards;
    public string playerName;

    public PlayerCards(string _playerName, List<Card> _cards)
    {
        playerName = _playerName;
        cards = _cards;
    }
}