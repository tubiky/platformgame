using UnityEditorInternal;
using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float horizontalInput;
   public float maxHealth = 100;
   public float currentHealth;

   private float jumpForce = 400f; // 점프 힘
   private float moveSpeed = 6f;
   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = true; // 바닥에 닿았는지 나타냄
   private bool m_FacingRight = true;
   private bool isDead = false; // 사망 상태
   
   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   public HealthBar healthBar;
    
   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        animator.SetBool("Grounded", true);
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("w") && jumpCount < 2)
        {
            Jump();
        }

        else if (horizontalInput != 0)
        {
            Movement();
        }   
    }

    private void Movement()
    {
        playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, playerRigidbody.velocity.y);

        if (horizontalInput < 0 && m_FacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !m_FacingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        animator.SetBool("Grounded", false);
        isGrounded = false;
        jumpCount++;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2(0, jumpForce));
        playerAudio.Play();

        if (jumpCount == 1)
        {
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 0.7f));
            playerAudio.Play();
        }
    }

    private void TakeDamge()
    {
        currentHealth -= 20;
        healthBar.SetCurrentValue(currentHealth);
    }

   private void Die() 
    {
        animator.SetTrigger("Die");

        playerRigidbody.velocity = Vector2.zero;

        playerAudio.clip = deathClip;
        playerAudio.Play();

        isDead = true;

        GameManager.instance.OnPlayerDead();
   }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0);
    }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
       // 바닥에 닿았음을 감지하는 처리
       if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
            jumpCount = 0;
        } 
        
        else if (collision.collider.tag == "Enemy") 
        {
            TakeDamge();
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
   }
}