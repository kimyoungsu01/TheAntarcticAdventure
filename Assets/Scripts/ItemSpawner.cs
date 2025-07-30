using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class ItemSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    public float startX = 8f;
    public float coinSpacing = 0.6f;       // �׷� �� ���� ����
    public float groupSpacing = 1.8f;      // �׷� ����
    public int groupCount = 4;             // �� �� ���� �� �׷� ����
    public int coinsPerGroup = 3;          // �׷�� ���� ���� (2~3 ��õ)

    public float baseY = 1.5f;             // �⺻ ����
    public float arcHeight = 1f;           // ��� ��ü ����

    public enum Pattern { Parabola, SlantParabola, Line }
    public Pattern pattern = Pattern.Parabola;

    private float timer;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2.5f;

    void Start()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCoinPattern();
            timer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnCoinPattern()
    {
        float currentX = startX;

        // �׷� ��ü�� ���󰡴� ū � �������� ���� ���
        for (int g = 0; g < groupCount; g++)
        {
            float t = groupCount > 1 ? g / (float)(groupCount - 1) : 0.5f;
            float groupY = CalculateCurveY(t);

            // �׷� �� ������ ���� ���� ���̿��� ����
            for (int i = 0; i < coinsPerGroup; i++)
            {
                Vector3 pos = new Vector3(currentX, groupY + (i * 0.05f), 0); // ��¦�� ���� ��
                Instantiate(coinPrefab, pos, Quaternion.identity);
                currentX += coinSpacing;
            }

            // �׷� �� ���� �߰�
            currentX += groupSpacing;
        }
    }

    // ��Ű�� ��Ÿ�� � (�ε巯�� �ϳ��� ����)
    float CalculateCurveY(float t)
    {
        float y = baseY;
        switch (pattern)
        {
            case Pattern.Parabola:
                y += -4f * arcHeight * (t - 0.5f) * (t - 0.5f) + arcHeight;
                break;
            case Pattern.SlantParabola:
                y += -4f * arcHeight * (t - 0.5f) * (t - 0.5f) + arcHeight + 0.5f * t;
                break;
            case Pattern.Line:
                y += arcHeight * t;
                break;
        }
        return y;
    }
}
