using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIInterface : GameManager
{
    GameManager gameManager;
    Player player;
    // 

    public int Score;
    public int hp;

    public void RefreshHpbar(/*펭귄*/)
    {
        gameManager = GameManager.Instance;
        // GameManager에 있는 HP를 받아와서 HPbar에 표시해주는것
        //if (collision.gameObject.CompareTag("장애물")) 
        //{
        //    Debug.Log("장애물");
        //}
    }

    public void RefreshScoreText(Collision2D collision)
    {
        // GameManager에 있는 점수를 받아와서 점수를 표시해주는것
        
        if (collision.gameObject.CompareTag("동전")) // 만약 동전 colision에 충돌한다면
        {
            gameManager.addscore(Score); // 점수가 올라간다
            Text.print(Score);// 텍스트가 값을 반영한다
            Debug.Log("점수가 올라간다");
        }

    }
}
