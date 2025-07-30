using UnityEngine;

public class Gold : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))  // 충돌 대상이 플레이어일 때만 작동
        {
            GameManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}
