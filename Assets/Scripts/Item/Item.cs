using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private AudioClip InteractClip;

    protected abstract void OnInteract(); //추상 메서드

    public void interact()
    {
        GameManager.Instance.SE.PlayOneShot(InteractClip);

        Debug.Log("아이템을 획득했습니다.");
        OnInteract();

        // ✅ 파괴 대신 비활성화
        gameObject.SetActive(false);

    }
}
