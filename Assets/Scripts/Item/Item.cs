using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void OnInteract(); //추상 메서드

    public void interact()
    {
        OnInteract();
        Destroy(gameObject);
        //아이템을 먹었을 떄 사라지는 코드

    }
}
