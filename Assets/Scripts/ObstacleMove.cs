using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // �������� ��� �̵�
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // ȭ�� ������ ������ ����
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
