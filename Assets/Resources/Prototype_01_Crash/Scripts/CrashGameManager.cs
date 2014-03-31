using UnityEngine;

public class CrashGameManager : MonoBehaviour
{

    public enum GameState { Playing, GameOver };
    public GameState gameState;
    public int score;
    public int birds;
    public int startingBirds;
    public int scoreForFreebird;
    private int lastFreebird;
    public Judge judge;
    public AudioClip[] sfx;

    public static CrashGameManager Instance()
    {
        return FindObjectOfType<CrashGameManager>();
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                if (score >= scoreForFreebird + lastFreebird)
                {
                    birds++;
                    lastFreebird = score;
                    Debug.Log("Freebird!");
                }
                if (birds == 0 && FindObjectOfType<Bird>() == null)
                {
                    audio.PlayOneShot(sfx[2]);
                    gameState = GameState.GameOver;
                    if (PlayerPrefs.GetInt("CrashHighScore", 0) < score)
                    {
                        PlayerPrefs.SetInt("CrashHighScore", score);
                    }
                }
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
        

    }

    public void OnGUI()
    {
        GUI.Box(new Rect(0.0f, 20.0f, 100.0f, 20.0f), "Score: " + score);
        GUI.Box(new Rect(0.0f, 0.0f, 100.0f, 20.0f), "High Score: " + PlayerPrefs.GetInt("CrashHighScore", 0));
        if (birds > 0)
        {
            GUI.Box(new Rect(0.0f, 40.0f, 100.0f, 20.0f), "Birds: " + birds);
        }
        else if (gameState == GameState.GameOver)
        {
            GUI.Box(new Rect(0.0f, 40.0f, 100.0f, 20.0f), "Game Over!");
        }

        if (GUI.Button(new Rect(Screen.width - 100.0f, 10.0f, 50.0f, 20.0f), "Reset"))
        {
            ResetGame();
        }
    }

    public static void Score(int val)
    {        
        CrashGameManager gm = Instance();
        gm.audio.PlayOneShot(gm.sfx[0]);
        Judge j = gm.judge;
        gm.score += val;
        if (val == 1) // 1
        {
            j.spriteRenderer.sprite = j.sprites[1];
        }
        else if (val == 2) // 2
        {
            j.spriteRenderer.sprite = j.sprites[2];
        }
        else if (val == 3) // 3
        {
            j.spriteRenderer.sprite = j.sprites[3];
        }
        else if (val == 4 || val == 5) // 4 - 5
        {
            j.spriteRenderer.sprite = j.sprites[4];
        }
        else if (val > 6) // > 6
        {
            j.spriteRenderer.sprite = j.sprites[5];
        }
        else
        {
            j.spriteRenderer.sprite = j.sprites[0];
        }
        j.Activate();
        Debug.Log("Score: " + gm.score);
    }

    public void UseBird()
    {
        birds -= 1;
        Instance().audio.PlayOneShot(Instance().sfx[1]);
        Debug.Log(birds + " birds left!");
    }

    private void ResetGame()
    {
        birds = startingBirds;
        score = 0;
        lastFreebird = 0;
        gameState = GameState.Playing;
    }
}