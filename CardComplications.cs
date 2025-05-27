using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CardComplications : MonoBehaviour
{
    public GameObject cardPrime;
    public GameObject canvas;
    GameObject enemyThe;
    List<GameObject> cardList = new List<GameObject>();
    int currentIndex = -1;
    public int indexNum;
    bool enemyCard = true;

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            addCard();
            GameObject startCard = cardList[currentIndex];
            startCard.GetComponent<CardBasics>().health = Random.Range(1, 6);
            startCard.GetComponent<CardBasics>().attack = Random.Range(1, 6);
            int healthscost = startCard.GetComponent<CardBasics>().health / 2;
            startCard.GetComponent<CardBasics>().cost = startCard.GetComponent<CardBasics>().attack + healthscost;
        }
    }

    public void addCard()
    {
        if(currentIndex >= 0)
        {
            cardList[currentIndex].SetActive(false);
        }
        GameObject newCard = (GameObject)Instantiate(cardPrime, GameObject.Find("Spawned_Card").transform.position, Quaternion.identity, canvas.transform);
        cardList.Add(newCard);
        currentIndex = cardList.Count - 1;
        cardList[currentIndex].SetActive(true);
    }

    public void upTheList()
    {
        if(currentIndex == cardList.Count - 1)
        {
            Debug.Log("Nuh uh");
        }
        else
        {
            cardList[currentIndex].SetActive(false);
            currentIndex += 1;
            cardList[currentIndex].SetActive(true);
        }
    }

    public void downTheList()
    {
        if(currentIndex == 0)
        {
            Debug.Log("No siri");
        }
        else
        {
            cardList[currentIndex].SetActive(false);
            currentIndex -= 1;
            cardList[currentIndex].SetActive(true);
        }
    }

    public void compareCards(GameObject mainCard, GameObject enemCard)
    {
        int currentAttack = mainCard.GetComponent<CardBasics>().attack;
        int opposingHealth = enemCard.GetComponent<CardBasics>().health;
        int currentHealth = mainCard.GetComponent<CardBasics>().health;
        int opposingAttack = enemCard.GetComponent<CardBasics>().attack;
        if(currentAttack >= opposingHealth)
        {
            if(opposingAttack >= currentHealth)
            {
                Destroy(mainCard);
            }
            else
            {
                mainCard.GetComponent<CardBasics>().health -= opposingAttack;
            }
            Destroy(enemCard);
            //enemyCard = false;
        }
        else
        {
            if(opposingAttack >= currentHealth)
            {
                if(currentAttack >= opposingHealth)
                {
                    Destroy(enemCard);
                    //enemyCard = false;
                }
                else
                {
                    enemCard.GetComponent<CardBasics>().health -= currentAttack;
                }
                Destroy(mainCard);
            }
            else
            {
                mainCard.GetComponent<CardBasics>().health -= opposingAttack;
                enemCard.GetComponent<CardBasics>().health -= currentAttack;
            }
        }
    }

    public GameObject spawnEnemyCard(string enemLocal)
    {
        if(enemyCard == true)
        {
            enemyThe = (GameObject)Instantiate(cardPrime, GameObject.Find(enemLocal).transform.position, Quaternion.identity, canvas.transform);
            enemyThe.GetComponent<CardBasics>().health = Random.Range(1, 6);
            enemyThe.GetComponent<CardBasics>().attack = Random.Range(1, 6);
            enemyThe.SetActive(true);
        }
        else
        {
            Debug.Log("Nah");
        }
        return enemyThe;
    }

    public GameObject placeCard(string objectLocal, GameObject cardSacrifice)
    {
        GameObject card = (GameObject)Instantiate(cardList[currentIndex], GameObject.Find(objectLocal).transform.position, Quaternion.identity, canvas.transform);
        Destroy(cardList[currentIndex]);
        cardList.RemoveAt(currentIndex);
        if(currentIndex > 0)
            {
                currentIndex -= 1;
            }
        if(cardList.Count > 0)
            {
                cardList[currentIndex].SetActive(true);
            }
        card.GetComponent<CardBasics>().isPlaced = true;
        cardSacrifice.GetComponent<CardBasics>().health -= card.GetComponent<CardBasics>().cost;
        if(cardSacrifice.GetComponent<CardBasics>().health == 0)
        {
            Destroy(cardSacrifice);
        }
        return card;
    }

    public GameObject addToHand()
    {
        addCard();
        return cardList[currentIndex];
    }

    public GameObject checkCurrentCard()
    {
        return cardList[currentIndex];
    }
}
