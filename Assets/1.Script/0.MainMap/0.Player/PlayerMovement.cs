using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 (Inspector 창에서 조절 가능)
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트

    void Start()
    {
        // SpriteRenderer 컴포넌트를 찾아서 할당합니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
            enabled = false;
        }
    }

    void Update()
    {
        // 수평(Horizontal) 및 수직(Vertical) 입력 값을 받습니다.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 이동 방향 벡터를 생성합니다.
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        float moveDistance = 0;
        // 매 프레임 이동할 거리를 계산합니다.
        if (GetComponent<Player>().isPet == true)
        {
            moveDistance = moveSpeed * 2 * Time.deltaTime;
        }
        else
        {
            moveDistance = moveSpeed * Time.deltaTime;
        }
        // transform.Translate를 사용하여 게임 오브젝트의 위치를 이동시킵니다.
        transform.Translate(movement * moveDistance);

        // 왼쪽으로 움직일 때 스프라이트 X 축 반전
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            if (GetComponent<Player>().isPet == true)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
        }
        // 오른쪽으로 움직일 때 스프라이트 X 축 반전 해제
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            if (GetComponent<Player>().isPet == true)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
        }
    }
}