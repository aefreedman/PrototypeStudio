using UnityEngine;
using System.Collections;

public class CrashGameManager : MonoBehaviour
{

    public int score;
    public int birds;
    public int scoreForFreebird;
    private int lastFreebird;

    public static CrashGameManager Instance()
    {
        return FindObjectOfType<CrashGameManager>();
    }

    void Start()
    {
        score = 0;
        lastFreebird = 0;
    }

    void Update()
    {
        if (score >= scoreForFreebird + lastFreebird)
        {
            birds++;
            lastFreebird = score;
            Debug.Log("Freebird!");
        }
        if (birds <= 0)
        {
            Debug.Log("GameOver!");
        }
    }

    public static void Score(int val)
    {
        Instance().score += val;
        Debug.Log("Score: " + Instance().score);
    }

    public void UseBird()
    {
        birds -= 1;
        Debug.Log(birds + " birds left!");
    }
}
