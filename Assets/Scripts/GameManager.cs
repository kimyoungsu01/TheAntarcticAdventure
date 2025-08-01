using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private void Awake()
    {
        gameManager = this;
    }
    //싱글톤으로 만든 후에 플레이어에 접근하게 만들기,
    public Player player;


    private int CurrentScore =0;
    private int NowHealth = 0;
    private int NowSpeed = 5;
    // 게임매니저에서 플레이어에 접근해서 변수를 가져오기

    public void addscore(int scoreAmt)
    {
        CurrentScore += scoreAmt;
        Debug.Log("현재 점수" + CurrentScore);
    }
    public void Heal()
    {
        NowHealth = player.currentHealth;
        NowHealth += 20;
        if (NowHealth > player.maxHealth)
        {
            player.currentHealth = player.maxHealth;
        }
        else
        {
            player.currentHealth += 20;
        }


    }

    public void ChangeSpeed(int SpeedAmt)
    {
        NowSpeed += SpeedAmt;
    }
}

