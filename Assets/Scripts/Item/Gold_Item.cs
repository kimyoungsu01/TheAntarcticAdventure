using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Item : Item
{

    protected override void OnInteract()
    {
        int score = 0; // 점수를 받아온다 임시 코드
        int type = 0; // 골드 아이템 타입


        if (type == 0)
        {
            score = score + 10;
        }

        else if (type == 0)
        {
            score = score + 5;
        }

        else if (type == 0)
        {
            score = score + 2;
        }

        else 
        { 
            score = score + 1;
        }

    }
}
