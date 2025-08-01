using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Item : Item
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public int ScoreAmt = 0; // 골드 아이템 타입


    protected override void OnInteract()
    {

        Debug.Log("Gold_Item OnInteract 호출됨!");
        Debug.Log("ScoreAmt 값: " + ScoreAmt);

        if (gameManager == null)
            Debug.LogError("GameManager가 null입니다!");

        Destroy(gameObject);
        gameManager.addscore(ScoreAmt);

    }
}
