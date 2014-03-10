using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class P6GameManager : GameManagerBase
{
    public P6Square topRight;
    public P6Square topLeft;
    public P6Square bottomRight;
    public P6Square bottomLeft;
    private List<P6Square> pad;
    public int points;
    public int beatsPerMinute;
    public GamepadInfo gamepad;
    public GamepadInfoHandler gamepadInfoHandler;
    public float bpmIncreasePeriod;
    private float lastIncrease;
    public int bpmIncrease;
    public int combo;

    public static P6GameManager Instance
    {
        get
        {
            return (P6GameManager)GameManagerBase.Instance;
        }
    }

    protected override void Start()
    {
        base.Start();
        pad = new List<P6Square>();
        pad.Add(topLeft);
        pad.Add(topRight);
        pad.Add(bottomLeft);
        pad.Add(bottomRight);
        Physics.gravity = Vector3.zero;
        lastIncrease = Time.time;
        gameState = State.InGame;
        combo = 0;
    }

    protected override void FixedUpdate()
    {
        switch (gameState)
        {
            case State.PreGame:
                break;

            case State.InGame:
                if (Time.time > lastIncrease + bpmIncreasePeriod)
                {
                    beatsPerMinute += bpmIncrease;
                    lastIncrease = Time.time;
                }

                if (gamepad != null)
                {
                    if (gamepad.buttonDown[(int)GamepadInfo.Button.x])
                    {
                        int p = topLeft.Press();
                        points += p * combo;
                    }
                    if (gamepad.buttonDown[(int)GamepadInfo.Button.a])
                    {
                        int p = bottomLeft.Press();
                        points += p * combo;
                    }
                    if (gamepad.buttonDown[(int)GamepadInfo.Button.y])
                    {
                        int p = topRight.Press();
                        points += p * combo;
                    }
                    if (gamepad.buttonDown[(int)GamepadInfo.Button.b])
                    {
                        int p = bottomRight.Press();
                        points += p * combo;
                    }
                }
                else
                {
                    gamepad = GamepadInfoHandler.Instance.AttachControllerToPlayer(gameObject);
                }

                if (points < 0)
                {
                    gameState = State.GameOver;
                }
                break;

            case State.GameOver:
                GameManagerBase.Instance.CreateEventMessage("GAME OVER", Color.red, 1.5f, 0.1f, 200, 0, false);
                gameState = State.PostGame;
                break;

            case State.PostGame:
                StartCoroutine(Restart());
                break;

            default:
                break;
        }
    }

    public void PulseAll(Color color, float scale)
    {
        for (int i = 0; i < pad.Count; i++)
        {
            pad[i].GetComponentInChildren<TweenPulse>().Pulse(color, scale);
            audio.PlayOneShot(audio.clip);
        }
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(Application.loadedLevel);
    }
}