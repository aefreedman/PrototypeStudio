using UnityEngine;
using System.Collections;

public class DialogueUnit : MonoBehaviour
{
    public enum DisplayStyle { multiline, singleline };
    public DisplayStyle displayStyle;
    
    public bool seen;
    public string[] text;
    public float[] delay;
    public bool promptAfter;
    public Color color;
    public float displayTime;
    public int size;
    public Vector3 displacement;
    public int targetAnchorNumber;
    public DialogueUnit nextDialogueUnit;
}
