using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Item : Item
{
    
    public float MaxSpeed = 2f;
    public float MinSpeed = 8f;
    public float CharSpeed = 5f;


    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public int SpeedAmt = 0; 

    //기본 속도를 5로 가정 최저속도 2 최대속도 8로 가정
    protected override void OnInteract()
    {
        //SpeedAmt 에 따라 속도를 증가,감소 시켜주는 코드

    }
}
