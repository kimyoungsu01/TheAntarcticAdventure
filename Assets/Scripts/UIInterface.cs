using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class UIInterface : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI 텍스트 연결
    public Image hpBar; // 체력바 UI 연결 (선택)

    private GameManager gameManager;
    private float smoothSpeed = 5f; // 체력바가 부드럽게 이동하도록 설정

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null) 
        {
            Debug.Log("게임매니저 연결됨. 현재 체력: " + gameManager.HP);
            return;
        }

        RefreshScoreText(); // 초기 점수 표시
        RefreshHpBar();     // 초기 체력 표시
    }

    public void RefreshHpBar()
    {
        // GameManager에 있는 HP를 받아와서 HPbar에 표시해주는것
        float targetFill = gameManager.HP / 100f;
        StopAllCoroutines();
        StartCoroutine(SmoothFillBar(targetFill));
    }

    private IEnumerator SmoothFillBar(float target)
    {
        float current = hpBar.fillAmount;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * smoothSpeed;
            hpBar.fillAmount = Mathf.Lerp(current, target, t);
            yield return null;
        }
        hpBar.fillAmount = target;
    }

    public void RefreshScoreText()
    {
        // GameManager에 있는 점수를 받아와서 점수를 표시해주는것
        scoreText.text = "Score: " + gameManager.Score.ToString();
    }
}
