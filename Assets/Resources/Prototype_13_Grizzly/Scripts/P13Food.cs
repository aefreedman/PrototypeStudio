using UnityEngine;
using System.Collections;

public class P13Food : P13Item
{
    protected override void Start()
    {
        base.Start();
        property = P13GameManager.Resources.Hunger;
        value = 5;
    }

    private void OnDrawGizmos()
    { }
}
