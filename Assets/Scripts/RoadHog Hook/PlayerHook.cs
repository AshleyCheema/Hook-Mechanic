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
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject hookPosition;

    private HookDetection hookDetection;
    private Sequence hookSequence;
    private GameObject hookInsta;

    private float distanceAway = 1.5f;

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
                animator.SetBool("HasThrown", hasThrown);
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
            //hookInsta = Instantiate(hook, new Vector3(transform.position.x, 1, transform.position.z), hook.transform.rotation);
            hookSequence = DOTween.Sequence();
            hookSequence.Append(hook.transform.DOMoveZ(hookDistance, 1.5f).SetEase(Ease.OutSine).OnKill( () => ThrowHook(false)).OnComplete(() => ThrowHook(false)));
        }
        else
        {
            hook.transform.DOMoveZ(transform.position.z + distanceAway, 1.5f).SetEase(Ease.OutSine).OnComplete(() => 
            {
                hasThrown = false;
                hookDetection.HookRelease(true);
                hook.transform.parent = hookPosition.transform;
                hook.transform.DOMove(hookPosition.transform.position, 0.1f).SetEase(Ease.OutSine);
            });
        }
    }

    private void ThrowHookAnim()
    {
        ThrowHook(hasThrown);
        hook.transform.parent = null;
        animator.SetBool("HasThrown", false);
    }

}
