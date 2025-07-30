using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class ItemSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    public float startX = 8f;
    public float coinSpacing = 0.6f;       // 그룹 내 코인 간격
    public float groupSpacing = 1.8f;      // 그룹 간격
    public int groupCount = 4;             // 한 번 스폰 시 그룹 개수
    public int coinsPerGroup = 3;          // 그룹당 코인 개수 (2~3 추천)

    public float baseY = 1.5f;             // 기본 높이
    public float arcHeight = 1f;           // 곡선의 전체 높이

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

        // 그룹 전체를 따라가는 큰 곡선 기준으로 높이 계산
        for (int g = 0; g < groupCount; g++)
        {
            float t = groupCount > 1 ? g / (float)(groupCount - 1) : 0.5f;
            float groupY = CalculateCurveY(t);

            // 그룹 내 코인은 거의 같은 높이에서 스폰
            for (int i = 0; i < coinsPerGroup; i++)
            {
                Vector3 pos = new Vector3(currentX, groupY + (i * 0.05f), 0); // 살짝만 높이 차
                Instantiate(coinPrefab, pos, Quaternion.identity);
                currentX += coinSpacing;
            }

            // 그룹 간 간격 추가
            currentX += groupSpacing;
        }
    }

    // 쿠키런 스타일 곡선 (부드러운 하나의 궤적)
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
