using System.Collections;
using UnityEngine;

public class FishingGameManager : GameManagerBase
{
    public GameObject player;
    public Color colorTarget;
    public float cameraColorLerpSpeed;
    public AudioClip waterDrop;

    protected override void Start()
    {
        base.Start();
        Physics.gravity = new Vector3(0, 0.0f, 0);
        SwitchGameState(State.InGame);
        Screen.lockCursor = true;
        Camera.main.backgroundColor = Color.black;
    }

    protected override void Update()
    {
        base.Update();
        switch (gameState)
        {
            case State.PreGame:
                break;

            case State.InGame:
                #region scripted stuff for start of game

                if (Input.GetButton("Vertical"))
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, colorTarget, cameraColorLerpSpeed * Time.deltaTime);
                    }
                }

                #endregion scripted stuff for start of game

                if (DialogueController.Instance.state == DialogueController.DialogueState.endOfTree)
                {
                    audio.Stop();
                    audio.PlayOneShot(waterDrop);
                    SwitchGameState(State.GameOver);
                }

                break;

            case State.GameOver:
                Camera.main.backgroundColor = Color.black;
                break;

            case State.PostGame:
                break;

            default:
                break;
        }
    }
}