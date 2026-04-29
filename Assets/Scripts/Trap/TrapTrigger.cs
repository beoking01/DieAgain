using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public TrapBase[] traps;
    public bool oneTimeOnly = true;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Trap triggered!");

            foreach (TrapBase trap in traps)
            {
                trap.TriggerTrap();
            }
        }
    }
}