using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetection : MonoBehaviour
{
    [SerializeField]
    private PlayerHook playerHook;

    private GameObject hookedEnemy;

    private void OnTriggerEnter(Collider other)
    { 
        if (playerHook.hasThrown)
        {
            if (other.gameObject.tag == "Enemy")
            {
                hookedEnemy = other.gameObject;
                hookedEnemy.transform.parent = this.gameObject.transform;
            }
        }
        else
        {
            if(hookedEnemy != null)
            {
                hookedEnemy.transform.parent = null;
                hookedEnemy = null;
            }
        }
    }
}
