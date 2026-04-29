using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public float delay = 0.2f;
    protected bool activated = false;

    public void TriggerTrap()
    {
        if (activated) return;

        activated = true;
        Invoke(nameof(Activate), delay);
    }

    protected abstract void Activate();
}