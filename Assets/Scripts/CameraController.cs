using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.125f; // ī�޶� �÷��̾ ���󰡴� �ӵ� (�ε巯�� ����)
    public Vector3 offset;             // �÷��̾�� ī�޶� ������ �Ÿ� ����

    // ī�޶� Y�� ��ġ ���� (�÷��̾� Y��� ������� ������ ���� ����)
    // public float fixedYPosition = 2.0f; 

    private void LateUpdate() // LateUpdate�� ��� Update �Լ��� ȣ��� �� ȣ��Ǿ� �� �ε巯�� ī�޶� �������� �����մϴ�.
    {
        if (player == null) return; // �÷��̾ �Ҵ���� �ʾ����� �ƹ��͵� ���� ����

        // ī�޶� ���� ��ǥ ��ġ (�÷��̾��� x, ������ y, ������ z)
        // ���� y�൵ ���󰡷��� player.transform.position.y�� ����ϼ���.
        // �׸��� fixedYPosition�� �����ؾ� �մϴ�.
        Vector3 desiredPosition = new Vector3(player.transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);

        // Lerp�� ����Ͽ� ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
