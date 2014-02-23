using UnityEngine;
using System.Collections;

public class AlienGameManager : GameManagerBase
{

    protected override void Start()
    {
        base.Start();
        Physics.gravity = new Vector3(0, -3.0f, 0);
        AlienMenu.Instance.DisableAllMenuChoices();
        if (startDebug)
        {
            AlienMenu.Instance.SetToStarConditions();
        }
        else
        {
            StartCoroutine(StartInstructions());
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator StartInstructions()
    {
        int size = 200;
        float life = 5.0f;
        AlienGameManager.Instance.CreateEventMessage("Welcome!", Color.white, life, 2.0f, size*2);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("This is Humanity Training Simulator 23052.a_3c", Color.white, life, 2.0f, size);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("it sumulates Humanity as it existed on Earth", Color.white, life, 2.0f, size);
        yield return new WaitForSeconds(3);
        AlienGameManager.Instance.CreateEventMessage("in what Humans called the 21st Century", Color.white, life, 2.0f, size);
        AlienMenu.Instance.SetToStarConditions();
    }
}