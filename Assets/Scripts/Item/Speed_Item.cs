using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Item : Item
{
    public float SpeedAmt = 0;
    public float MaxSpeed = 2f;
    public float MinSpeed = 8f;
    public float CharSpeed = 5f;

    //기본 속도를 5로 가정 최저속도 2 최대속도 8로 가정
    protected override void OnInteract()
    {
        //SpeedAmt 에 따라 속도를 증가,감소 시켜주는 코드
        if (SpeedAmt == 0)
        {
            CharSpeed += 1f;
        }
        else
        {
            CharSpeed -= 1f;
        }
    }
}
