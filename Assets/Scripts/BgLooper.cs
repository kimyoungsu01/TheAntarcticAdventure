using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public string groundTag = "Ground";
    public string backgroundTag = "BackGround";
    public string obstacleTag = "Obstacle";  // 장애물 세트 부모 태그

    void Start()
    {
        // 시작 시 별도의 처리는 필요 없음
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parentSet = collision.transform.parent;
        if (parentSet == null) return;

        string parentTag = parentSet.tag;

        // Ground, Background, Obstacle 모두 동일하게 루프 처리
        if (parentTag == groundTag || parentTag == backgroundTag || parentTag == obstacleTag)
        {
            MoveSetNextToRightmost(parentSet, parentTag);
        }
    }

    // 세트를 같은 태그 내에서 가장 오른쪽 세트 옆으로 이동시키는 함수
    void MoveSetNextToRightmost(Transform setToMove, string tag)
    {
        GameObject[] sets = GameObject.FindGameObjectsWithTag(tag);

        float maxRightEdge = float.MinValue;

        // 현재 세트를 제외하고 오른쪽 끝 위치를 찾음
        foreach (GameObject set in sets)
        {
            if (set == setToMove.gameObject) continue;

            float rightEdge = GetSetRightEdge(set.transform);
            if (rightEdge > maxRightEdge)
                maxRightEdge = rightEdge;
        }

        float setLeftEdge = GetSetLeftEdge(setToMove);

        // 새 위치 계산 → 기존과 동일하게 "딱 붙임"
        Vector3 newPos = setToMove.position;
        newPos.x += maxRightEdge - setLeftEdge;

        setToMove.position = newPos;
    }

    // 세트의 오른쪽 가장자리 X 좌표 반환
    float GetSetRightEdge(Transform set)
    {
        float maxRight = float.MinValue;
        BoxCollider2D[] colliders = set.GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D col in colliders)
        {
            float colRight = col.transform.position.x + (col.size.x * col.transform.lossyScale.x / 2f);
            if (colRight > maxRight)
                maxRight = colRight;
        }

        return maxRight;
    }

    // 세트의 왼쪽 가장자리 X 좌표 반환
    float GetSetLeftEdge(Transform set)
    {
        float minLeft = float.MaxValue;
        BoxCollider2D[] colliders = set.GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D col in colliders)
        {
            float colLeft = col.transform.position.x - (col.size.x * col.transform.lossyScale.x / 2f);
            if (colLeft < minLeft)
                minLeft = colLeft;
        }

        return minLeft;
    }
}
