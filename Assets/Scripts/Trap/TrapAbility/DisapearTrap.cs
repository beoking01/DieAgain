using UnityEngine;

public class DisappearTrap : TrapBase
{
    protected override void Activate()
    {
        gameObject.SetActive(false);
    }
}