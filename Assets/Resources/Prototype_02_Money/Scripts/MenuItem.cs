using UnityEngine;
using MMConstants;

public class MenuItem : MonoBehaviour
{
    public Item itemToSpawn;
    private TextMesh textMesh;
    private bool disable;
    public bool Disable
    {
        set
        {
            disable = value;
        }
        get
        {
            return disable;
        }
    }

    MMGameManager gm;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        gm = (MMGameManager)GameManagerBase.Instance;
    }

    void OnMouseOver()
    {
        textMesh.color = Color.yellow;
        if (Input.GetMouseButtonDown(0))
        {
            if (!disable)
            {
                gm.SpawnItem(itemToSpawn);

                switch (itemToSpawn)
                {
                    case Item.Recepticle:
                        break;
                    case Item.MoneyMaker:
                        Menu.Instance.EnableAllMenuChoices();
                        disable = true;
                        break;
                    case Item.ShuttleSpeed:
                        break;
                    case Item.Premium:
                        break;
                    case Item.GiveUp:
                        break;
                    case Item.Quit:
                        Application.Quit();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MMGameManager.Instance.CreateEventMessage("Disabled!", Color.red, 2.0f);
            }
            


        }
    }

    void OnMouseExit()
    {
        textMesh.color = Color.white;
    }


    private void Update()
    {
        if (disable)
        {
            textMesh.color = Color.red;
        }
        else
        {
            textMesh.color = Color.white;
        }
    }
}