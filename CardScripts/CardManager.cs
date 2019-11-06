using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject CardPrefab;

    public GameObject SpawnPointOne;

    public GameObject SpawnPointTwo;

    //public CardDisplay DisplayCard;

    Deck<Card> deck;
    public Deck<Card> player_one_deck;
    public Deck<Card> player_two_deck;
    public Deck<Card> playing_deck;

    void Awake()
    {
        List<Card> cards = new List<Card>();
        //List<Card> player_one_cards;
        //List<Card> player_two_cards;
        //List<Card> playing_cards = new List<Card>();
        //Card playing_card;

        for (Card.Shape s = Card.Shape.circle; s <= Card.Shape.star; ++s)
        {
            for (Card.Rank r = Card.Rank.one; r <= Card.Rank.fourteen; ++r)
            {
                cards.Add(new Card(s, r, Card.Type.normal));
            }
        }

        RemoveInvalidCards(cards);
        AddWhotCards(cards);
        //cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        //cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        //cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        //cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));

        deck = new Deck<Card>(cards);
        deck.Shuffle();

        Card first_card = deck.DrawSingleCard();

        Card second_card = deck.DrawSingleCard();
        //ca.ShowCard(first_card);

        //GameObject go = Instantiate(A, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //go.transform.parent = GameObject.Find("Stage Scroll").transform;

        GameObject go = Instantiate(CardPrefab,
           new Vector2(SpawnPointOne.transform.position.x, SpawnPointOne.transform.position.y),
            Quaternion.identity) as GameObject;
        go.transform.parent = GameObject.Find("Hand").transform;
        go.GetComponent<CardDisplay>().ShowCard(first_card);
        //DisplayCard.ShowCard(first_card);

        GameObject go2 = Instantiate(CardPrefab,
           new Vector2(SpawnPointTwo.transform.position.x, SpawnPointTwo.transform.position.y),
            Quaternion.identity) as GameObject;
        go2.transform.parent = GameObject.Find("Hand").transform;
        go2.GetComponent<CardDisplay>().ShowCard(second_card);

        Debug.Log(CardName(first_card) + " --- " + CardName(second_card) + "  " + CompareCard(first_card, second_card));

        //player_one_cards = deck.Draw(5);
        //player_one_deck = new Deck<Card>(player_one_cards);

        //player_two_cards = deck.Draw(5);
        //player_two_deck = new Deck<Card>(player_two_cards);

        //playing_card = deck.DrawSingleCard();

        //playing_cards.Add(playing_card);
        //playing_deck = new Deck<Card>(playing_cards);

        //int a = 0;
        //foreach (Card card in cards)
        //{         
        //    Debug.Log(a + " --- " + card.GetCardShape() + '-' + card.GetCardRank());
        //    a++;
        //}

        //Debug.Log(playing_card.GetCardShape() + '-' + playing_card.GetCardRank());
        //Abilities(playing_card);
        //Debug.Log(cards);

    }

    public string CardName(Card card)
    {
        return card.GetCardShape() + '-' + card.GetCardRank();
    }

    public bool CompareCard(Card first_card, Card second_card)
    {
        if(CompareCardShape(first_card, second_card) || CompareCardRank(first_card, second_card))
        {
            return true;
        }
        return false;
    }

    public bool CompareCardShape(Card first_card, Card second_card)
    {
        if (first_card.GetCardShape() == second_card.GetCardShape())
        {
            return true;
        }
        return false;
    }

    public bool CompareCardRank(Card first_card, Card second_card)
    {
        if (first_card.GetCardRank() == second_card.GetCardRank())
        {
            return true;
        }
        return false;
    }


    public void RemoveInvalidCards(List<Card> cards)
    {
        // Removing the star cards
        cards.RemoveRange(55, 5);

        // Removing the crosses cards
        cards.RemoveAt(27);
        cards.RemoveAt(30);
        cards.RemoveAt(33);

        // Removing the Square cards
        cards.RemoveAt(15);
        cards.RemoveAt(18);
        cards.RemoveAt(21);
    }

    public void AddWhotCards(List<Card> cards)
    {
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
        cards.Add(new Card(Card.Shape.whot, Card.Rank.whot, Card.Type.jackpot));
    }

    public bool IsActionCard(Card card)
    {
        if( card.GetRank() == Card.Rank.fourteen
            || card.GetRank() == Card.Rank.eight
            || card.GetRank() == Card.Rank.five
            || card.GetRank() == Card.Rank.two
            || card.GetRank() == Card.Rank.one
            || card.GetRank() == Card.Rank.whot
            )
        {
            return true;
        }
        return false;
    }

    public void GoToMarket()
    {

    }

    public void BlockPickTwoCard()
    {

    }

    public void BlockPickThreeCard()
    {

    }

    public void Abilities(Card card)
    {
        //
        switch (card.GetRank())
        {
            case Card.Rank.fourteen:
                // everyone draws a card
                Debug.Log("General Market");
                return;

            case Card.Rank.eight:
                // skip next person turn
                Debug.Log("Suspension");
                return;

            case Card.Rank.five:
                // next person draws 5 cards
                Debug.Log("Pick 5");
                return;

            case Card.Rank.two:
                // next person draws 2 cards
                Debug.Log("Pick 2");
                return;

            case Card.Rank.one:
                // skip everyone turn
                Debug.Log("Hold on");
                return;

            case Card.Rank.whot:
                // call for any card you want
                Debug.Log("I need");
                return;

            default:
                // next turn
                Debug.Log("Nothing");
                return;
        }
    }
}
