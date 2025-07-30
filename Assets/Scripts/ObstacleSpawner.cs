using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnX = 8f;
    public float minHeight = -2f;
    public float maxHeight = 2f;
    public float minWidth = 0.5f;
    public float maxWidth = 2f;

    private float timer;
    public float spawnInterval = 2f;
    private bool spawnOnTop = true;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObstacle();
            timer = spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        float yPos = spawnOnTop ? Random.Range(0f, maxHeight) : Random.Range(minHeight, 0f);
        spawnOnTop = !spawnOnTop; // ��/�Ʒ� �����ư��� ����

        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(spawnX, yPos, 0), Quaternion.identity);

        // ��ֹ� �ʺ� ���� ����
        float width = Random.Range(minWidth, maxWidth);
        Vector3 scale = obstacle.transform.localScale;
        scale.x = width;
        obstacle.transform.localScale = scale;
    }
}
