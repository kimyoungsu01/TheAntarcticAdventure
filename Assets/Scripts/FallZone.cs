using UnityEngine;

public class FallZone : MonoBehaviour
{
    public float transparentAlpha = 0.3f; // 투명도 (0~1)

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ground 태그 감지
        if (other.CompareTag("Ground"))
        {
            SetAlphaRecursive(other.transform, transparentAlpha);
        }

        // 플레이어 낙사 처리
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            SetAlphaRecursive(other.transform, 1f);
        }
    }

    //  자식 오브젝트까지 탐색하여 SpriteRenderer의 알파값 변경
    void SetAlphaRecursive(Transform target, float alpha)
    {
        SpriteRenderer[] renderers = target.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}
