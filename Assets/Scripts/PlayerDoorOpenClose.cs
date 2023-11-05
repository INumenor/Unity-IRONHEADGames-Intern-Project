using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorOpenClose : MonoBehaviour
{
    [SerializeField] public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("isDoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("isDoorClose");
        }
    }
}
