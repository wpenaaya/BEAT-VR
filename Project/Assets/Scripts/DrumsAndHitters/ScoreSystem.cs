using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ScoreSystem : MonoBehaviour
{
    public int totalScore = 0;
    public string totalScoreString = "0";
    public int perfectScore = 500;
    public int greatScore = 300;
    public int okayScore = 100;

    public int misses = 0;

    public void Clear()
    {
        totalScore = 0;
        misses = 0;
    }

    public void RecordHit(HitScores hitScore)
    {
        switch (hitScore)
        {
            case HitScores.PERFECT:
                totalScore += perfectScore;
                break;
            case HitScores.GREAT:
                totalScore += greatScore;
                break;
            case HitScores.OKAY:
                totalScore += okayScore;
                break;
            case HitScores.MISS:
                misses++;
                break;
        }
    }
}
