using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHookThresh : MonoBehaviour
{
    [SerializeField]
    private GameObject hook;
    [SerializeField]
    private float hookDistance;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject hookPosition;

    private HookDetectionThresh hookDetection;
    private Sequence hookSequence;
    private GameObject hookInsta;

    [SerializeField]
    private float distanceAway;

    public bool hasThrown { get; set; }
    private bool isHooked;

    private void Start()
    {
        hookDetection = gameObject.GetComponentInChildren<HookDetectionThresh>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!hasThrown)
            {
                hasThrown = true;
                animator.SetBool("HasThrown", hasThrown);
            }
            else if(isHooked)
            {
                TugHook();
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
            float distanceBetween = Vector3.Distance(hook.transform.position, transform.position);

            hook.transform.DOMoveZ(distanceBetween -= distanceAway, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => 
            {
                isHooked = true;
            });
        }
    }

    private void TugHook()
    {
        float distanceBetween = Vector3.Distance(hook.transform.position, transform.position);
        hook.transform.DOMoveZ(distanceBetween -= distanceAway, 0.5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            transform.DOMoveZ(hook.transform.position.z - 1.0f, 0.5f).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                hasThrown = false;
                hookDetection.HookRelease(true);
                hook.transform.parent = hookPosition.transform;
                hook.transform.DOMove(hookPosition.transform.position, 0.1f).SetEase(Ease.OutSine);
            });
        });
    }

    private void ThrowHookAnim()
    {
        ThrowHook(hasThrown);
        hook.transform.parent = null;
        animator.SetBool("HasThrown", false);
    }

}
