using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetection : MonoBehaviour
{
    [SerializeField]
    private PlayerHook playerHook;

    private GameObject hookedEnemy;
    public bool enemyHit { get; set; }

    private void OnTriggerEnter(Collider other)
    { 
        if (playerHook.hasThrown)
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemyHit = true;
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

    public void HookRelease(bool animFinished)
    {
        if(animFinished && hookedEnemy != null)
        {
            enemyHit = false;
            hookedEnemy.transform.parent = null;
            hookedEnemy = null;
        }
    }
}
