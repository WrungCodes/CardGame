using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public List<Card> playerCards;

    public Card currentCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayerCards(CardSerializer _cardSerializer)
    {
        playerCards.Add(new Card(_cardSerializer));
    }

    public void RemoveFromPlayerCards(Card card)
    {
        playerCards.Remove(card);
    }


}
