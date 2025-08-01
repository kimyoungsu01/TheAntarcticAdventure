using UnityEngine;

public class ObstacleSet : MonoBehaviour
{
    public float respawnDistance = 30f;  // 플레이어로부터 얼마나 떨어지면 다시 등장할지
    private Vector3 initialPosition;
    private Transform playerTransform;

    void Start()
    {
        initialPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform == null) return;

        // 플레이어가 장애물보다 일정 거리 이상 앞서가면 다시 이동시킴
        if (playerTransform.position.x - transform.position.x > respawnDistance)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = initialPosition;
    }
}
