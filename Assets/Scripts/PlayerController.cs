using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isDead = false;
    private bool isSliding = false;
    private int jumpCount = 0; //  ���� Ƚ�� ī��Ʈ

    private BoxCollider2D col;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isDead) return;

        //  2�� ���� (���� Ƚ�� < 2)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && !isSliding)
        {
            Jump();
        }

        //  �����̵� (S Ű�� ������ �ִ� ���ȸ�)
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            if (!isSliding)
                StartSlide();
        }
        else if (isSliding)
        {
            EndSlide();
        }

        // �� �Ʒ��� �������� ���
        if (transform.position.y < -5f)
            Die();
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount++;
        isGrounded = false;
    }

    void StartSlide()
    {
        isSliding = true;
        col.size = new Vector2(col.size.x, col.size.y / 2);
        col.offset = new Vector2(col.offset.x, col.offset.y - 0.25f);
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
    }

    void EndSlide()
    {
        isSliding = false;
        col.size = originalColliderSize;
        col.offset = originalColliderOffset;
        transform.localScale = originalScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; //  ���� ������ ���� ī��Ʈ �ʱ�ȭ
        }

        //if (collision.gameObject.CompareTag("Obstacle"))
        //    Die();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            GameManager.Instance.AddScore(10);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance.GameOver();
    }
}
