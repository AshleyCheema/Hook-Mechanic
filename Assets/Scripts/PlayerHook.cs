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

    private HookDetection hookDetection;
    private Sequence hookSequence;

    public bool hasThrown { get; set; }

    private void Start()
    {
        hookDetection = gameObject.GetComponentInChildren<HookDetection>();
    }

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

        if(hookDetection.enemyHit)
        {
            hookSequence.Kill();
        }
    }

    private void ThrowHook(bool onHit)
    {
        if (onHit)
        {
            hookSequence = DOTween.Sequence();
            hookSequence.Append(hook.transform.DOMoveZ(hookDistance, 1.5f).SetEase(Ease.OutSine).OnKill( () => ThrowHook(false)).OnComplete(() => ThrowHook(false)));
        }
        else
        {
            hook.transform.DOMoveZ(transform.position.z + 0.5f, 1.5f).SetEase(Ease.OutSine).OnComplete(() => 
            {
                hasThrown = false;
                hookDetection.HookRelease(true);
            });
        }
    }
}
