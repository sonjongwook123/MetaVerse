using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 추적할 대상 (주로 플레이어의 Transform)
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도 (값이 작을수록 더 즉각적임)
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 대상으로부터의 카메라 오프셋

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // 목표 위치에 오프셋을 적용하여 원하는 카메라 위치를 계산합니다.
            Vector3 targetPosition = target.position + offset;

            // SmoothDamp 함수를 사용하여 현재 카메라 위치에서 목표 위치로 부드럽게 이동합니다.
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        }
    }
}
