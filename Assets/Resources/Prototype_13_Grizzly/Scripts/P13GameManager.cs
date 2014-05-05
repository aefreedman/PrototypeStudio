using UnityEngine;
using System.Collections;

public class P13GameManager : GameManagerBase
{
    public GameObject root;
    public GameObject player;
    private FirstPersonDrifter playerSettings;
    public SpriteRenderer[] GuiSprite;
    public TextMesh text;

    #region Resources

    public enum Resources { Hunger, Thirst, Stamina, Sanity };

    public int[] resource;
    public float[] decayPeriod;
    private float[] lastResourceDecayTime;
    private bool regenStaminaLock;

    #endregion Resources

    #region Audio

    public AudioClip[] walkClip;
    public float walkClipPeriod;
    private float lastWalkClipPlay;

    #endregion Audio

    #region Time

    public enum TimeOfDay { Dawn, Morning, Afternoon, Dusk, Night };

    public TimeOfDay timeOfDay;

    #endregion Time

    private static P13GameManager instance;

    public static P13GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<P13GameManager>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        text.text = "";
        if (root == null)
        {
            GameObject.Find("_root");
        }

        playerSettings = player.GetComponent<FirstPersonDrifter>();
        timeOfDay = TimeOfDay.Dawn;

        lastResourceDecayTime = new float[4];
        for (int i = 0; i < lastResourceDecayTime.Length; i++)
        {
            lastResourceDecayTime[i] = Time.time;
        }
        lastWalkClipPlay = Time.time;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        switch (gameState)
        {
            case State.PreGame:
                gameState = State.InGame;
                break;

            case State.InGame:
                DecayResources();
                RunResourceGUI();

                //Color c = RenderSettings.skybox.GetColor("_Tint");
                //float h, s, v;
                //Utilities.ColorToHSV(c, out h, out s, out v);
                ////v -= Time.deltaTime / 100.0f;
                //RenderSettings.skybox.SetColor("_Tint", Utilities.GetHSVUnitFromHSV255(h, s, v));

                if (resource[(int)Resources.Stamina] > 30)
                {
                    playerSettings.enableRunning = true;
                    playerSettings.walkSpeed = 6.0f;
                    if (resource[(int)Resources.Stamina] > 90)
                    {
                        playerSettings.walkSpeed = 7.0f;
                    }
                    if (Input.GetButton("Run"))
                    {
                        walkClipPeriod = 0.3f;
                        decayPeriod[(int)Resources.Stamina] = 0.1f;
                        decayPeriod[(int)Resources.Hunger] = 1.0f;
                        decayPeriod[(int)Resources.Thirst] = 1.0f;
                    }
                    else
                    {
                        walkClipPeriod = 0.5f;
                        decayPeriod[(int)Resources.Stamina] = 5.0f;
                        decayPeriod[(int)Resources.Hunger] = 5.0f;
                        decayPeriod[(int)Resources.Thirst] = 5.0f;
                    }
                    if (!player.GetComponentInChildren<HeadBob>().enabled)
                    {
                        player.GetComponentInChildren<HeadBob>().enabled = true;
                    }
                }
                else
                {
                    playerSettings.walkSpeed = 2.0f;
                    walkClipPeriod = 0.8f;
                    decayPeriod[(int)Resources.Stamina] = 5.0f;
                    playerSettings.enableRunning = false;
                    player.GetComponentInChildren<HeadBob>().enabled = false;
                }

                if (resource[(int)Resources.Hunger] > 30 && resource[(int)Resources.Thirst] > 30 && !Input.GetButton("Run") && !regenStaminaLock)
                {
                    StartCoroutine(RegenStamina());
                }

                switch (timeOfDay)
                {
                    case TimeOfDay.Dawn:
                        break;

                    case TimeOfDay.Morning:
                        break;

                    case TimeOfDay.Afternoon:
                        break;

                    case TimeOfDay.Dusk:
                        break;

                    case TimeOfDay.Night:
                        break;

                    default:
                        break;
                }

                if (Input.GetAxis("Vertical") != 0)
                {
                    if (Time.time > lastWalkClipPlay + walkClipPeriod)
                    {
                        player.audio.PlayOneShot(walkClip[Random.Range(0, walkClip.Length)]);
                        lastWalkClipPlay = Time.time;
                    }
                }

                break;

            case State.GameOver:
                break;

            case State.PostGame:
                ResetGame();
                break;

            case State.Debug:
                break;

            default:
                break;
        }


    }

    private void RunResourceGUI()
    {
        for (int i = 0; i < GuiSprite.Length; i++)
        {
            float percent = (float)resource[i] / 100.0f;
            float h, s, v;
            h = resource[i] / 255;
            v = (255.0f - (255.0f * percent) + 100.0f) / 255;
            s = (255.0f - (255.0f * percent)) / 255;
            int r, g, b;
            Utilities.HsvToRgb(h, s, v, out r, out g, out b);
            GuiSprite[i].color = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, s + 0.3f);
        }
    }

    private void DecayResources()
    {
        for (int i = 0; i < resource.Length; i++)
        {
            if (Time.time > lastResourceDecayTime[i] + decayPeriod[i])
            {
                resource[i] -= 1;
                lastResourceDecayTime[i] = Time.time;
                resource[i] = Mathf.Clamp(resource[i], 0, 100);
            }
            if (resource[i] == 0)
            {
                StartCoroutine(EndGame(5.0f));
            }
        }

    }

    public void AddResource(Resources _resource, int value)
    {
        resource[(int)_resource] += value;
        resource[(int)_resource] = Mathf.Clamp(resource[(int)_resource], 0, 100);
    }

    private IEnumerator EndGame(float delay)
    {
        SwitchGameState(State.GameOver);
        playerSettings.enabled = false;
        player.GetComponentInChildren<HeadBob>().enabled = false;
        MouseLook[] m = player.GetComponentsInChildren<MouseLook>();
        for (int i = 0; i < m.Length; i++)
        {
            m[i].enabled = false;
        }
        //CreateEventMessage("Game Over", Color.gray, Vector3.zero, 0.0f, 300, 0, false);
        text.text = "Dead";
        yield return new WaitForSeconds(delay);
        SwitchGameState(State.PostGame);
    }

    private IEnumerator RegenStamina()
    {
        regenStaminaLock = true;
        yield return new WaitForSeconds(0.1f);
        resource[(int)Resources.Stamina]++;
        resource[(int)Resources.Stamina] = Mathf.Clamp(resource[(int)Resources.Stamina], 0, 100);
        regenStaminaLock = false;
    }

    private void SwitchTimeOfDay(TimeOfDay currentTime)
    {
        switch (currentTime)
        {
            case TimeOfDay.Dawn:
                timeOfDay = TimeOfDay.Morning;
                break;

            case TimeOfDay.Morning:
                timeOfDay = TimeOfDay.Afternoon;

                break;

            case TimeOfDay.Afternoon:
                timeOfDay = TimeOfDay.Dusk;
                break;

            case TimeOfDay.Dusk:
                timeOfDay = TimeOfDay.Night;
                break;

            case TimeOfDay.Night:
                timeOfDay = TimeOfDay.Dawn;
                break;

            default:
                break;
        }
    }
}