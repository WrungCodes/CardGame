using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

	List<Player> players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Player
{
	public string username;
	public string status;
	public bool isTurn;

	public List<Card> cards;

    public void SetTurn()
    {
        isTurn = true;
    }

    public void EndTurn()
    {
        isTurn = false;
    }

    public void AddCards(List<Card> _cards)
    {
        cards.AddRange(_cards);
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}
