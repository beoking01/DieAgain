using System;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public TrapBase[] traps;
    public bool oneTimeOnly = true;
    [SerializeField] string trapTag = "Player";


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(trapTag))
        {
            Debug.Log("Trap triggered!");

            foreach (TrapBase trap in traps)
            {
                trap.TriggerTrap();
            }
        }
    }
}