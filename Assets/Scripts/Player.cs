using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float forwardSpeed = 8f;     // 앞으로 나아가는 속도
    public float jumpForce = 10f;       // 점프 힘
    public float slideDuration = 0.8f;  // 슬라이드 지속 시간

    public int maxHealth = 100;         // 최대 체력
    private int currentHealth;          // 현재 체력
    public int obstacleDamage = 20;     // 장애물 충돌 시 감소 체력
    public float healthDrainRate = 1.0f; // 초당 감소 체력
    private float healthDrainTimer;     // 체력 감소 타이머

    private Rigidbody2D rb;             // 2D 게임이므로 Rigidbody2D 사용
    private CapsuleCollider2D capsuleCollider; // CapsuleCollider2D로 변경

    private bool isGrounded;            // 캐릭터가 땅에 있는지 여부
    private int jumpCount;              // 현재 점프 횟수 (2단 점프를 위해)
    private const int MAX_JUMP_COUNT = 2; // 최대 점프 횟수

    private bool isSliding;             // 슬라이드 중인지 여부
    private Vector2 originalColliderSize;   // 원래 콜라이더 크기 (2D)
    private Vector2 originalColliderOffset; // 원래 콜라이더 오프셋 (2D)

  
    Animator animator;

    // 초당 에너지 -1
    // 체력회복 아이템 +20
    // 체력 100
    // 장애물 맞을때 -20
    public GameObject player;

    public int type;

    float fullenergy = 100.0f;
    float energy = 0.0f;

    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // 초기 콜라이더 크기와 오프셋을 저장 (2D 콜라이더 속성)
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;

        currentHealth = maxHealth; // 게임 시작 시 현재 체력을 최대 체력으로 설정
        healthDrainTimer = 1.0f;   // 타이머 초기화 (1초마다 감소시키기 위해)
        Debug.Log("현재 체력: " + currentHealth); // 초기 체력 확인용

        if (animator == null)
            Debug.LogError("not Found Animator");

    }

    void Update()
    {
        // 2단 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MAX_JUMP_COUNT)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 현재 y 속도를 0으로 초기화
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            animator.SetBool("IsJump", true);

        }

        // 슬라이드 (Shift 키)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            StartSlide();
            animator.SetBool("IsSlide", true);

        }

        // 초마다 체력 감소
        healthDrainTimer -= Time.deltaTime; // 지난 시간만큼 타이머 감소
        if (healthDrainTimer <= 0)
        {
            currentHealth -= (int)healthDrainRate; // 체력 감소
            Debug.Log("시간 감소! 현재 체력: " + currentHealth);
            healthDrainTimer = 1.0f; // 타이머 리셋
            CheckHealth(); // 체력 확인
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

        // 장애물과 충돌했을 때
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            currentHealth -= obstacleDamage; // 체력 감소
            Debug.Log("장애물 충돌! 현재 체력: " + currentHealth);
            CheckHealth(); // 체력 확인 

            // 충돌애니 추가해야함 내일해야지

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
        //capsuleCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2f);
        //capsuleCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4f));

        // 일정 시간 후 슬라이드를 멈추는 코루틴 시작
        Invoke("StopSlide", slideDuration);
    }

    void StopSlide()
    {
        isSliding = false;
        // 콜라이더를 원래대로 되돌림 (2D 콜라이더 속성)
        //capsuleCollider.size = originalColliderSize;
        //capsuleCollider.offset = originalColliderOffset;

        animator.SetBool("IsSlide", false);
    }

    // 체력 확인 및 게임 오버 처리 함수
    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0; // 체력이 0보다 작아지지 않도록
            Debug.Log("게임 오버! 체력이 0이 되었습니다.");
            // 여기에 게임 오버 처리 로직 추가 (예: 게임 멈추기, 다른 씬 로드 등)
            Time.timeScale = 0; // 게임 시간을 멈춤 (간단한 예시)
            // Destroy(gameObject); // 캐릭터 오브젝트 제거
        }
    }

}
