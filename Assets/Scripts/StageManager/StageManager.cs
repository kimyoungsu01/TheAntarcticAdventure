using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard }

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public Difficulty currentDifficulty = Difficulty.Normal;

    [Header("���̵��� ���� �ӵ�")]
    public float easyStartSpeed = 4f;
    public float normalStartSpeed = 6f;
    public float hardStartSpeed = 8f;

    [Header("���ӵ� (�ʴ� ���� �ӵ�)")]
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
            Debug.LogError("Player�� ã�� �� �����ϴ�!");
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

        Debug.Log($"[StageManager] ���̵� ����: {currentDifficulty}, ���� �ӵ�: {currentSpeed}");
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // �����Ϳ��� currentDifficulty �ٲ� �� ��� �ݿ��Ϸ���
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
