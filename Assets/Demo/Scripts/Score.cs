using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore;
    public int targetScore;

    public Score(int newCurrentScore, int newTargetScore)
    {
        this.currentScore = newCurrentScore;
        this.targetScore = newTargetScore;
    }

    public void setCurrentScore (int newCurrentScore)
    {
        this.currentScore = newCurrentScore;
    }

    public int getCurrentScore ()
    {
        return this.currentScore;
    }

    public void setTargetScore (int newTargetScore)
    {
        this.targetScore = newTargetScore;
    }

    public int getTargetScore ()
    {
        return this.targetScore;
    }

    public int scoreIterator()
    {
        if (getCurrentScore() < getTargetScore())
        {
            setCurrentScore(getCurrentScore() + 1);
            Debug.Log("You have gained a point! Your current score is " + getCurrentScore());
        }
        else
        {
            Debug.Log("You have collected all the points you needed to reach the target score!");
        }
        return getCurrentScore();
    }

    public int scoreDecrementor ()
    {
        setCurrentScore(getCurrentScore() - 1);
        Debug.Log("You have lost a point! Your current score is " + getCurrentScore());
        return getCurrentScore();
    }
}
