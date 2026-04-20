using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isMovingRight = true;
    
    // 방향 전환을 위해 SpriteRenderer가 필요하다면 선언 (선택사항)
    // private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 만약 FlipX를 사용하고 싶다면 여기서 GetComponent<SpriteRenderer>()를 호출하세요.
    }

    private void Update()
    {
        // 이동 로직
        if (isMovingRight)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            // 방향 상태 변경
            isMovingRight = !isMovingRight;
            
            // 시각적인 방향 전환 함수 호출
            FlipCharacter();
        }
    }

    // 캐릭터의 좌우 이미지를 반전시키는 함수
    private void FlipCharacter()
    {
        // 현재 캐릭터의 localScale을 가져옵니다.
        Vector3 currentScale = transform.localScale;
        
        // X축 값에 -1을 곱하여 반전시킵니다.
        currentScale.x *= -1;
        
        // 변경된 값을 다시 적용합니다.
        transform.localScale = currentScale;
    }
}