﻿//using System.Collections;
//using System.Collections.Generic;
//using Photon.Realtime;
//using UnityEngine;

//public class Manager : MonoBehaviour
//{
//    List<GamePlayer> players;

//    Deck market;

//    Deck playingCard;

//    Card currentCard;

//    Deck allCards;

//    //private Player currentPlayer;


//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    public Card SetCurrentCard(Card card)
//    {
//        currentCard = card;
//        return card;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void ResetTurns()
//    {
//        for (int i = 0; i < players.Count; i++)
//        {
//            if (i == 0)
//            {
//                players[i].isTurn = true;
//                //players[i].wasTurn = false;
//                players[i].isTurnNext = false;
//            }
//            else if (i == 1)
//            {
//                players[i].isTurn = false;
//                //players[i].wasTurn = false;
//                players[i].isTurnNext = true;
//            }
//            else
//            {
//                players[i].isTurn = false;
//                //players[i].wasTurn = false;
//                players[i].isTurnNext = false;
//            }
//        }
//    }

//    void UpdateTurns()
//    {
//        for (int i = 0; i < players.Count; i++)
//        {
//            if (players[i].isTurnNext)
//            {
//                players[i].isTurn = true;
//                players[i].isTurnNext = false;

//                if (players.Count - 1 == i)
//                {
//                    players[0].isTurnNext = true;
//                }
//                else
//                {
//                    players[i + 1].isTurnNext = true;
//                    players[i + 1].isTurn = false;
//                }
//            }
//        }
//    }

//    void RemoveInvalidCards(List<Card> cards)
//    {
//        // Removing the star cards
//        cards.RemoveRange(55, 5);

//        // Removing the crosses cards
//        cards.RemoveAt(27);
//        cards.RemoveAt(30);
//        cards.RemoveAt(33);

//        // Removing the Square cards
//        cards.RemoveAt(15);
//        cards.RemoveAt(18);
//        cards.RemoveAt(21);
//    }

//    void AddWhotCards(List<Card> cards)
//    {
//        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
//        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
//        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
//        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
//    }

//    public void StartCardShuffle()
//    {
//        List<Card> cards_list = new List<Card>();

//        for (Card.Shape s = Card.Shape.circle; s <= Card.Shape.star; ++s)
//        {
//            for (Card.Rank r = Card.Rank.one; r <= Card.Rank.fourteen; ++r)
//            {
//                cards_list.Add(new Card(s, r, Card.Type.normal));
//            }
//        }

//        RemoveInvalidCards(cards_list);
//        AddWhotCards(cards_list);

//        allCards = new Deck();
//        allCards.AddCards(cards_list);
//        allCards.Shuffle();
//    }

//    void DealCardsToPlayers(Deck cards)
//    {
//        for (int i = 0; i < GameEnv.NO_OF_START_CARDS; i++)
//        {
//            foreach (GamePlayer player in players)
//            {
//                Card drawnCard = cards.DrawSingleCard();
//                player.AddCard(drawnCard);
//            }
//        }
//    }

//    void SetPlayingCards(Deck deck)
//    {
//        Card drawn_card = deck.DrawSingleCard();
//        playingCard = new Deck();
//        playingCard.AddCard(drawn_card);
//        currentCard = drawn_card;
//    }

//    void FillMarketDeck(Deck deck)
//    {
//        List<Card> drawnCards = deck.DrawCards(deck.CardCount());
//        market = new Deck();
//        market.AddCards(drawnCards);
//    }

//}
