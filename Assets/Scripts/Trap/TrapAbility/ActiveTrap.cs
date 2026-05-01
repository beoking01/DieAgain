using UnityEngine;

public class ActiveTrap : TrapBase
{
    protected override void Activate()
    {
        gameObject.SetActive(true);
    }
}