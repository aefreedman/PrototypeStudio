using UnityEngine;
using System.Collections.Generic;

public class P6GameManager : GameManagerBase
{

    public P6Square topRight;
    public P6Square topLeft;
    public P6Square bottomRight;
    public P6Square bottomLeft;
    private List<P6Square> pad;
    public int points;
    public GamepadInfo gamepad;
    public GamepadInfoHandler gamepadInfoHandler;

    protected override void Start()
    {
        base.Start();
        pad = new List<P6Square>();
        pad.Add(topLeft);
        pad.Add(topRight);
        pad.Add(bottomLeft);
        pad.Add(bottomRight);
        Physics.gravity = Vector3.zero;
        gamepad = GamepadInfoHandler.Instance.AttachControllerToPlayer(gameObject);
    }

    protected override void FixedUpdate()
    {
        if (gamepad != null)
        {
            if (gamepad.buttonDown[0])
            {
                int p = topLeft.Press();
                points += p;
            }
            if (gamepad.buttonDown[0])
            {
                int p = bottomLeft.Press();
                points += p;
            }
            if (gamepad.buttonDown[0])
            {
                int p = topRight.Press();
                points += p;
            }
            if (gamepad.buttonDown[0])
            {
                int p = bottomRight.Press();
                points += p;
            }
        }

    }

    public void PulseAll(Color color, float scale)
    {
        for (int i = 0; i < pad.Count; i++)
        {
            pad[i].GetComponentInChildren<TweenPulse>().Pulse(color, scale);
        }
    }
}