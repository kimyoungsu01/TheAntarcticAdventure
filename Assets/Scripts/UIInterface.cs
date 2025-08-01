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
    private float originalWidth; // 체력 100일 때의 체력바 너비
    private float currentWidth; // 현재 체력바 너비
    private float smoothSpeed = 5f; // 체력바가 부드럽게 이동하도록 설정

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager 연결 실패!");
            return;
        }

        originalWidth = hpBar.rectTransform.sizeDelta.x;
        currentWidth = originalWidth;

        RefreshScoreText(); // 초기 점수 표시
        RefreshHpBar();     // 초기 체력 표시
    }
    private void Update()
    {
        UpdateHpBarSmoothly(); // 프레임마다 체력바 업데이트
    }

    private void UpdateHpBarSmoothly() //체력바 부드럽게 해준다
    {
        float targetWidth = Mathf.Clamp01(gameManager.HP / 100f) * originalWidth;
        currentWidth = Mathf.Lerp(currentWidth, targetWidth, Time.deltaTime * smoothSpeed);

        Vector2 size = hpBar.rectTransform.sizeDelta;
        size.x = currentWidth;
        hpBar.rectTransform.sizeDelta = size;
    }

    public void RefreshHpBar()
    {
        // GameManager에 있는 HP를 받아와서 HPbar에 표시해준다
        float hpPercent = Mathf.Clamp01(gameManager.HP / 100f);
        float targetWidth = hpPercent * originalWidth;

        Vector2 size = hpBar.rectTransform.sizeDelta;
        size.x = targetWidth;
        hpBar.rectTransform.sizeDelta = size;
    }

    public void RefreshScoreText()
    {
        // GameManager에 있는 점수를 받아와서 점수를 표시해준다
        scoreText.text = "Score: " + gameManager.Score.ToString();
    }
}
