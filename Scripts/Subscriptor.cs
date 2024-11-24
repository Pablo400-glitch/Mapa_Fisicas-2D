using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subscriptor : MonoBehaviour
{
    public PlayerMovement player;
    public Text itemCounterText;

    void Start()
    {
        player.onItemCollected += UpdateItemCounter;
    }

    void UpdateItemCounter(int totalItemsCollected)
    {
        itemCounterText.text = "Items Collected: " + totalItemsCollected.ToString();
    }

    void OnDestroy()
    {
        player.onItemCollected -= UpdateItemCounter;
    }
}
