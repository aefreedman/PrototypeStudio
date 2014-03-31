using System.Collections;
using UnityEngine;

/// <summary>
/// Base controller for the automatic dialogue system. Uses DialogueUnits.
/// </summary>
public class DialogueController : MonoBehaviour
{
    public enum DialogueState { paused, running, endOfTree }
    public DialogueState state;
    public DialogueUnit currentDialogueUnit;
    public DialogueUnit nextDialogueUnit;
    public TextInput textInput;
    public bool allowTextInput;
    public bool continueToNextDialogueUnit;
    protected static DialogueController instance;

    public static DialogueController Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<DialogueController>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    
    protected void Start()
    {
        state = DialogueState.paused;
    }

    protected void Update()
    {
        if (nextDialogueUnit == null)
        {
            allowTextInput = true;
        }

        if (allowTextInput)
        {
            if (!textInput.gameObject.activeInHierarchy)
            {
                textInput.gameObject.SetActive(true);
            }
            else
            {
                if (Input.GetButtonDown("Input Text"))
                {
                    string s = textInput.EnterInput();
                    GameObject o = GameManagerBase.Instance.CreateEventMessage(s, Color.white, 1);
                    o.transform.Rotate(Vector3.right * 45);
                    o.GetComponent<TextMesh>().offsetZ = 0.5f;
                    o.GetComponent<SpringJoint>().damper = 0.85f;
                    allowTextInput = false;
                }
            }
        }
        else // if not allowInput
        {
            if (textInput.gameObject.activeInHierarchy)
            {
                textInput.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Internal method to send strings to the game manager event text system.
    /// </summary>
    /// <param name="input">Dialogue Unit in use</param>
    /// <param name="text">string to use</param>
    /// <param name="delay">delay before message is created</param>
    /// <param name="index">index in string array if using multifiring DialogueUnit</param>
    /// <returns></returns>
    protected internal IEnumerator GenerateText(DialogueUnit input, string text, float delay, int index = 0)
    {
        Debug.Log("[DialogueController] sending current dialogue in " + delay.ToString("0.00") + " seconds");
        yield return new WaitForSeconds(delay);
        Debug.Log("[DialogueController] displaying: \"" + text + "\" for " + input.displayTime[index].ToString("0.00") + " seconds.");
        GameObject o = GameManagerBase.Instance.CreateEventMessage(input.text[index], input.color, input.displacement, input.displayTime[index], input.size, input.targetAnchorNumber[index], false);
        o.transform.Rotate(Vector3.right * 45);
        o.GetComponent<TextMesh>().offsetZ = input.displacement.z;
        o.GetComponent<SpringJoint>().damper = 0.85f;
    }

    protected internal IEnumerator RunText(DialogueUnit input)
    {
        if (input.seen)
        {
            StopCoroutine("RunText");
        }
        else
        {
            Debug.Log("[DialogueController] starting new DialogueUnit in " + input.delay[0].ToString("0.00") + " seconds");
            yield return new WaitForSeconds(input.delay[0]);
            state = DialogueState.running;
            currentDialogueUnit = input;
            string s = "";
            float delay = 0.0f;
            if (input.displayStyle == DialogueUnit.DisplayStyle.multiline)
            {
                for (int i = 0; i < input.text.Length; i++)
                {
                    s = s + input.text[i] + System.Environment.NewLine;
                }
                StartCoroutine(GenerateText(input, s, 0));
            }
            else if (input.displayStyle == DialogueUnit.DisplayStyle.singleline)
            {
                for (int i = 0; i < input.text.Length; i++)
                {
                    s = input.text[i];
                    delay += input.delay[i + 1];
                    StartCoroutine(GenerateText(input, s, input.delay[i + 1], i));
                    Debug.Log("[DialogueController] sending next line in " + (input.displayTime[i] + input.delay[i + 1]).ToString("0.00") + " seconds");
                    yield return new WaitForSeconds(input.displayTime[i] + input.delay[i + 1]);
                }
            }

            if (input.isEnd)
            {
                DoEndOfDialogueTree();
            }
            else
            {
                nextDialogueUnit = input.nextDialogueUnit;
                input.seen = true;
                continueToNextDialogueUnit = true;
                if (input.promptAfter)
                {
                    Debug.Log("[DialogueController] reached end of DialogueUnit and prompting...");
                    allowTextInput = true;
                    state = DialogueState.paused;
                }
                else
                {
                    if (nextDialogueUnit != null)
                    {
                        Debug.Log("[DialogueController] reached end of DialogueUnit and continuing automatically.");
                        StartCoroutine(QueueDialogue(delay + nextDialogueUnit.delay[0], nextDialogueUnit));
                    }
                }
            }
        }
    }


    protected internal IEnumerator QueueDialogue(float delay, DialogueUnit _unit)
    {
        Debug.Log("[DialogueController] starting next DialogueUnit in " + delay.ToString("0.00") + " seconds");
        yield return new WaitForSeconds(delay);
        StartCoroutine(RunText(_unit));
    }

    /// <summary>
    /// Call this method to trigger the next DialogueUnit in the DialogueController.
    /// </summary>
    /// <returns>True if DialogueUnit has been triggered</returns>
    protected bool TriggerDialogue()
    {
        if (continueToNextDialogueUnit)
        {
            if (nextDialogueUnit != null)
            {
                StartCoroutine(RunText(nextDialogueUnit));
                continueToNextDialogueUnit = false;
                return true;
            }
            else
            {
                Debug.LogWarning("[DialogueController] attempted to run DialogueUnit but nextDialogueUnit is null.");
                return false;
            }

        }
        else
        {
            Debug.Log("[DialogueController] received trigger but is blocked.");
            return false;
        }
    }

    protected virtual void DoEndOfDialogueTree()
    {
        Debug.Log("[DialogueController] has reached the end of its current dialogue tree.");
        state = DialogueState.endOfTree;
    }
}