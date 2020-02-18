using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;

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

    GameObject PlayingDeck;

    public bool isActiveCard = false;

    public Card owncard;

    private int fingerID = -1;
    //public GameObject PlayinDeck;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(fingerID) == false)
        {
            return;
        }
    }

    private void Awake()
    {
        #if !UNITY_EDITOR
             fingerID = 0; 
        #endif
        ImageDisplay = GameObject.FindWithTag("ImageDisplay").GetComponent<ImageDisplay>();
        cardAnimator = GameObject.FindWithTag("CardsPool").GetComponent<CardAnimator>();
        PlayingDeck = GameObject.FindWithTag("PlayingDeck");
    }

    void OnMouseDown() {

        if (!cardAnimator.iscardDealt)
        {
            return;
        }

        if (cardAnimator.isMovingToPosition)
        {
            return;
        }

        if (cardAnimator.isPlayingCardPopUp == true|| cardAnimator.isPlayCardPopUp == true || cardAnimator.isGotoMarketPopUp == true) {
            cardAnimator.RemovePlayCardPopUp();
            cardAnimator.RemovePlayingCardPopUp();
            cardAnimator.RemoveMarketPopUp();
            //return;
        }

        cardAnimator.ReturnAllCardsToDefualt();

        if (this.gameObject.transform.IsChildOf(cardAnimator.gameObject.transform))
        {
            // Ask For Market POP UP
            cardAnimator.isGotoMarketPopUp = true;
            cardAnimator.SetMarketPopUp();
            //cardAnimator.AskForMarket();
        }

        if (this.gameObject.transform.IsChildOf(PlayingDeck.transform))
        {
            // Ask For Market POP UP
            cardAnimator.isPlayingCardPopUp = true;
            cardAnimator.SetPlayingCardPopUp();
            //cardAnimator.AskForMarket();
        }


        else
        {
            bool isMineCard = this.gameObject.GetComponentInParent<PlayerCardList>().isMine;
            if (isMineCard)
            {
                if (isActiveCard == false && isMineCard)
                {
                    this.gameObject.transform.SetSiblingIndex(0);

                    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + Constants.CARDS_ACTIVE_OFFSET);
                    isActiveCard = true;
                }

                if (isActiveCard == true)
                {
                    // Ask to play POP UP
                    cardAnimator.SetPlayCardPopUp(this.owncard, this.gameObject);
                    cardAnimator.isPlayingCardPopUp = true;
                    //cardAnimator.PlayCard(this.gameObject);
                }
            }
        }
    }

    public void returnCardToDefualt()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - Constants.CARDS_ACTIVE_OFFSET);
        isActiveCard = false;
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

    }
}
