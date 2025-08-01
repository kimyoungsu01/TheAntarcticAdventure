using UnityEngine;
using System.Collections; // 코루틴을 사용하려면 이 네임스페이스가 필요합니다.

public class Player : MonoBehaviour
{
    public float wallPushBackForce = 2.0f; // 벽에 부딪혔을 때 뒤로 밀어내는 힘

    public float forwardSpeed = 5f;        // 앞으로 나아가는 속도
    public float jumpForce = 10f;          // 점프 힘
    public float slideDuration = 0.8f;     // 슬라이드 지속 시간

    public int maxHealth = 100;            // 최대 체력
    public int currentHealth;             // 현재 체력-- 황상욱, 전역변수로 전환
    public int obstacleDamage = 20;
    public int potionHealAmount = 20;
    public float healthDrainRate = 1.0f;   // 초당 감소 체력
    private float healthDrainTimer;        // 체력 감소 타이머

    // 무적 관련 변수
    public float invincibilityDuration = 2.0f; // 무적 시간 (초)
    private bool isInvincible = false;       // 현재 무적 상태인지 여부

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private int jumpCount;
    private const int MAX_JUMP_COUNT = 2;

    private bool isSliding;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    Animator animator;
    //-황상욱
    GameManager gameManager;
    // 초당 에너지 -1 
    // 체력회복 아이템 +20 
    // 체력 100
    // 장애물 맞을때 -20 

    void Start()
    {
        //-황상욱-게임메니저 인스턴스 추가
        gameManager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // 초기 콜라이더 크기와 오프셋을 저장 
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;

        currentHealth = maxHealth; // 게임 시작 시 현재 체력을 최대 체력으로 설정
        healthDrainTimer = 1.0f;   // 타이머 초기화 (1초마다 감소시키기 위해)
        Debug.Log("현재 체력: " + currentHealth); // 초기 체력 확인용

        if (animator == null)
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다! (GetComponentInChildren 확인)");
        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer 컴포넌트를 찾을 수 없습니다. 무적 시 깜빡임 효과를 적용할 수 없습니다.");
    }

    void Update()
    {
        // 2단 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < MAX_JUMP_COUNT)
        {
            if (jumpCount < MAX_JUMP_COUNT)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // 현재 y 속도를 0으로 초기화
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
                animator.SetBool("IsJump", true);
            }
        }

        // 슬라이드 (Shift 키)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            StartSlide();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isSliding)
        {
            StopSlide();
        }


