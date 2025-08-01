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

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void RefreshHpbar()
    {
        // GameManager에 있는 HP를 받아와서 HPbar에 표시해주는것
    }

    public void RefreshScoreText()
    {
        // GameManager에 있는 점수를 받아와서 점수를 표시해주는것
    }
}
