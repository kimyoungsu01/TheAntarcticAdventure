using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float forwardSpeed = 8f;     // 앞으로 나아가는 속도
    public float jumpForce = 10f;       // 점프 힘
    public float slideDuration = 0.8f;  // 슬라이드 지속 시간

    private Rigidbody2D rb;             // 2D 게임이므로 Rigidbody2D 사용
    private CapsuleCollider2D capsuleCollider; // CapsuleCollider2D로 변경

    private bool isGrounded;            // 캐릭터가 땅에 있는지 여부
    private int jumpCount;              // 현재 점프 횟수 (2단 점프를 위해)
    private const int MAX_JUMP_COUNT = 2; // 최대 점프 횟수

    private bool isSliding;             // 슬라이드 중인지 여부
    private Vector2 originalColliderSize;   // 원래 콜라이더 크기 (2D)
    private Vector2 originalColliderOffset; // 원래 콜라이더 오프셋 (2D)

  
    Animator animator;

    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // 초기 콜라이더 크기와 오프셋을 저장 (2D 콜라이더 속성)
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;

        if (animator == null)
            Debug.LogError("not Found Animator");

    }

    void Update()
    {
        // 2단 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MAX_JUMP_COUNT)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 현재 y 속도를 0으로 초기화 (Vector2 사용)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ForceMode2D.Impulse 사용
            jumpCount++;
            animator.SetBool("IsJump", true);

        }

        // 슬라이드 (Shift 키)
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

    // 캐릭터가 땅에 닿았는지 확인 (2D)
    void OnCollisionEnter2D(Collision2D collision) 
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    // 캐릭터가 땅에서 떨어졌을 때 (2D)
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
        // 콜라이더 크기를 줄이고 오프셋을 조정하여 슬라이드 자세를 만듦 (2D 콜라이더 속성)
        capsuleCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2f);
        capsuleCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4f));

        // 일정 시간 후 슬라이드를 멈추는 코루틴 시작
        Invoke("StopSlide", slideDuration);
    }

    void StopSlide()
    {
        isSliding = false;
        // 콜라이더를 원래대로 되돌림 (2D 콜라이더 속성)
        capsuleCollider.size = originalColliderSize;
        capsuleCollider.offset = originalColliderOffset;

        animator.SetBool("IsSlide", false);
    }


}
