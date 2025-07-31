using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{

    public int Score;
    public int hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        void hp(/*펭귄*/)
        {
            //if (collision.gameObject.CompareTag("장애물")) 
            //{
            //    Debug.Log("장애물");
            //}
        }

        void Score(/*펭귄*/)
        {
            //if (collision.gameObject.CompareTag("펭귄"))
            //{
            //    Debug.Log("점수가 올라간다");
            //}
        }
    }
}
