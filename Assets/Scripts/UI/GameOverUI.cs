using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI gameoverscoreText;
    public TextMeshProUGUI restartsText;

    //Start is called before the first frame update
    void Start()
    {
        if (gameoverscoreText == null)
        {
            Debug.LogError("restart text is null");
        }

        if (restartsText == null)
        {
            Debug.LogError("score text is null");
        }
        
        if (restartsText == null) // null 체크 후 비활성화
        {
            restartsText.gameObject.SetActive(false);
        }
    }

    //Update is called once per frame

    public void SetRestart()
    {
        if (restartsText != null) // null 체크 후 활성화
        {
            restartsText.gameObject.SetActive(true);
        }
    }
    public void UpdateScore(float score)
    {
        if (gameoverscoreText != null) // null 체크 후 점수 업데이트
        {
            gameoverscoreText.text = score.ToString();
        }
    }
    public void ShowGameOverScreen(float finalScore)
    {
        
        this.gameObject.SetActive(true);// 이 스크립트가 붙어있는 게임 오브젝트(UI 패널) 자체를 활성화합니다.
                                        
        UpdateScore(finalScore); // `UpdateScore` 메서드를 호출하여 점수를 업데이트합니다.

        SetRestart(); // `SetRestart` 메서드를 호출하여 재시작 텍스트를 활성화합니다.
    }
    /// 게임 오버 화면 전체를 비활성화하여 화면에서 숨깁니다.
    public void HideGameOverScreen()
    {
        // 이 스크립트가 붙어있는 게임 오브젝트(UI 패널) 자체를 비활성화합니다.
        this.gameObject.SetActive(false);
    }
}
