using UnityEngine;

public class Gold : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))  // �浹 ����� �÷��̾��� ���� �۵�
        {
            GameManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}
