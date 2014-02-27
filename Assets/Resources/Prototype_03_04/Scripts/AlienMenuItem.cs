using UnityEngine;

public class AlienMenuItem : MenuItemBase
{
    public enum Action { BuildCity, MakePlanet };

    public Action action;

    protected override void Activate()
    {
        switch (action)
        {
            case Action.BuildCity:
                AlienGameManager.Instance.SpawnGameObject(Resources.Load<GameObject>("Prototype_03/Prefabs/City"));
                AlienGameManager.Instance.CreateEventMessage("Populous!", Color.green);
                AlienAudioManager.Instance.PlayClipOneShot((int)AlienAudioManager.clips.pop);
                break;
            case Action.MakePlanet:
                AlienGameManager.Instance.SpawnGameObject(Resources.Load<GameObject>("Prototype_03/Prefabs/Earth"));
                AlienGameManager.Instance.CreateEventMessage("Planet get!", Color.green);
                AlienAudioManager.Instance.PlayClipOneShot((int)AlienAudioManager.clips.pop);
                break;
            default:
                break;
        }
    }

    protected override void DisabledEvent()
    {
        AlienGameManager.Instance.CreateEventMessage("Disabled", Color.red);
    }
}