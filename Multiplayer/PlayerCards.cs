using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;


[Serializable]
public class PlayerCards
{
    public List<Card> cards;
    public string playerName;
    public bool isTurn = false;

    public Player player;

    public void SetToTurn()
    {
        isTurn = true;
    }

    public void UnSetTurn()
    {
        isTurn = false;
    }

    public List<Card> GetAllCards()
    {
        return cards;
    }

    public void AddCards(Card card)
    {
        cards.Add(card);
    }

    public void RemoveCards(Card card)
    {
        cards.Remove(card);
    }

    public PlayerCards(string _playerName, List<Card> _cards, Player _player, bool _isTurn)
    {
        playerName = _playerName;
        cards = _cards;
        player = _player;
        isTurn = _isTurn;
    }
}