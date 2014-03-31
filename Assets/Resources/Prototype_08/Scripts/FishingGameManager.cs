using System.Collections;
using UnityEngine;

public class FishingGameManager : GameManagerBase
{
    public TextInput textInput;
    public GameObject target;
    public GameObject player;
    private bool lookAtText;
    private bool allowInput;
    public DialogueUnit[] dialogue;
    public DialogueUnit nextDialogueUnit;
    private bool canNextText;
    private int lookTargetAnchor;
    public Vector3 hsvTarget;
    private Vector3 hsvCurrent;
    public Color colorTarget;
    public float cameraColorLerpSpeed;
    public AudioClip waterDrop;

    protected override void Start()
    {
        base.Start();
        Physics.gravity = new Vector3(0, 0.0f, 0);
        allowInput = false;
        if (nextDialogueUnit == null)
        {
            nextDialogueUnit = dialogue[0];
        }
        SwitchGameState(State.InGame);
        canNextText = true;
        lookTargetAnchor = 0;
        hsvCurrent = new Vector3(194.0f / 255.0f, 0.96f, 0);
        lookAtText = true;
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
                Quaternion playerLookTarget = Quaternion.identity;
                //Camera.main.backgroundColor = UnityEditor.EditorGUIUtility.HSVToRGB(hsvCurrent.x, hsvCurrent.y, hsvCurrent.z);
                //int r, g, b;
                //Utilities.HsvToRgb(hsvCurrent.x, hsvCurrent.y, hsvCurrent.z, out r, out g, out b);
                //Camera.main.backgroundColor = new Color(r / 255, g / 255, b / 255);
                #region scripted stuff for start of game

                if (Input.GetButton("Vertical"))
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        //hsvCurrent = Vector3.Lerp(hsvCurrent, hsvTarget, cameraColorLerpSpeed * Time.deltaTime);
                        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, colorTarget, cameraColorLerpSpeed * Time.deltaTime);
                    }
                }

                #endregion scripted stuff for start of game

                if (nextDialogueUnit == null)
                {
                    allowInput = true;
                }

                if (lookAtText)
                {
                    //player.transform.LookAt(eventTextAnchor[lookTargetAnchor].transform);
                }

                if (allowInput)
                {
                    if (!textInput.gameObject.activeInHierarchy)
                    {
                        textInput.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            string s = textInput.EnterInput();
                            GameObject o = CreateEventMessage(s, Color.white, 1);
                            lookAtText = true;
                            lookTargetAnchor = 1;
                            o.transform.Rotate(Vector3.right * 45);
                            o.GetComponent<TextMesh>().offsetZ = 0.5f;
                            o.GetComponent<SpringJoint>().damper = 0.85f;
                            //StartCoroutine(GenerateTextResponse(s));
                            allowInput = false;
                        }
                        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                        {
                            lookAtText = false;
                        }
                    }
                }
                else // if not allowInput
                {
                    if (textInput.gameObject.activeInHierarchy)
                    {
                        textInput.gameObject.SetActive(false);
                    }

                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (canNextText)
                        {
                            StartCoroutine(RunText(nextDialogueUnit));
                            canNextText = false;
                        }
                    }
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

    private IEnumerator DoTextResponse(string input)
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));

        if (input.Equals("yes", System.StringComparison.CurrentCultureIgnoreCase))
        {
            CreateEventMessage("no", Color.red, 1);
        }
        else
        {
            CreateEventMessage(
                "Why don't you " + System.Environment.NewLine +
                input + System.Environment.NewLine +
                " on your own?",
                Color.red,
                1
                );
        }
    }

    private IEnumerator GenerateText(DialogueUnit input, string text, float delay, int index = 0)
    {
        yield return new WaitForSeconds(delay);
        lookTargetAnchor = input.targetAnchorNumber[index];
        GameObject o = CreateEventMessage(input.text[index], input.color, input.displacement, input.displayTime[index], input.size, input.targetAnchorNumber[index], false);
        o.transform.Rotate(Vector3.right * 45);
        o.GetComponent<TextMesh>().offsetZ = input.displacement.z;
        o.GetComponent<SpringJoint>().damper = 0.85f;
    }

    private IEnumerator RunText(DialogueUnit input)
    {
        if (input.seen)
        {
            StopCoroutine("RunText");
        }
        else
        {
            yield return new WaitForSeconds(input.delay[0]);
            lookAtText = true;
            string s = "";
            float delay = 0.0f;
            if (input.displayStyle == DialogueUnit.DisplayStyle.multiline)
            {
                for (int i = 0; i < input.text.Length; i++)
                {
                    s = s + input.text[i] + System.Environment.NewLine;
                }
                lookTargetAnchor = input.targetAnchorNumber[0];
                StartCoroutine(GenerateText(input, s, 0));
            }
            else if (input.displayStyle == DialogueUnit.DisplayStyle.singleline)
            {
                for (int i = 0; i < input.text.Length; i++)
                {
                    s = input.text[i] + System.Environment.NewLine;
                    delay += input.delay[i + 1];
                    StartCoroutine(GenerateText(input, s, input.delay[i + 1], i));
                    yield return new WaitForSeconds(input.displayTime[i] + input.delay[i + 1]);
                }
            }

            if (input.isEnd)
            {
                audio.Stop();
                audio.PlayOneShot(waterDrop);
                SwitchGameState(State.GameOver);
            }
            else
            {

                nextDialogueUnit = input.nextDialogueUnit;
                input.seen = true;
                canNextText = true;
                if (input.promptAfter)
                {
                    allowInput = true;
                }
                else
                {
                    if (nextDialogueUnit != null)
                    {
                        StartCoroutine(QueueDialogue(delay + nextDialogueUnit.delay[0], nextDialogueUnit));
                    }
                }
            }


        }
    }

    private IEnumerator QueueDialogue(float delay, DialogueUnit _unit)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(RunText(_unit));
    }

    public void TriggerDialogue()
    {
        if (canNextText)
        {
            StartCoroutine(RunText(nextDialogueUnit));
            canNextText = false;
        }
    }
}