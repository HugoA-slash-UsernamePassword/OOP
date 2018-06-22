using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static int score;
    public int goal = 0;
    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void LateUpdate()
    {
        if(score >= 24)
        {
            goal = score + 1;
        }
    }
    // Update is called once per frame
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        GUI.Label(new Rect(scrW * 0.25f, scrH * 0.25f, scrW * 2f, scrH * 0.5f), "Score: " + score);
        GUI.Label(new Rect(scrW * 0.25f, scrH * 1f, scrW * 2f, scrH * 0.5f), "Score " + goal + " points to win");
        if(score <= -10)
        {
            GUI.Label(new Rect(scrW * 0.25f, scrH * 1.75f, scrW * 2f, scrH * 0.5f), "Press 'R' to restart");
        }
    }
}
