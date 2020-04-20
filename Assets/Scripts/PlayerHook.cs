using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHook : MonoBehaviour
{
    [SerializeField]
    private GameObject hook;
    [SerializeField]
    private float hookDistance;

    public bool hasThrown { get; set; }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!hasThrown)
            {
                hasThrown = true;
                ThrowHook(hasThrown);
            }
        }
    }

    private void ThrowHook(bool onHit)
    {
        if (onHit)
        {
            hook.transform.DOMoveZ(hookDistance, 1.5f).SetEase(Ease.OutSine).OnComplete(() => ThrowHook(false));
        }
        else
        {
            hook.transform.DOMoveZ(transform.position.z + 0.5f, 1.5f).SetEase(Ease.OutSine).OnComplete(() => hasThrown = false);
        }
    }
}
