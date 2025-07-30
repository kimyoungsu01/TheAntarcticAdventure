using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.125f; // 카메라가 플레이어를 따라가는 속도 (부드러움 정도)
    public Vector3 offset;             // 플레이어와 카메라 사이의 거리 조정

    // 카메라 Y축 위치 고정 (플레이어 Y축과 상관없이 일정한 높이 유지)
    // public float fixedYPosition = 2.0f; 

    private void LateUpdate() // LateUpdate는 모든 Update 함수가 호출된 후 호출되어 더 부드러운 카메라 움직임을 제공합니다.
    {
        if (player == null) return; // 플레이어가 할당되지 않았으면 아무것도 하지 않음

        // 카메라가 따라갈 목표 위치 (플레이어의 x, 고정된 y, 고정된 z)
        // 만약 y축도 따라가려면 player.transform.position.y를 사용하세요.
        // 그리고 fixedYPosition은 제거해야 합니다.
        Vector3 desiredPosition = new Vector3(player.transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);

        // Lerp를 사용하여 현재 위치에서 목표 위치로 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
