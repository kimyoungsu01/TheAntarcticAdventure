using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // 왼쪽으로 계속 이동
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 화면 밖으로 나가면 제거
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
