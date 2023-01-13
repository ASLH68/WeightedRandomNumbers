using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class WeightedSelection : MonoBehaviour
{
    private float[] weights = new float[5];

    private int[] pickAmounts = new int[5];

    private bool setWeights = false;
    private int selectedPlayer;

    [SerializeField] int numPlayers;
    [SerializeField] int numRounds;

   public void PickNumber()
   {
        InitializeWeights();

        for(int i = 0; i < numRounds; i++)
        {
            LogInformation(0);
            WeightedNumber();
            LogInformation(1);
        }

        LogPickedNums();

        Reset();
   }

    private void WeightedNumber()
    {
        float randNum = Random.Range(0f, 100f);
        Debug.Log("randNum = " + randNum);

        float totalWeight = 0f;

        // gets the random player in the list
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];

            if (i == 0 && randNum < weights[i])
            {
                selectedPlayer = i;
                break;
            }
            else if (i == weights.Length - 1 && randNum > (totalWeight - weights[i]))
            {
                selectedPlayer = i;
                break;
            }
            else if (randNum > totalWeight - weights[i] && randNum <= totalWeight)
            {
                selectedPlayer = i;
                break;
            }
        }

        float weightChange = weights[selectedPlayer]/2f;
        Debug.Log("weight change " + weightChange);

        // readjusts the weights after a player is chosen
        for (int i = 0; i < weights.Length; i++)
        {
            if (i == selectedPlayer)
            {
                weights[i] -= weightChange;   // reduces weight of selected player
            }
            else
            {
                weights[i] += (weightChange / (weights.Length - 1));
            }
        }
 

        pickAmounts[selectedPlayer]++;
    }

    /// <summary>
    /// Initializes the weights and picked amount arrays to the correct values
    /// </summary>
    private void InitializeWeights()
    {
        if (!setWeights)
        {
            setWeights = true;
            weights = new float[numPlayers];

            pickAmounts = new int[numPlayers];

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = 100f / numPlayers;
            }
        }
    }

    /// <summary>
    /// Logs the weight values and which player was selected
    /// </summary>
    /// <param name="when"></param>
    private void LogInformation(int when)
    {
        string st = "";

        if(when == 0)
        {
            Debug.Log("Before button ");
        }
        else
        {
            Debug.Log("After button ");
            Debug.Log("Selected Player = " + selectedPlayer);
        }

        foreach(float weight in weights)
        {
            st += weight + " ";
        }

        Debug.Log("weights = " + st);
    }

    /// <summary>
    /// Logs the amount of times each player was picked
    /// </summary>
    private void LogPickedNums()
    {
        string st = "";

        foreach (int num in pickAmounts)
        {
            st += num + " ";
        }

        Debug.Log("picked amounts: " + st);
    }

    /// <summary>
    /// Resets the weights after each run
    /// </summary>
    private void Reset()
    {
        setWeights = false;
    }
}
