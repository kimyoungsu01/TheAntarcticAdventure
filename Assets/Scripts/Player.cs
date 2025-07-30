using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float forwardSpeed = 8f;     // ������ ���ư��� �ӵ�
    public float jumpForce = 10f;       // ���� ��
    public float slideDuration = 0.8f;  // �����̵� ���� �ð�

    private Rigidbody2D rb;             // 2D �����̹Ƿ� Rigidbody2D ���
    private CapsuleCollider2D capsuleCollider; // CapsuleCollider2D�� ����

    private bool isGrounded;            // ĳ���Ͱ� ���� �ִ��� ����
    private int jumpCount;              // ���� ���� Ƚ�� (2�� ������ ����)
    private const int MAX_JUMP_COUNT = 2; // �ִ� ���� Ƚ��

    private bool isSliding;             // �����̵� ������ ����
    private Vector2 originalColliderSize;   // ���� �ݶ��̴� ũ�� (2D)
    private Vector2 originalColliderOffset; // ���� �ݶ��̴� ������ (2D)

  
    Animator animator;

    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // �ʱ� �ݶ��̴� ũ��� �������� ���� (2D �ݶ��̴� �Ӽ�)
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;

        if (animator == null)
            Debug.LogError("not Found Animator");

    }

    void Update()
    {
        // 2�� ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MAX_JUMP_COUNT)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // ���� y �ӵ��� 0���� �ʱ�ȭ (Vector2 ���)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ForceMode2D.Impulse ���
            jumpCount++;
            animator.SetBool("IsJump", true);

        }

        // �����̵� (Shift Ű)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            StartSlide();
            animator.SetBool("IsSlide", true);

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);
    }

    // ĳ���Ͱ� ���� ��Ҵ��� Ȯ�� (2D)
    void OnCollisionEnter2D(Collision2D collision) 
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    // ĳ���Ͱ� ������ �������� �� (2D)
    void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;

            animator.SetBool("IsJump", false);
        }
    }

    void StartSlide()
    {
        isSliding = true;
        // �ݶ��̴� ũ�⸦ ���̰� �������� �����Ͽ� �����̵� �ڼ��� ���� (2D �ݶ��̴� �Ӽ�)
        capsuleCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2f);
        capsuleCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4f));

        // ���� �ð� �� �����̵带 ���ߴ� �ڷ�ƾ ����
        Invoke("StopSlide", slideDuration);
    }

    void StopSlide()
    {
        isSliding = false;
        // �ݶ��̴��� ������� �ǵ��� (2D �ݶ��̴� �Ӽ�)
        capsuleCollider.size = originalColliderSize;
        capsuleCollider.offset = originalColliderOffset;

        animator.SetBool("IsSlide", false);
    }


}
