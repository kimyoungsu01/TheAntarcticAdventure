using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI gameoverscoreText;
    public TextMeshProUGUI restartsText;

    //Start is called before the first frame update
    void Start()
    {
        if (gameoverscoreText != null)
        {
            Debug.LogError("restart text is null");
        }

        if (restartsText != null)
        {
            Debug.LogError("score text is null");
        }

        restartsText.gameObject.SetActive(false);

    }

    //Update is called once per frame

    public void SetRestart()
    {
        restartsText.gameObject.SetActive(true);
    }
    public void UpdateScore(int score)
    {
        gameoverscoreText.text = score.ToString();
    }
}
