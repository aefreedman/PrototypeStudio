using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class polls the connected gamepads and populates their GamepadInfo with
/// the values of all their joysticks and buttons.
///
/// Essentially just mirrors the behavior of the InputManager, with the benefit of
/// the data for an entire gamepad being an object that you can reference by
/// gamepad rather than individual axis/button names.
/// </summary>
public class GamepadInfoHandler : MonoBehaviour
{

    private int numberOfConnectedControllers;

    public GameObject gamepadInfoPrefab;
    public Dictionary<GamepadInfo, GameObject> gamepadPlayerDictionary;
    public GamepadInfo[] gamepads;
    public int managerCount = 1;
    //private bool acceptingInput;
    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return IsReady;
        }
    }

    private static GamepadInfoHandler instance;

    public static GamepadInfoHandler Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get
        {
            if (instance == null)
            {
                GameObject gamepadManagerObject = (GameObject.FindGameObjectWithTag("GamepadManager"));
                if (gamepadManagerObject != null)
                {
                    instance = gamepadManagerObject.GetComponent<GamepadInfoHandler>();
                    instance.managerCount = (GameObject.FindGameObjectsWithTag("GamepadManager")).Length;

                    if (instance != null)
                    {
                        Debug.Log("[GamepadHandler] Instance Count : " + instance.managerCount.ToString());
                        return instance;
                    }
                    else
                    {
                        instance = new GameObject("Gamepad Handler").AddComponent<GamepadInfoHandler>();
                        instance.managerCount += 1;
                        Debug.Log("[GamepadHandler] Instance Count : " + instance.managerCount.ToString());
                        return instance;
                    }
                }
            }

            Debug.Log("[GamepadHandler] Instance Count : " + instance.managerCount.ToString());
            return instance;
        }
    }

    public int getNumberOfConnectedControllers()
    {
        return numberOfConnectedControllers;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //acceptingInput = true;
        numberOfConnectedControllers = Input.GetJoystickNames().Length;

        gamepadPlayerDictionary = new Dictionary<GamepadInfo, GameObject>();
        gamepads = new GamepadInfo[numberOfConnectedControllers];

        Debug.Log("[GamepadHandler] Found " + numberOfConnectedControllers.ToString() + " Controllers.");

        for (int i = 0; i < numberOfConnectedControllers; i++)
        {
            GameObject gamepadInfo;
            gamepadInfo = Instantiate(gamepadInfoPrefab) as GameObject;
            gamepadPlayerDictionary.Add(gamepadInfo.GetComponent<GamepadInfo>(), null);
            gamepads[i] = gamepadInfo.GetComponent<GamepadInfo>();
        }
    }

    public GamepadInfo GetFreeController()
    {
        GamepadInfo freeGamepad = null;
        //do we have an entry where the value is null? (open gamepad)
        //get the key where the value is null
        if (gamepadPlayerDictionary != null)
        {
            foreach (KeyValuePair<GamepadInfo, GameObject> pair in gamepadPlayerDictionary)
            {
                if (pair.Key is GamepadInfo && pair.Value == null)
                {
                    freeGamepad = (GamepadInfo)pair.Key;
                }
            }
            return freeGamepad;
        }
        else
        {
            Debug.LogError("[GamepadHandler] Dictionary null!");
            return null;
        }
    }

    /// <summary>
    /// Will attach a gamepad to a GameObject
    /// </summary>
    /// <param name="unattachedPlayer"></param>
    public GamepadInfo AttachControllerToPlayer(GameObject unattachedPlayer)
    {
        Debug.Log("[GamepadHandler] Getting free gamepad...");
        GamepadInfo freeGamepad = GetFreeController();
        Debug.Log("[GamepadHandler] Success. Controllers available.");
        if (freeGamepad == null)
        {
            Debug.LogError("[GamepadHandler] Error: Can't find a free controller!" + unattachedPlayer.gameObject.name);
            return null;
        }
        else
        {
            Debug.Log("[GamepadHandler] Attaching...");
            gamepadPlayerDictionary[freeGamepad] = unattachedPlayer;
            return freeGamepad;
        }
    }

    private void Update()
    {
        //if (acceptingInput)
        //{
            GetJoystickData();
        //}
    }

    private void GetJoystickData()
    {
        for (int i = 1; i < numberOfConnectedControllers + 1; i++)
        {
            string leftX = "L_XAxis_" + i.ToString();
            string leftY = "L_YAxis_" + i.ToString();
            string rightX = "R_XAxis_" + i.ToString();
            string rightY = "R_YAxis_" + i.ToString();
            string triggerLAxis = "TriggersL_" + i.ToString();
            string triggerRAxis = "TriggersR_" + i.ToString();
            string dpadX = "DPad_XAxis_" + i.ToString();
            string dpadY = "DPad_YAxis_" + i.ToString();
            string a = "A_" + i.ToString();
            string b = "B_" + i.ToString();
            string x = "X_" + i.ToString();
            string y = "Y_" + i.ToString();
            string LB = "LB_" + i.ToString();
            string RB = "RB_" + i.ToString();
            string back = "Back_" + i.ToString();
            string start = "Start_" + i.ToString();
            string LS = "LS_" + i.ToString();
            string RS = "RS_" + i.ToString();

            bool[] button = new bool[10];
            button[0] = Input.GetButton(a);
            button[1] = Input.GetButton(b);
            button[2] = Input.GetButton(x);
            button[3] = Input.GetButton(y);
            button[4] = Input.GetButton(LS);
            button[5] = Input.GetButton(RS);
            button[6] = Input.GetButton(back);
            button[7] = Input.GetButton(start);
            button[8] = Input.GetButton(LB);
            button[9] = Input.GetButton(RB);

            bool[] buttonUp = new bool[10];
            buttonUp[0] = Input.GetButtonUp(a);
            buttonUp[1] = Input.GetButtonUp(b);
            buttonUp[2] = Input.GetButtonUp(x);
            buttonUp[3] = Input.GetButtonUp(y);
            buttonUp[4] = Input.GetButtonUp(LS);
            buttonUp[5] = Input.GetButtonUp(RS);
            buttonUp[6] = Input.GetButtonUp(back);
            buttonUp[7] = Input.GetButtonUp(start);
            buttonUp[8] = Input.GetButtonUp(LB);
            buttonUp[9] = Input.GetButtonUp(RB);

            bool[] buttonDown = new bool[10];
            buttonDown[0] = Input.GetButtonDown(a);
            buttonDown[1] = Input.GetButtonDown(b);
            buttonDown[2] = Input.GetButtonDown(x);
            buttonDown[3] = Input.GetButtonDown(y);
            buttonDown[4] = Input.GetButtonDown(LS);
            buttonDown[5] = Input.GetButtonDown(RS);
            buttonDown[6] = Input.GetButtonDown(back);
            buttonDown[7] = Input.GetButtonDown(start);
            buttonDown[8] = Input.GetButtonDown(LB);
            buttonDown[9] = Input.GetButtonDown(RB);

            gamepads[i - 1].SetData(i, new Vector2(Input.GetAxis(leftX), Input.GetAxis(leftY)), new Vector2(Input.GetAxis(rightX), Input.GetAxis(rightY)), Input.GetAxis(triggerLAxis), Input.GetAxis(triggerRAxis), new Vector2(Input.GetAxis(dpadX), Input.GetAxis(dpadY)), button, buttonDown, buttonUp);
        }
    }

    //public void Disable()
    //{
    //    acceptingInput = false;
    //}

    //public void Enable()
    //{
    //    acceptingInput = true;
    //}
}