using UnityEngine;

public class FallZone : MonoBehaviour
{
    public float transparentAlpha = 0.3f; // ���� (0~1)

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ground �±� ����
        if (other.CompareTag("Ground"))
        {
            SetAlphaRecursive(other.transform, transparentAlpha);
        }

        // �÷��̾� ���� ó��
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

    //  �ڽ� ������Ʈ���� Ž���Ͽ� SpriteRenderer�� ���İ� ����
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
