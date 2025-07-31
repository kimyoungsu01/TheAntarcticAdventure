using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public int Score;
    public int hp;

    public void RefreshHpbar(/*펭귄*/)
    {
        // GameManager에 있는 HP를 받아와서 HPbar에 표시해주는것
        //if (collision.gameObject.CompareTag("장애물")) 
        //{
        //    Debug.Log("장애물");
        //}
    }

    public void RefreshScoreText(/*펭귄*/)
    {
        // GameManager에 있는 점수를 받아와서 점수를 표시해주는것
        //if (collision.gameObject.CompareTag("펭귄"))
        //{
        //    Debug.Log("점수가 올라간다");
        //}
    }
}
