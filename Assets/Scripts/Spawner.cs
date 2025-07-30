using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    public float obstacleSpawnX = 8f;
    public float obstacleMinHeight = -2f;
    public float obstacleMaxHeight = 2f;
    public float obstacleMinWidth = 0.5f;
    public float obstacleMaxWidth = 2f;
    public float obstacleSpawnInterval = 2f;

    [Header("Item Settings")]
    public GameObject itemPrefab;
    public float itemSpawnX = 8f;
    public int minItemsPerGroup = 5;
    public int maxItemsPerGroup = 10;
    public float itemSpacing = 0.7f;
    public float arcHeight = 2f;
    public float groupSpacing = 3f;

    public float itemMinSpawnTime = 1f;  // 아이템 스폰 최소 시간 간격
    public float itemMaxSpawnTime = 2.5f; // 아이템 스폰 최대 시간 간격

    private float obstacleTimer;
    private float itemSpawnTimer;
    private float lastItemGroupEndX = 0f;
    private bool spawnObstacleOnTop = true;

    void Start()
    {
        obstacleTimer = obstacleSpawnInterval;
        itemSpawnTimer = Random.Range(itemMinSpawnTime, itemMaxSpawnTime);
        SpawnItemGroup();  // 시작하자마자 아이템 그룹 한 번 스폰
    }

    void Update()
    {
        // 장애물 타이머
        obstacleTimer -= Time.deltaTime;
        if (obstacleTimer <= 0f)
        {
            SpawnObstacle();
            obstacleTimer = obstacleSpawnInterval;
        }

        // 아이템 타이머
        itemSpawnTimer -= Time.deltaTime;
        if (itemSpawnTimer <= 0f)
        {
            SpawnItemGroup();
            itemSpawnTimer = Random.Range(itemMinSpawnTime, itemMaxSpawnTime);
        }
    }

    void SpawnObstacle()
    {
        float yPos = spawnObstacleOnTop ? Random.Range(0f, obstacleMaxHeight) : Random.Range(obstacleMinHeight, 0f);
        spawnObstacleOnTop = !spawnObstacleOnTop;

        GameObject obs = Instantiate(obstaclePrefab, new Vector3(obstacleSpawnX, yPos, 0f), Quaternion.identity);
        float width = Random.Range(obstacleMinWidth, obstacleMaxWidth);
        Vector3 scale = obs.transform.localScale;
        scale.x = width;
        obs.transform.localScale = scale;
        obs.tag = "Obstacle";
    }

    void SpawnItemGroup()
    {
        int pattern = Random.Range(0, 3); // 0: 직선, 1: 포물선, 2: 원형
        int itemCount = Random.Range(minItemsPerGroup, maxItemsPerGroup + 1);
        float radius = 1.5f;

        float groupStartX = Mathf.Max(itemSpawnX, lastItemGroupEndX + groupSpacing);

        for (int i = 0; i < itemCount; i++)
        {
            Vector3 spawnPos;

            if (pattern == 0) // 직선
            {
                spawnPos = new Vector3(groupStartX + i * itemSpacing, 0, 0);
            }
            else if (pattern == 1) // 포물선
            {
                float t = itemCount > 1 ? i / (float)(itemCount - 1) : 0.5f;
                float x = groupStartX + i * itemSpacing;
                float y = -4f * arcHeight * (t - 0.5f) * (t - 0.5f) + arcHeight;
                spawnPos = new Vector3(x, y, 0);
            }
            else // 원형
            {
                float angle = (i / (float)itemCount) * Mathf.PI * 2f;
                float x = groupStartX + Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                spawnPos = new Vector3(x, y, 0);
            }

            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        }

        // 다음 그룹 스폰 위치 갱신
        if (pattern == 2)
            lastItemGroupEndX = groupStartX + radius * 2;
        else
            lastItemGroupEndX = groupStartX + itemCount * itemSpacing;
    }
}
