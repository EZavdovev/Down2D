using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int ScorePlayer;
    public int Record;
    public int Floor;
    private float Timer;
    public Text Txt;
    public Text Floortxt;
    public Text Recordtxt;
    private const int Kill_score = 10;
    private const int Fall_score = 1000;
    private void Start()
    {
        GameManager.EnemyDied.Subscribe(ChangeScore);
        GameManager.PlayerFall.Subscribe(ChangeScore);
        GameManager.PlayerFall.Subscribe(ChangeFloor);

        Record = PlayerPrefs.GetInt("Record", Record);
    }

    private int CalculateScore(string type_score)
    {
        if (type_score == "Enemy") return Kill_score;
        if (type_score == "Fall") return Fall_score;
        return 0;
    }
    public void ChangeScore(string type_score)
    {
        ScorePlayer += CalculateScore(type_score);
    }

    public void ChangeFloor(string type_score)
    {
        if(type_score == "Fall")
            Floor++;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Record", Record);
        Txt.text = "Score: " + ScorePlayer;
        Floortxt.text = "Floors: " + Floor;
        Recordtxt.text = "Record: " + Record;
        Timer += 1 * Time.deltaTime;
        if(Timer > 1)
        {
            ScorePlayer += 1;
            Timer = 0;
        }
        if (ScorePlayer > Record)
            Record = ScorePlayer;
    }
}
