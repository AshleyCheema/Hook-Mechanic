using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetection : PlayerHook
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            hookHit = true;
        }
    }
}
