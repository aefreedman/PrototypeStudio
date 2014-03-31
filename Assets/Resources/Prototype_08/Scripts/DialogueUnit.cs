using UnityEngine;
using System.Collections;

public class DialogueUnit : MonoBehaviour
{
    public enum DisplayStyle { multiline, singleline };
    public DisplayStyle displayStyle;
    
    public bool seen;
    public bool promptAfter;
    public bool isEnd;
    public Color color;
    public int size;
    public Vector3 displacement;
    public DialogueUnit nextDialogueUnit;
    public string[] text;
    public float[] delay;
    public float[] displayTime;
    public int[] targetAnchorNumber;
}
