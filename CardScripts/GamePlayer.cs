﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePlayer: MonoBehaviour
{
    public string username;
    private PhotonView PhotonView;
    //public string status;

    //public bool wasTurn = false;
    //public bool isTurn = false;
    //public bool isTurnNext = false;

    //private Player player;


    //public int player_no;
    public List<Card> cards;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    //public GamePlayer(string _username, int _player_no)
    //{
    //    username = _username;
    //    player_no = _player_no;
    //    isTurn = false;

    //    cards = new List<Card>();
    //}

    //public void SetTurn()
    //{
    //    isTurn = true;
    //}

    //public void EndTurn()
    //{
    //    isTurn = false;
    //}

    //public void AddCards(List<Card> _cards)
    //{
    //    cards.AddRange(_cards);
    //}

    //public void AddCard(Card _card)
    //{
    //    cards.Add(_card);
    //}

    //public Card RemoveCard(Card card)
    //{
    //    int index = cards.IndexOf(card);
    //    cards.RemoveAt(index);
    //    return card;
    //}

    //public int GetCard(Card card)
    //{
    //    return cards.IndexOf(card);
    //}

    //public int GetCardNumber()
    //{
    //    return cards.Count;
    //}

    //public List<Card> GetCards()
    //{
    //    return cards;
    //}
}