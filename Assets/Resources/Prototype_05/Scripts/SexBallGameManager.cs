using UnityEngine;
using System.Collections;

public class SexBallGameManager : GameManagerBase
{

    public enum Team { One, Two, None };
    public GamepadInfoHandler gamepadInfoHandler;

    public GameObject teamOneGoal;
    public GameObject teamTwoGoal;
    public GameObject ball;
    public SexGamePlayer[] player;
    public AudioClip[] boing;
    public AudioClip oh;

    public bool pause;
    public bool triggerPause;
    public float pointScorePauseLength;
    public float tipHitDisableTime;

    public int teamOneScore;
    public int teamTwoScore;

    private float gameTimeSec;
    private float gameTimeMin;
    private float gameTimeStart;
    public float gameTimeLimit;

    protected override void Start()
    {
        base.Start();
        gameState = State.PreGame;
        gamepadInfoHandler.Disable();
        canReset = true;
        SetupPlayers();
        pause = false;
        CreateEventMessage("Press SPACE to start", Color.yellow, 6.0f, 0, 600, 0);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        switch (gameState)
        {
            case State.PreGame:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
                break;
            case State.InGame:
                if (gameTimeMin >= 0 && gameTimeSec <= 0)
                {
                    gameTimeMin -= 1;
                    gameTimeSec = 59.0f;
                }
                gameTimeSec -= Time.deltaTime;
                if (triggerPause)
                {
                    StartCoroutine(PauseGame(pointScorePauseLength));
                    triggerPause = false;
                }
                if (IsGameTimeOver())
                {
                    EndGame();
                }
                break;
            case State.GameOver:
                GetWinner();
                break;
            case State.PostGame:
                break;
            default:
                break;
        }
    }
    
    void OnGUI()
    {
        int boxWidth = 100;
        int boxHeight = 40;
        int gap = 40;
        string nl = System.Environment.NewLine;

        GUI.Box(new Rect(Screen.width / 2 - boxWidth / 2, 20, boxWidth, boxHeight), gameTimeMin.ToString("0") + ":" + gameTimeSec.ToString("00"));
        GUI.Box(new Rect(Screen.width / 2 - gap - boxWidth/2, 40, boxWidth, boxHeight), "P1" + nl + teamOneScore.ToString("00"));
        GUI.Box(new Rect(Screen.width / 2 + gap - boxWidth/2, 40, boxWidth, boxHeight), "P2" + nl + teamTwoScore.ToString("00"));
    }
    

    private bool IsGameTimeOver()
    {
        if (Time.time > gameTimeStart + gameTimeLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartGame()
    {
        gameState = State.InGame;
        teamOneScore = 0;
        teamTwoScore = 0;
        gameTimeStart = Time.time;
        gamepadInfoHandler.Enable();
        gameTimeMin = Mathf.FloorToInt(gameTimeLimit / 60);
        gameTimeSec = gameTimeLimit - (60 * gameTimeMin);
        CreateEventMessage("GO!", Color.green, 3.0f, 0, 600, 0);
    }

    private void EndGame()
    {
        gameState = State.GameOver;
        CreateEventMessage("Game Over!", Color.red, 12.0f, 0, 600, 0);
    }

    private Team GetWinner()
    {
        if (teamOneScore > teamTwoScore)
        {
            return Team.One;
            CreateEventMessage("Left player wins!", Color.white, 12.0f, 0, 600, 0);
        }
        else if (teamTwoScore > teamOneScore)
        {
            return Team.Two;
            CreateEventMessage("Right player wins!", Color.white, 12.0f, 0, 600, 0);

        }
        else
        {
            return Team.None;
        }
    }

    private void SetupPlayers()
    {
        //for (int i = 0; i < player.Length; i++)
        //{
        //    player[i].SetGamepad();
        //}
        player[0].SetGamepad();
        player[1].gamepad = player[0].gamepad;
    }

    public void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ball.rigidbody.velocity = Vector3.zero;
    }

    public void ResetPlayers()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].Reset();
        }
    }

    public void ScoreGoal(Team team)
    {
        switch (team)
        {
            case Team.One:
                teamTwoScore += 2;
                CreateEventMessage("TAP IN!", Color.blue, 3.0f, 0, 600, 0);

                break;
            case Team.Two:
                teamOneScore += 2;
                CreateEventMessage("TAP IN!", new Color(1.0f, 0.75f, 0.75f), 3.0f, 0, 600, 0);

                break;
            case Team.None:
                break;
            default:
                break;
        }
        ResetBall();
        ResetPlayers();
        triggerPause = true;
        audio.PlayOneShot(oh);
    }

    public IEnumerator PauseGame(float time)
    {
        pause = true;
        gamepadInfoHandler.Disable();
        CreateEventMessage("3", new Color(1.0f, 0.75f, 0.75f), 1.0f, 0, 600, 0);
        yield return new WaitForSeconds(time/3);
        CreateEventMessage("2", new Color(1.0f, 0.75f, 0.75f), 1.0f, 0, 600, 0);
        yield return new WaitForSeconds(time/3);
        CreateEventMessage("1", new Color(1.0f, 0.75f, 0.75f), 1.0f, 0, 600, 0);
        yield return new WaitForSeconds(time/3);
        CreateEventMessage("SAFE", new Color(1.0f, 0.75f, 0.75f), 1.0f, 0, 600, 0);
        pause = false;
        gamepadInfoHandler.Enable();
    }
}