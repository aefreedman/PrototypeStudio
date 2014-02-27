using UnityEngine;

public class TextInput : MonoBehaviour
{
    public string stringToEdit = "";
    public bool ignoreCase;

    private void OnGUI()
    {
        int width = 200;
        int height = 20;
        if (ignoreCase)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2 - width / 2, Screen.height - 10 - height / 2, width, height), stringToEdit.ToLower(), 25);
        }
        else
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2 - width / 2, Screen.height - 10 - height / 2, width, height), stringToEdit, 25);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            
        }
    }

    public string EnterInput()
    {
        string s = stringToEdit;
        stringToEdit = "";
        return s;
    }
}