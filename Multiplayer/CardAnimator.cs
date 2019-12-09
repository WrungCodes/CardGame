﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Rendering;

public class CardAnimator : MonoBehaviour
{
    public GameObject cardPrefab;

    public GameObject PlayingDeck;


    public List<CardObj> AllCardsObj;

    public List<GameObject> PlayersObjectArray;

    public GameObject CardManager;

    public GameObject gamePlayerPrefab;

    public Transform sp1;
    public Transform sp2;
    //public Transform sp3;
    public Transform sp4;

    public Transform mainSp;

    bool iscardDealt = false;
    bool isInitialized = false;



    // Start is called before the first frame update
    void Start()
    {
        //GameObject a = Instantiate(gamePlayerPrefab, mainSp.position, Quaternion.identity);
        //a.GetComponent<PlayerCardList>().SetNameText("Mallo");
        SpawnPlayers();
        InitializeAllCard();
        isInitialized = true;

    }

    void SpawnPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Transform spawnTransform;

            if (player.GetNext() == PhotonNetwork.LocalPlayer)
            {
                spawnTransform = sp2;
            }

            if (PhotonNetwork.LocalPlayer.GetNext() == player)
            {
                spawnTransform = sp4;
            }

            if (player.IsLocal)
            {
                GameObject p = Instantiate(gamePlayerPrefab, mainSp.position, Quaternion.identity);
                p.GetComponent<PlayerCardList>().isMine = true;
                p.GetComponent<PlayerCardList>().SetNameText(player.NickName);
                PlayersObjectArray.Add(p);
            }
            else
            {
                GameObject p = Instantiate(gamePlayerPrefab, SpawnPlayersLocation(player).position, Quaternion.identity);
                p.GetComponent<PlayerCardList>().SetNameText(player.NickName);

                PlayersObjectArray.Add(p);
            }
        }
    }

    Transform SpawnPlayersLocation(Player player)
    {
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            if (player.GetNext() == PhotonNetwork.LocalPlayer)
            {
                 return sp2;
            }

            if (PhotonNetwork.LocalPlayer.GetNext() == player)
            {
                return sp4;
            }

            return sp1;
        }

        if(PhotonNetwork.PlayerList.Length == 3)
        {
            if (player.GetNext() == PhotonNetwork.LocalPlayer)
            {
                return sp2;
            }
            return sp4;
        }

        return sp1;
    }

    // Update is called once per frame
    void Update()
    {
        if (iscardDealt == false && isInitialized == true)
        {
            DealCardsToPlayerAsync();
            iscardDealt = true;
        }
    }

    private void InitializeAllCard()
    {
        for (int x = 1; x <= 53; x++)
        {
            GameObject card = Instantiate(cardPrefab,
                new Vector2(this.gameObject.transform.position.x + x/20, (this.gameObject.transform.position.y) + x / 20), Quaternion.identity);
            card.transform.parent = this.transform;
            AllCardsObj.Add(card.GetComponent<CardObj>());
        }
    }

    public async Task DealCardsToPlayerAsync()
    {
        //PlayersObjectArray = new List<GameObject>();
        //PlayersObjectArray.Add(p1);
        //PlayersObjectArray.Add(p2);
        //foreach(Player pl in PhotonNetwork.PlayerList)
        //{
        //    PlayersObjectArray
        //}
        int extralenght = 0;

        for (int x = 1; x <= Constants.PLAYER_INITIAL_CARDS; x++)
        {
            foreach (GameObject p in PlayersObjectArray)
            {
                StartCoroutine(MoveToPosition(AllCardsObj[AllCardsObj.Count - 1].gameObject.transform,
                    new Vector3(p.transform.position.x + extralenght, p.transform.position.y, 0)
                    , 0.6f));
                AllCardsObj[AllCardsObj.Count - 1].gameObject.transform.SetParent(p.transform);

                p.GetComponent<PlayerCardList>().cardObjs.Add(AllCardsObj[AllCardsObj.Count - 1]);

                AllCardsObj.Remove(AllCardsObj[AllCardsObj.Count - 1]);

                await Task.Delay(900);
            }

            extralenght += Constants.CARDS_SPACE_OFFSET;
        }

        Debug.Log("start");

        StartCoroutine(MoveToPosition(AllCardsObj[AllCardsObj.Count - 1].gameObject.transform, PlayingDeck.transform.position, 0.6f));
        PlayingDeck.GetComponent<PlayingDeck>().cardObjs.Add(AllCardsObj[AllCardsObj.Count - 1]);
        AllCardsObj[AllCardsObj.Count - 1].SetCardValue(CardManager.GetComponent<CardManager>().PlayingDeck[0]);
        AllCardsObj[AllCardsObj.Count - 1].SetCardFacedUp();

        AllCardsObj[AllCardsObj.Count - 1].gameObject.transform.SetParent(PlayingDeck.transform);

        AllCardsObj.Remove(AllCardsObj[AllCardsObj.Count - 1]);

        PlayerCards playercard = CardManager.GetComponent<CardManager>().AllPlayerList.Where(ap => ap.playerName == PhotonNetwork.NickName).First();
        int i = 0;

        GameObject player = PlayersObjectArray.Where(ap => ap.GetComponent<PlayerCardList>().isMine == true).First();

        foreach(CardObj cardobj in player.GetComponent<PlayerCardList>().cardObjs)
        {
            cardobj.SetCardValue(playercard.cards[i]);
            cardobj.SetCardFacedUp();
            i++;
        }

        SpaceAllCards();
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

    }

    public void ReturnAllCardsToDefualt()
    {
        GameObject player = PlayersObjectArray.Where(ap => ap.GetComponent<PlayerCardList>().isMine == true).First();

        foreach (CardObj cardobj in player.GetComponent<PlayerCardList>().cardObjs)
        {
            if (cardobj.isActiveCard == true)
            {
                cardobj.returnCardToDefualt();
            }
        }
    }

    public void SpawnAllCardsOnTheSpot()
    {
        GameObject player = PlayersObjectArray.Where(ap => ap.GetComponent<PlayerCardList>().isMine == true).First();

        foreach (CardObj cardobj in player.GetComponent<PlayerCardList>().cardObjs)
        {
            cardobj.gameObject.transform.position = player.transform.position;
        }
    }

    public void SpaceAllCards()
    {
        SpawnAllCardsOnTheSpot();
        int extraSpace = 0;
        GameObject player = PlayersObjectArray.Where(ap => ap.GetComponent<PlayerCardList>().isMine == true).First();

        foreach (CardObj cardobj in player.GetComponent<PlayerCardList>().cardObjs)
        {
            cardobj.gameObject.transform.position = new Vector3(
                cardobj.gameObject.transform.position.x
                + extraSpace
                ,
                cardobj.gameObject.transform.position.y
                );
            extraSpace += Constants.PLAYER_CARDS_SPACE_OFFSET ;
        }
    }

    public void AddCardtoPlayingDeck(Card card, string playerId)
    {
        GameObject playerObject = GetPlayerObject(playerId);

        List<CardObj> listOfPlayerCard = playerObject.GetComponent<PlayerCardList>().cardObjs;

        if (PhotonNetwork.LocalPlayer.NickName == playerId)
        {
            CardObj co = GetCardObj(listOfPlayerCard, card);
            listOfPlayerCard.Remove(co);
            AddCardToPlayingDeck(co);
            SpaceAllCards();
        }
        else
        {
            CardObj co = listOfPlayerCard[listOfPlayerCard.Count - 1];
            listOfPlayerCard.Remove(co);
            co.SetCardValue(card);
            co.SetCardFacedUp();
            AddCardToPlayingDeck(co);
        }
    }

    private void AddCardToPlayingDeck(CardObj co)
    {
        PlayingDeck.GetComponent<PlayingDeck>().cardObjs.Add(co);
        MoveCard(co.gameObject, PlayingDeck.transform.position);
        co.gameObject.transform.SetSiblingIndex(0);
        co.gameObject.transform.SetParent(PlayingDeck.transform);
        co.gameObject.transform.SetSiblingIndex(0);
        //co.gameObject.GetComponent<SortingGroup>().sortingOrder = 1;

    }

    public void MoveCard(GameObject cardObject, Vector3 destinationGameObject)
    {
        StartCoroutine(MoveToPosition(cardObject.transform, destinationGameObject, 0.6f));
    }

    public void AskForMarket()
    {
        Debug.Log("Startied");
        CardManager cm = CardManager.GetComponent<CardManager>();

        cm.DealCardFromMarket();
    }

    public void AddCardToPlayerDeck(Card card, string playerId)
    {
        GameObject playerObject = GetPlayerObject(playerId);

        List<CardObj> listOfPlayerCard = playerObject.GetComponent<PlayerCardList>().cardObjs;

        if (PhotonNetwork.LocalPlayer.NickName == playerId)
        {
            CardObj co = AllCardsObj[AllCardsObj.Count - 1];
            co.SetCardValue(card);
            co.SetCardFacedUp();
            AllCardsObj.Remove(co);

            CardObj lastCardObject = listOfPlayerCard[listOfPlayerCard.Count - 1];

            listOfPlayerCard.Add(co);

            if (listOfPlayerCard.Count == 0)
            {
                MoveCard(co.gameObject, playerObject.transform.position);
            }
            else
            {
                MoveCard(co.gameObject, new Vector3(
                lastCardObject.gameObject.transform.position.x + (1 * Constants.CARDS_SPACE_OFFSET),
                (lastCardObject.gameObject.transform.position.y)
                ));
            }

            co.gameObject.transform.SetParent(playerObject.transform);

            SpaceAllCards();
        }
        else
        {
            CardObj co = AllCardsObj[AllCardsObj.Count - 1];
            AllCardsObj.Remove(co);

            CardObj lastCardObject = listOfPlayerCard[listOfPlayerCard.Count - 1];

            listOfPlayerCard.Add(co);

            if (listOfPlayerCard.Count == 0)
            {
                MoveCard(co.gameObject, playerObject.transform.position);
            }
            else
            {
                MoveCard(co.gameObject, new Vector3(
                lastCardObject.gameObject.transform.position.x + Constants.CARDS_SPACE_OFFSET,
                (lastCardObject.gameObject.transform.position.y)
                ));
            }
            co.gameObject.transform.SetParent(playerObject.transform);
        }
    }

    public void PlayCard(GameObject cardGameObject)
    {
        CardObj card = cardGameObject.GetComponent<CardObj>();

        CardManager cm = CardManager.GetComponent<CardManager>();

        Card playedCard = card.GetCard();

        Card currentCard = cm.GetCurrentPlayingCard();

        cm.PlayCard(playedCard);
    }

    public GameObject GetPlayerObject(string playerId)
    {
        return PlayersObjectArray.Where(
            ap => ap.GetComponent<PlayerCardList>().PlayerName == playerId
            ).First();
    }

    public CardObj GetCardObj(List<CardObj> listOfCard, Card card)
    {
        return listOfCard.Where(c => c.Rank == card.Rank && c.Suit == card.Suit).First();
    }

    public bool CheckIfCardPlayerIsValid(Card playedCard, Card currentCard)
    {
        return true;
    }
}