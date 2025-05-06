using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float horizontalBoundary = 5f; // 초기 x 포지션 기준으로 좌우 이동 범위
    public float verticalBoundary = 3f;   // 초기 y 포지션 기준으로 상하 이동 범위

    private float currentSpeed;
    private Vector2 moveDirection;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        // 초기 이동 방향을 랜덤으로 결정
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        moveDirection = new Vector2(randomX, randomY).normalized; // 방향 벡터 정규화

        // 초기 이동 속도를 랜덤으로 설정
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // 현재 방향으로 이동
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);

        // 좌우 경계 확인 및 방향 변경 (초기 x 포지션 기준)
        if (transform.position.x > initialPosition.x + horizontalBoundary)
        {
            transform.position = new Vector3(initialPosition.x + horizontalBoundary, transform.position.y, transform.position.z);
            moveDirection.x *= -1;
            currentSpeed = Random.Range(minSpeed, maxSpeed); // 방향 전환 시 새 속도 할당 (선택 사항)
        }
        else if (transform.position.x < initialPosition.x - horizontalBoundary)
        {
            transform.position = new Vector3(initialPosition.x - horizontalBoundary, transform.position.y, transform.position.z);
            moveDirection.x *= -1;
            currentSpeed = Random.Range(minSpeed, maxSpeed); // 방향 전환 시 새 속도 할당 (선택 사항)
        }

        // 상하 경계 확인 및 방향 변경 (초기 y 포지션 기준)
        if (transform.position.y > initialPosition.y + verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, initialPosition.y + verticalBoundary, transform.position.z);
            moveDirection.y *= -1;
            currentSpeed = Random.Range(minSpeed, maxSpeed); // 방향 전환 시 새 속도 할당 (선택 사항)
        }
        else if (transform.position.y < initialPosition.y - verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, initialPosition.y - verticalBoundary, transform.position.z);
            moveDirection.y *= -1;
            currentSpeed = Random.Range(minSpeed, maxSpeed); // 방향 전환 시 새 속도 할당 (선택 사항)
        }
    }
}