        // 초마다 체력 감소
        healthDrainTimer -= Time.deltaTime;
        if (healthDrainTimer <= 0)
        {
            if (!isInvincible) // 무적 상태가 아닐 때만 체력 감소
            {
                currentHealth -= (int)healthDrainRate;
                Debug.Log("시간 감소! 현재 체력: " + currentHealth);
                CheckHealth();
            }
            healthDrainTimer = 1.0f; // 무적 상태와 관계없이 타이머는 계속 리셋
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);
    }

    // --- 체력 및 게임 오버 처리 ---
    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0; // 체력이 0보다 작아지지 않도록
            Debug.Log("게임 오버! 체력이 0이 되었습니다.");
            Time.timeScale = 0; // 게임 시간을 멈춤 (간단한 예시)
                                // Destroy(gameObject); // 캐릭터 오브젝트 제거
                                // 게임 오버 처리 시작
            if (GameManager.Instance != null) // GameManager 인스턴스가 존재하는지 확인
            {
                GameManager.Instance.EndGame(); // GameManager의 EndGame 메서드 호출 (게임 오버 처리 시작)
            }
        }



    }
    // <--- 여기: HealHealth 함수 추가 시작
    // 체력을 회복시키는 새로운 함수
    private void HealHealth(int amount)
    {
        currentHealth += amount; // 체력 증가
        // 최대 체력을 넘지 않도록 제한
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("체력 회복! 현재 체력: " + currentHealth);

    }


    // --- 슬라이드 관련 함수 ---
    void StartSlide()
    {

        if (isSliding) return;


        isSliding = true;
        animator.SetBool("IsSlide", true);


        // 슬라이드 콜라이더 크기/오프셋 조절 (주석 해제하여 사용 가능)
        if (capsuleCollider != null)
        {
            capsuleCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2f); // 높이를 절반으로
            capsuleCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4f)); // Y 오프셋을 아래로 이동
        }
        // Invoke("StopSlide", slideDuration);
    }

    void StopSlide()
    {
        if (!isSliding) return;

        isSliding = false;
        animator.SetBool("IsSlide", false);

        // 슬라이드 콜라이더 크기/오프셋 복원 (주석 해제하여 사용 가능)
        if (capsuleCollider != null)
        {
            capsuleCollider.size = originalColliderSize;
            capsuleCollider.offset = originalColliderOffset;
        }

        animator.SetBool("IsSlide", false);
    }


    // --- 충돌 및 트리거 감지 ---

    // 바닥과의 물리적 충돌 감지 (Is Trigger가 아닌 콜라이더)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("IsJump", false);
        }
        // 물리적인 벽과의 충돌 처리 (선택 사항, Obstacle과 분리할 경우)
        // else if (collision.gameObject.CompareTag("Wall"))
        // {
        //    rb.velocity = new Vector2(0, rb.velocity.y);
        //    Vector2 pushDirection = collision.contacts[0].normal.normalized * 0.1f;
        //    transform.position += (Vector3)pushDirection;
        //    Debug.Log("벽에 충돌! 다시 내려감.");
        // }
    }

    // 캐릭터가 땅에서 떨어졌을 때 (2D)
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 땅에서 떨어짐
            // animator.SetBool("IsJump", true); // 땅에서 떨어졌을 때 점프 애니메이션 유지 등의 로직
        }
    }

    // 장애물과의 트리거 충돌 감지 (Is Trigger가 체크된 콜라이더)
    void OnTriggerEnter2D(Collider2D other) // Collision2D 대신 Collider2D를 매개변수로 사용
    {
        if (other.CompareTag("Obstacle")) // "Obstacle" 태그를 가진 오브젝트와 트리거 충돌
        {
            if (!isInvincible) // 무적 상태가 아닐 때만 데미지 적용
            {
                currentHealth -= obstacleDamage; // 체력 감소
                Debug.Log("장애물(트리거) 충돌! 현재 체력: " + currentHealth);
                CheckHealth();

                // 넉백 효과 추가
                // 캐릭터의 현재 속도에 반대 방향으로 힘을 가하여 밀어냅니다.
                // ForceMode2D.Impulse는 즉각적인 힘을 가합니다.
                // rb.mass를 곱해주면 캐릭터의 질량에 상관없이 일정한 넉백 느낌을 낼 수 있습니다.
                rb.AddForce(new Vector2(-wallPushBackForce * rb.mass, 0), ForceMode2D.Impulse);

                StartInvincibility(); // 무적 상태 시작
            }

        }
        /* 
    else if (other.CompareTag("Potion"))

{
    HealHealth(potionHealAmount);  // <--- 이 부분이 호출되어야 합니다.
    Destroy(other.gameObject);
    Debug.Log("포션 획득! 현재 체력: " + currentHealth);
}
*/
        else if (other.CompareTag("Item"))
        {
            Debug.Log("아이템 획득!"); // 아이템 획득 로그
            var item = other.GetComponent<Item>(); 
            item.interact();
        }
        // 장애물이 이제 물리적으로 막지 않으므로, 충돌 시 밀어내는 코드는 필요 없거나,
        // 시각적인 밀어내기 효과를 원한다면 AddForce를 약하게 한 번만 주는 방식 고려
        // 예: rb.AddForce(new Vector2(-wallPushBackForce, 0), ForceMode2D.Impulse);

        //- 황상욱 태그를 아이템으로 바꾸고 getcomponent로 item 클래스를 받아오고 interact()호출

    }

    // --- 무적 관련 함수 ---
    void StartInvincibility()
    {
        isInvincible = true;
        Debug.Log("무적 상태 시작!");

        // 무적 시간 후 무적 해제
        Invoke("EndInvincibility", invincibilityDuration);

        // 선택 사항: 무적 시 시각적 피드백 (깜빡임 코루틴 시작)
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashPlayer());
        }
    }

    void EndInvincibility()
    {
        isInvincible = false;
        Debug.Log("무적 상태 종료!");

        if (spriteRenderer != null)
        {
            StopCoroutine(FlashPlayer()); // 코루틴이 여전히 실행 중일 수 있으므로 안전하게 중지
            spriteRenderer.enabled = true; // 깜빡임 중 숨겨졌다면 다시 보이게 함
            spriteRenderer.color = Color.white; // 원래 색상으로 복구
        }
    }

    IEnumerator FlashPlayer()
    {
        float flashInterval = 0.1f; // 깜빡이는 간격
        float timer = 0f;

        while (timer < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // 스프라이트 켜고 끄기
            // 또는 spriteRenderer.color = (spriteRenderer.color == Color.white) ? Color.red : Color.white; // 색상 변경
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }
        spriteRenderer.enabled = true; // 무적 시간이 끝나면 다시 보이게 함
        spriteRenderer.color = Color.white; // 원래 색상으로 복구
    }
}