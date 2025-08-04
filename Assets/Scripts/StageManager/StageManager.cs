using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard }

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public Difficulty currentDifficulty = Difficulty.Normal;

    [Header("난이도별 시작 속도")]
    public float easyStartSpeed = 4f;
    public float normalStartSpeed = 6f;
    public float hardStartSpeed = 8f;

    [Header("가속도 (초당 증가 속도)")]
    public float acceleration = 0.05f;

    private Player player;
    private float currentSpeed;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player를 찾을 수 없습니다!");
            return;
        }

        SetDifficulty(currentDifficulty);
    }

    void Update()
    {
        if (player == null) return;

        currentSpeed += acceleration * Time.deltaTime;
        player.forwardSpeed = currentSpeed;
    }

    public void SetDifficulty(Difficulty newDifficulty)
    {
        currentDifficulty = newDifficulty;

        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                currentSpeed = easyStartSpeed;
                break;
            case Difficulty.Normal:
                currentSpeed = normalStartSpeed;
                break;
            case Difficulty.Hard:
                currentSpeed = hardStartSpeed;
                break;
        }

        if (player != null)
            player.forwardSpeed = currentSpeed;

        Debug.Log($"[StageManager] 난이도 변경: {currentDifficulty}, 시작 속도: {currentSpeed}");
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // 에디터에서 currentDifficulty 바꿀 때 즉시 반영하려면
        if (!Application.isPlaying)
        {
            switch (currentDifficulty)
            {
                case Difficulty.Easy:
                    currentSpeed = easyStartSpeed;
                    break;
                case Difficulty.Normal:
                    currentSpeed = normalStartSpeed;
                    break;
                case Difficulty.Hard:
                    currentSpeed = hardStartSpeed;
                    break;
            }
        }
    }
#endif
}
