﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObj : MonoBehaviour
{
    public Suits Suit = Suits.None;
    public Ranks Rank = Ranks.None;

    public GameObject CardFace;
    public GameObject CardBack;

    public SpriteRenderer smallShape1;
    public SpriteRenderer smallShape2;

    public SpriteRenderer bigShape;

    public SpriteRenderer number1;
    public SpriteRenderer number2;

    ImageDisplay ImageDisplay;

    CardAnimator cardAnimator;

    public bool isActiveCard = false;

    public Card owncard;

    //public GameObject PlayinDeck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        ImageDisplay = GameObject.FindWithTag("ImageDisplay").GetComponent<ImageDisplay>();
        cardAnimator = GameObject.FindWithTag("CardsPool").GetComponent<CardAnimator>();
    }

    void OnMouseDown() {
        cardAnimator.ReturnAllCardsToDefualt();

        if (this.gameObject.transform.IsChildOf(cardAnimator.gameObject.transform))
        {
            cardAnimator.AskForMarket();
        }
        else
        {
            bool isMineCard = this.gameObject.GetComponentInParent<PlayerCardList>().isMine;
            if (isMineCard)
            {
                if (isActiveCard == false && isMineCard)
                {
                    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + Constants.CARDS_ACTIVE_OFFSET);
                    isActiveCard = true;
                }

                if (isActiveCard == true)
                {
                    cardAnimator.PlayCard(this.gameObject);
                }
            }

        }
        //if (this.gameObject.transform.parent == cardAnimator.gameObject)
        //{
        //}

    }

    public void returnCardToDefualt()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - Constants.CARDS_ACTIVE_OFFSET);
        isActiveCard = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCardFacedUp()
    {
        CardFace.SetActive(true);
        CardBack.SetActive(false);
    }

    public void SetCardFacedDown()
    {
        CardFace.SetActive(true);
        CardBack.SetActive(false);
    }

    public void SetCardValue(Card card)
    {
        Suit = card.Suit;
        Rank = card.Rank;

        SetImage(card);

        owncard = card;
    }

    private void SetImage(Card card)
    {
        ImageDisplay.GetRankImage(card);

        smallShape1.sprite = ImageDisplay.GetSuitImage(card);
        smallShape2.sprite = ImageDisplay.GetSuitImage(card);

        bigShape.sprite = ImageDisplay.GetSuitImage(card);

        number1.sprite = ImageDisplay.GetRankImage(card);
        number2.sprite = ImageDisplay.GetRankImage(card);
    }

    public Card GetCard()
    {
        return owncard;
    }

    public void AddCardtoPlayingDeck()
    {
        //PlayinDeck
    }

    //public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    //{
    //    var currentPos = transform.position;
    //    var t = 0f;
    //    while (t < 1)
    //    {
    //        t += Time.deltaTime / timeToMove;
    //        transform.position = Vector3.Lerp(currentPos, position, t);
    //        yield return null;
    //    }
    //}
}
