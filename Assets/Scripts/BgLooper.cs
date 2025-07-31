using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BgLooper : MonoBehaviour
{
    public string groundTag = "Ground";
    public string backgroundTag = "BackGround";

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        if (obstacles.Length > 0)
        {
            obstacleLastPosition = obstacles[0].transform.position;
            obstacleCount = obstacles.Length;

            for (int i = 0; i < obstacleCount; i++)
            {
                obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parentSet = collision.transform.parent;
        if (parentSet == null) return;

        if (parentSet.CompareTag(groundTag) || parentSet.CompareTag(backgroundTag))
        {
            string tag = parentSet.tag;
            MoveSetNextToRightmost(parentSet, tag);
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
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
            if (rightEdge > maxRightEdge)
                maxRightEdge = rightEdge;
        }

        float setLeftEdge = GetSetLeftEdge(setToMove);

        Vector3 newPos = setToMove.position;

        // 다음 세트의 왼쪽 끝이 현재 가장 오른쪽 끝에 딱 붙도록 위치 조정
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
            if (colRight > maxRight)
                maxRight = colRight;
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
            if (colLeft < minLeft)
                minLeft = colLeft;
        }

        return minLeft;
    }
}
