using UnityEngine;

public class PGGameManager : MonoBehaviour
{
    public World world;
    public Person choiceA;
    public Person choiceB;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10.0f, 10.0f, 100.0f, 20.0f), "Start"))
        {
            RefreshChoices();
        }
        int gap = 50;
        float buttonHeight = 100.0f;
        if (choiceA != null)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - gap, Screen.height / 2, 100.0f, buttonHeight), choiceA.name))
            {
                MakeChoice(choiceA);
            }
        }
        if (choiceB != null)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + gap, Screen.height / 2, 100.0f, buttonHeight), choiceB.name))
            {
                MakeChoice(choiceB);
            }
        }
    }

    private Person PickRandomPerson(World world)
    {
        return world.people[Random.Range(0, world.people.Count)];
    }

    private void MakeChoice(Person choice)
    {
        choice.Kill();
        RefreshChoices();
    }

    private void RefreshChoices()
    {
        choiceA = null;
        choiceB = null;
        choiceA = PickRandomPerson(world);
        choiceB = PickRandomPerson(world);
    }
}