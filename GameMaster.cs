using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public int value1;
    public int value2;
    public int value3;
    public int currentSavings = 100;
    int newValue;
    [SerializeField] TMP_InputField input;
    int bet;
    int loseCount;
    bool win = false;
    bool failPushback = false;
    public ExtraHandler extraHandler;

    
    //both generates values that will be changed in next void, and calculates if you get a win
    public void inistiateGamble()
    {
        int.TryParse(input.text, out int result);
        bet = result;
        if(bet < 10)
        {
            bet = 10;
        }
        if(bet > currentSavings)
        {
            Debug.Log("Sorry m8, ye can't make that big a bet");
        }
        else
        {
            currentSavings -= bet;
            value1 = Random.Range(1, 21);
            value2 = Random.Range(1, 21);
            value3 = Random.Range(1, 21);
            changeValues();
            bool sixUsed = false;
            if(value2 == value1 || value2 == 6)
            {
                if(value2 == 6 && value2 != value1)
                {
                    sixUsed = true;
                }
                if(value3 == value1 || value3 == 6)
                {
                    if(sixUsed == true)
                    {
                        Debug.Log("Ooh, better luck next time");
                    }
                    else
                    {
                        win = true;
                        loseCount = 0;
                        failPushback = false;
                        extraHandler.winningSound();
                        if(value3 == 6 && value3 != value1)
                        {
                            sixUsed = true;
                        }
                        Debug.Log("Yay! You got three of a kind!");
                    }
                }
                else
                {
                    loseCount += 1;
                    Debug.Log("Womp Womp");
                }
            }
            else
            {
                loseCount +=1;
                Debug.Log("Womp Womp");
            }
            giverewards(sixUsed);
        }
    }

    //turns values generated earlier into a simple range of one to seven
    public void changeValues()
    {
        int currentValueChanging = value1;
        
        for(int i = 0; i < 3; i++)
        {
            if(currentValueChanging <= 5)
            {
                newValue = 1;
            }
            else if(currentValueChanging <= 10)
            {
                newValue = 2;
            }
            else if(currentValueChanging <= 13)
            {
                newValue = 3;
                if(failPushback)
                {
                    newValue = 1;
                }
            }
            else if(currentValueChanging <= 16)
            {
                newValue = 4;
                if(failPushback)
                {
                    newValue = 1;
                }
            }
            else
            {
                switch(currentValueChanging)
                {
                    case 17:
                        newValue = 5;
                        if(failPushback)
                        {
                            newValue = 1;
                        }
                        break;
                    case 18:
                        newValue = 5;
                        if(failPushback)
                        {
                            newValue = 2;
                        }
                        break;
                    case 19:
                        newValue = 6;
                        if(failPushback)
                        {
                            newValue = 2;
                        }
                        break;
                    case 20:
                        newValue = 7;
                        if(failPushback)
                        {
                            newValue = 3;
                        }
                        break;
                }
            }
            switch(i)
            {
                case 0:
                    value1 = newValue;
                    currentValueChanging = value2;
                    break;
                case 1:
                    value2 = newValue;
                    if(failPushback)
                    {
                        value2 = value1;
                    }
                    currentValueChanging = value3;
                    break;
                case 2:
                    value3 = newValue;
                    if(failPushback)
                    {
                        value3 = value1;
                    }
                    break;
            }

        }
    }

    //calculates what needs to be given to theb player depending on if they win, and if they failed enough times for a pity win
    void giverewards(bool checkWild)
    {
        if(win)
        {
            double reward = (double)bet;
            int addedReward;
            switch(value1)
            {
                case 1:
                    reward *= 1.5;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 2:
                    reward *= 2.0;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 3:
                    reward *= 2.5;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 4:
                    reward *= 4;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 5:
                    reward *= 7.5;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 6:
                    reward *= 20.0;
                    addedReward = (int)reward;
                    currentSavings += addedReward;
                    break;
                case 7:
                    if(checkWild)
                    {
                        reward *= 5.5;
                        addedReward = (int)reward;
                        currentSavings += addedReward;
                        break;
                    }
                    else
                    {
                        reward *= 25.5;
                        addedReward = (int)reward;
                        currentSavings += addedReward;
                        break;
                    }
                    
            }
            win = false;
            extraHandler.displayValues(currentSavings, value1, value2, value3);
        }
        else if(loseCount == 7)
        {
            failPushback = true;
            Debug.Log("Pity enabled");
        }
        extraHandler.displayValues(currentSavings, value1, value2, value3);
        if(currentSavings < 10)
        {
            extraHandler.gameOver();
        }
    }
}
