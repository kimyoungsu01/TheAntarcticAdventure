using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public float HP => player.currentHealth;
    public float Score => CurrentScore;
    public AudioSource Bgm;
    public AudioSource SE;
    public AudioSource GameOverSound;
    [SerializeField] private AudioClip RestartSound;


    private float CurrentScore =0;
    private float NowHealth = 0;
    public TMP_Text ScoreText;


    // 게임매니저에서 플레이어에 접근해서 변수를 가져오기

    public GameOverUI gameOverUIInstance; // 게임 오버 UI 스크립트 참조

    public float GetCurrentScore()// CurrentScore 변수의 값을 외부에서 읽을 수 있도록 public 메서드를 추가합니다.
    {
        return CurrentScore;
    }
    public void addscore(int scoreAmt)
    {
        CurrentScore += scoreAmt;
        Debug.Log("현재 점수" + CurrentScore);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        ScoreText.text = CurrentScore.ToString();
    }

    public void Heal()
    {
        float NowHealth = player.currentHealth;
        NowHealth += 20f;
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
        float MaxSpeed = 8f;
        float MinSpeed = 2f;
        player.forwardSpeed = Mathf.Clamp(player.forwardSpeed + SpeedAmt, MinSpeed, MaxSpeed);
        Debug.Log("속도" + player.forwardSpeed);


    }
    public void EndGame()//게임 오버를 처리하는 메서드입니다.
    {
        Debug.Log("GameManager: 게임 종료! 최종 점수: " + GetCurrentScore());
        Bgm.Stop();
        GameOverSound.Play();
        //게임 오버 화면을 활성화하고 점수를 전달합니다.
        if (gameOverUIInstance != null)
        {
            gameOverUIInstance.ShowGameOverScreen(GetCurrentScore());
        }
        else
        {
            Debug.LogError("GameManager: gameOverUIInstance가 연결되지 않았습니다. 게임 오버 UI를 표시할 수 없습니다! 유니티 인스펙터에서 연결해주세요.");
        }

        // 게임 시간을 멈춥니다.
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Debug.Log("GameManager: 게임을 재시작합니다!");

        // 게임 오버 UI를 숨깁니다.
        if (gameOverUIInstance != null)
        {
            gameOverUIInstance.HideGameOverScreen();

        }
        Time.timeScale = 1f;
    
    // 게임 시간을 다시 정상화합니다.
    // 점수 초기화 (새로운 게임 시작 시 점수를 0으로 만들고 싶을 때)
    CurrentScore = 0;

    // 현재 씬을 다시 로드하여 게임을 재시작합니다.
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}


}

