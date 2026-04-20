using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .5f;
    public float raycastDistance = .2f;
    public float traceDistance = 2f;

    private Transform player;
    private bool isFacingRight = true; // 현재 바라보는 방향 상태

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;

        // 거리가 멀면 추적 중지
        if (direction.magnitude > traceDistance)
            return;

        // --- 방향 전환 로직 추가 ---
        // 플레이어가 몬스터보다 오른쪽에 있고 현재 왼쪽을 보고 있다면 오른쪽으로 반전
        if (direction.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        // 플레이어가 몬스터보다 왼쪽에 있고 현재 오른쪽을 보고 있다면 왼쪽으로 반전
        else if (direction.x < 0 && isFacingRight)
        {
            FlipCharacter();
        }
        // --------------------------

        Vector2 directionNormalized = direction.normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                // Space.World를 추가하여 로컬 회전값에 상관없이 월드 좌표 기준으로 이동하게 함
                transform.Translate(directionNormalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    // 캐릭터의 좌우 이미지를 반전시키는 함수
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight; // 상태 반전
        
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}