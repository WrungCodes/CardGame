using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : IEquatable<Card>
{
    //private CardSerializer cardSerializer;

    public Ranks Rank;

    public Suits Suit;

    public Card(Ranks ranks, Suits suits)
    {
        Rank = ranks;
        Suit = suits;
    }

    public Card(CardSerializer _cardSerializer)
    {
        Rank = SetCardRank(_cardSerializer.Rank);
        Suit = SetCardSuit(_cardSerializer.Suit);
    }

    public Ranks GetRank()
    {
        return Rank;
    }

    public Suits GetSuit()
    {
        return Suit;
    }

    public void SetCardFromCardSerializer(CardSerializer _cardSerializer)
    {
        SetCardRank(_cardSerializer.Rank);
        SetCardSuit(_cardSerializer.Suit);
    }

    private Ranks SetCardRank(string _rank)
    {
        return (Ranks)Enum.Parse(typeof(Ranks), _rank); //_rank;
    }

    private Suits SetCardSuit(string _suit)
    {
        return (Suits)Enum.Parse(typeof(Suits), _suit);
    }

    public CardSerializer ConvertCardToCardSerializer()
    {
        return new CardSerializer(Rank.ToString(), Suit.ToString());
    }

    public bool Equals(Card other)
    {
        return this.Rank == other.Rank && this.Suit == other.Suit;
    }
}
