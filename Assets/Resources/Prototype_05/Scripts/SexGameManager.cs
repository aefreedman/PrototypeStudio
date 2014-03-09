using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SexGameManager : GameManagerBase
{

    public TextInput textInput;
    public string responseFileName;
    public List<string> inputTrigger;
    public List<string> response;

    protected override void Start()
    {
        base.Start();
        Physics.gravity = new Vector3(0, 2.0f, 0);
        //List<List<string>> responseList;
        //responseList = CSVReader.ReadFile(responseFileName);
        // TODO : Best implemented as key value pairings?
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string s = textInput.EnterInput();
            CreateEventMessage(s, Color.white, 0);
            StartCoroutine(GenerateTextResponse(s));
        }
    }

    private IEnumerator GenerateTextResponse(string input)
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
}