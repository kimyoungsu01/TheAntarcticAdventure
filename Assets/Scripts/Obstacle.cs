using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = UnityEngine.Random.Range(holeSizeMin, holeSizeMax);

        float minGap = 2f;
        if (holeSize < minGap) holeSize = minGap;

        float offsetY = UnityEngine.Random.Range(lowPosY, highPosY);

        // ✅ 좌우 퍼짐 정도 설정
        float horizontalSpread = 2f; // ← 퍼짐 범위 (조절 가능)

        // ✅ 탑/바텀 위치를 Y뿐 아니라 X도 랜덤 오프셋 적용
        float topY = offsetY + (holeSize / 2f) + UnityEngine.Random.Range(0.5f, 2f);
        float bottomY = offsetY - (holeSize / 2f) - UnityEngine.Random.Range(0.5f, 2f);

        float topX = UnityEngine.Random.Range(-horizontalSpread, horizontalSpread);
        float bottomX = UnityEngine.Random.Range(-horizontalSpread, horizontalSpread);

        // ✅ 개별 오브젝트에 위치 적용 (X, Y 모두 랜덤)
        topObject.localPosition = new Vector3(topX, topY, 0);
        bottomObject.localPosition = new Vector3(bottomX, bottomY, 0);

        // ✅ 장애물 전체 위치 (X는 이전 장애물 기준으로 일정한 간격)
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0, 0);
        transform.position = placePosition;

        return placePosition;
    }

}
