using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public string groundTag = "Ground";
    public string backgroundTag = "BackGround";
    public string obstacleTag = "Obstacle";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parentSet = collision.transform.parent;
        if (parentSet == null) return;

        string parentTag = parentSet.tag;

        if (parentTag == groundTag || parentTag == backgroundTag || parentTag == obstacleTag)
        {
            MoveSetNextToRightmost(parentSet, parentTag);

            // ✅ 세트 이동 후 비활성화된 아이템 다시 활성화
            ReactivateItems(parentSet);
        }
    }

    void MoveSetNextToRightmost(Transform setToMove, string tag)
    {
        GameObject[] sets = GameObject.FindGameObjectsWithTag(tag);
        float maxRightEdge = float.MinValue;

        foreach (GameObject set in sets)
        {
            if (set == setToMove.gameObject) continue;
            float rightEdge = GetSetRightEdge(set.transform);
            if (rightEdge > maxRightEdge) maxRightEdge = rightEdge;
        }

        float setLeftEdge = GetSetLeftEdge(setToMove);
        Vector3 newPos = setToMove.position;
        newPos.x += maxRightEdge - setLeftEdge;
        setToMove.position = newPos;
    }

    float GetSetRightEdge(Transform set)
    {
        float maxRight = float.MinValue;
        BoxCollider2D[] colliders = set.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            float colRight = col.transform.position.x + (col.size.x * col.transform.lossyScale.x / 2f);
            if (colRight > maxRight) maxRight = colRight;
        }
        return maxRight;
    }

    float GetSetLeftEdge(Transform set)
    {
        float minLeft = float.MaxValue;
        BoxCollider2D[] colliders = set.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            float colLeft = col.transform.position.x - (col.size.x * col.transform.lossyScale.x / 2f);
            if (colLeft < minLeft) minLeft = colLeft;
        }
        return minLeft;
    }

    // ✅ 세트 안의 비활성화된 아이템들을 다시 켜줌
    void ReactivateItems(Transform set)
    {
        foreach (Item item in set.GetComponentsInChildren<Item>(true)) // (true) → 비활성화 포함 검색
        {
            item.gameObject.SetActive(true);
        }
    }
}
