using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Item : Item
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    protected override void OnInteract()
    {
        gameManager.Heal();


    }
}
