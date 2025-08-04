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
        if (gameoverscoreText == null)
        {
            Debug.LogError("restart text is null");
        }

        if (restartsText == null)
        {
            Debug.LogError("score text is null");
        }
        
        if (restartsText == null) // null üũ �� ��Ȱ��ȭ
        {
            restartsText.gameObject.SetActive(false);
        }
    }

    //Update is called once per frame

    public void SetRestart()
    {
        if (restartsText != null) // null üũ �� Ȱ��ȭ
        {
            restartsText.gameObject.SetActive(true);
        }
    }
    public void UpdateScore(float score)
    {
        if (gameoverscoreText != null) // null üũ �� ���� ������Ʈ
        {
            gameoverscoreText.text = score.ToString();
        }
    }
    public void ShowGameOverScreen(float finalScore)
    {
        
        this.gameObject.SetActive(true);// �� ��ũ��Ʈ�� �پ��ִ� ���� ������Ʈ(UI �г�) ��ü�� Ȱ��ȭ�մϴ�.
                                        
        UpdateScore(finalScore); // `UpdateScore` �޼��带 ȣ���Ͽ� ������ ������Ʈ�մϴ�.

        SetRestart(); // `SetRestart` �޼��带 ȣ���Ͽ� ����� �ؽ�Ʈ�� Ȱ��ȭ�մϴ�.
    }
    /// ���� ���� ȭ�� ��ü�� ��Ȱ��ȭ�Ͽ� ȭ�鿡�� ����ϴ�.
    public void HideGameOverScreen()
    {
        // �� ��ũ��Ʈ�� �پ��ִ� ���� ������Ʈ(UI �г�) ��ü�� ��Ȱ��ȭ�մϴ�.
        this.gameObject.SetActive(false);
    }
}
