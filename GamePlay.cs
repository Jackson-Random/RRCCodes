using UnityEngine;
using System.Collections.Generic;

public class GamePlay : MonoBehaviour
{
    List<bool> cardSlots = new List<bool>();
    public CardComplications cardComplications;
    GameObject[] playerCards = new GameObject[4];
    GameObject[] enemyCards = new GameObject[4];
    GameObject cardMain;
    GameObject enemyMain;
    bool startTurn = false;
    int deckToken;
    int[] enemyCountdowns = new int[4];
    string placementName;
    public bool placementNeeded = false;
    int slotFilled;
    
    //void that adds both player and enemy main cards, as well as deck tokens
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            cardSlots.Add(false);
        }
        deckToken = Random.Range(10, 14);
        int randomSpot = Random.Range(1, 5);
        switch(randomSpot)
        {
            case 1:
                cardMain = cardComplications.spawnEnemyCard("Card_Spot-1");
                cardSlots[0] = true;
                break;
            case 2:
                cardMain = cardComplications.spawnEnemyCard("Card_Spot-2");
                cardSlots[1] = true;
                break;
            case 3:
                cardMain = cardComplications.spawnEnemyCard("Card_Spot-3");
                cardSlots[2] = true;
                break;
            case 4:
                cardMain = cardComplications.spawnEnemyCard("Card_Spot-4");
                cardSlots[3] = true;
                break;
        }
        cardMain.GetComponent<CardBasics>().health = 30;
        cardMain.GetComponent<CardBasics>().attack = 0;
        cardMain.GetComponent<CardBasics>().isPlaced = true;
        randomSpot = Random.Range(1, 5);
        switch(randomSpot)
        {
            case 1:
                enemyMain = cardComplications.spawnEnemyCard("Enemy_Card1");
                enemyCards[0] = enemyMain;
                break;
            case 2:
                enemyMain = cardComplications.spawnEnemyCard("Enemy_Card2");
                enemyCards[1] = enemyMain;
                break;
            case 3:
                enemyMain = cardComplications.spawnEnemyCard("Enemy_Card3");
                enemyCards[2] = enemyMain;
                break;
            case 4:
                enemyMain = cardComplications.spawnEnemyCard("Enemy_Card4");
                enemyCards[3] = enemyMain;
                break;
        }
        enemyMain.GetComponent<CardBasics>().health = 30;
        enemyMain.GetComponent<CardBasics>().attack = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Void that is called upon completing battle phase
    void startOfTurn()
    {
        startTurn = true;
        for(int i = 0; i < 4; i++)
        {
            if(enemyCountdowns[i] == 0 && enemyCards[i] == null)
            {
                enemyCountdowns[i] = Random.Range(3, 6);
            }
            else
            {
                enemyCountdowns[i] -= 1;
                if(enemyCountdowns[i] == 0)
                {
                    switch(i)
                    {
                        case 0:
                            enemyCards[0] = cardComplications.spawnEnemyCard("Enemy_Card1");
                            break;
                        case 1:
                            enemyCards[1] = cardComplications.spawnEnemyCard("Enemy_Card2");
                            break;
                        case 2:
                            enemyCards[2] = cardComplications.spawnEnemyCard("Enemy_Card3");
                            break;
                        case 3:
                            enemyCards[3] = cardComplications.spawnEnemyCard("Enemy_Card4");
                            break;
                    }
                    enemyCountdowns[i] = Random.Range(3, 6);
                }
            }
        }
    }

    //all four identical voids do the same function, setting up where a card will be placed
    public void setCard1()
    {
        if(playerCards[0] == null)
        {
            cardSlots[0] = false;
        }
        
        if(cardSlots[0])
        {
            Debug.Log("Slot is filled. Kill");
        }
        else
        {
            placementNeeded = true;
            placementName = "Card_Spot-1";
            slotFilled = 0;
            Debug.Log("Card is ready, choose a card to Subtract Health");
        }
    }

    public void setCard2()
    {
        if(playerCards[1] == null)
        {
            cardSlots[1] = false;
        }
        
        if(cardSlots[1])
        {
            Debug.Log("Slot is filled. Kill");
        }
        else
        {
            placementNeeded = true;
            placementName = "Card_Spot-2";
            slotFilled = 1;
            Debug.Log("Card is ready, choose a card to Subtract Health");
        }
    }

    public void setCard3()
    {
        if(playerCards[2] == null)
        {
            cardSlots[2] = false;
        }
        
        if(cardSlots[2])
        {
            Debug.Log("Slot is filled. Kill");
        }
        else
        {
            placementNeeded = true;
            placementName = "Card_Spot-3";
            slotFilled = 2;
            Debug.Log("Card is ready, choose a card to Subtract Health");
        }
    }

    public void setCard4()
    {
        if(playerCards[3] == null)
        {
            cardSlots[3] = false;
        }
        
        if(cardSlots[3])
        {
            Debug.Log("Slot is filled. Kill");
        }
        else
        {
            placementNeeded = true;
            placementName = "Card_Spot-4";
            slotFilled = 3;
            Debug.Log("Card is ready, choose a card to Subtract Health");
        }
    }

    /*
    public void spawningEnem()
    {
        enemyA = cardComplications.spawnEnemyCard("Enemy_Card");
    }
    */

    //drawing a card
    public void addRandomCard()
    {
        if(startTurn == false)
        {
            Debug.Log("You already drawed dummy");
        }
        else
        {
            if(deckToken > 0)
            {
                GameObject addedCard = cardComplications.addToHand();
                addedCard.GetComponent<CardBasics>().attack = Random.Range(1, 6);
                addedCard.GetComponent<CardBasics>().health = Random.Range(1, 6);
                int healthcost = addedCard.GetComponent<CardBasics>().health / 2;
                addedCard.GetComponent<CardBasics>().cost = addedCard.GetComponent<CardBasics>().attack + healthcost;
                deckToken -= 1;
            }
            startTurn = false;
        }
    }

    //void where the cards battle their lane, and calculate health and if they live
    public void battleCards()
    {
        if(startTurn)
        {
            Debug.Log("You still need to draw a card");
        }

        if(startTurn == false)
        {
            for(int i = 0; i < 4; i++)
            {
                if(playerCards[i] == null && enemyCards[i] != null)
                {
                    cardComplications.compareCards(cardMain, enemyCards[i]);
                }
                else if(enemyCards[i] == null && playerCards[i] != null)
                {
                    cardComplications.compareCards(playerCards[i], enemyMain);
                }
                else if(enemyCards[i] != null && playerCards[i] != null)
                {
                    cardComplications.compareCards(playerCards[i], enemyCards[i]);
                }
                else
                {
                    cardComplications.compareCards(cardMain, enemyMain);
                }
            }
            startOfTurn();
        }
        
    }

    //void where cards actually get placed in their correct slot
    public void placementTime(GameObject cardPlaced)
    {
        GameObject currentCard = cardComplications.checkCurrentCard();
        if(currentCard.GetComponent<CardBasics>().cost > cardPlaced.GetComponent<CardBasics>().health)
        {
            Debug.Log("Too high cost, not enough HP");
        }
        else
        {
            playerCards[slotFilled] = cardComplications.placeCard(placementName, cardPlaced);
            cardSlots[slotFilled] = true;
            placementNeeded = false;
        }
    }

}
