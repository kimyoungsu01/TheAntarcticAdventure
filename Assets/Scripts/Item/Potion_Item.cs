using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Item : Item
{
    int health = 50; // 플레이어 체력을 받아온다 임시코드

    protected override void OnInteract()
    {
        health += 20;
    }
}
