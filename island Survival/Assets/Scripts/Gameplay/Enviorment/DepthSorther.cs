using System;
using DG.Tweening;
using MyBox;
using UnityEngine;
using Tmn;

public class DepthSorther : MonoBehaviour
{
    [SerializeField, Tag]
    private String fadeObjectTag;

    [SerializeField]
    private float fadedAlpha;

    [Header("Tween Value")]
    [SerializeField]
    private float fadeInDuration;
    
    [SerializeField]
    private float fadeOutDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(fadeObjectTag))
        {
            FadeableObject obj = other.GetComponent<FadeableObject>();
            
            obj.FadeObject.DOFade(fadedAlpha, fadeInDuration);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(fadeObjectTag))
        {
            FadeableObject obj = other.GetComponent<FadeableObject>();

            obj.FadeObject.DOFade(1f, fadeOutDuration);
        }
    }

    #region !Obsolote

    // private void Update()
    // {
    //     FadeableObject obj = null;
    //     RaycastHit2D hit;
    //     hit = Physics2D.Raycast(transform.position, Vector3.forward, 10f, fadeObjectMask);
    //     
    //     Debug.DrawRay(transform.position, Vector3.forward * 10f, Color.red);
    //
    //     
    //     if (hit )
    //     { 
    //         obj = hit.transform.GetComponent<FadeableObject>();
    //         
    //         // obj.FadeObject.FadeIn();
    //         playerRenderer.sortingOrder = obj.FadeObject.sortingOrder - 1;
    //     }
    //
    //     if (_lastFadeableObject != null)
    //     {
    //         // _lastFadeableObject.FadeObject.FadeOut();
    //     }
    //     _lastFadeableObject = obj;
    //
    //     if (!hit)
    //     {
    //         playerRenderer.sortingOrder = 1;
    //     }
    //     
    //     //
    //     // if (!hit.collider)
    //     // {
    //     //     if (_isFaded)
    //     //     {
    //     //         playerRenderer.sortingOrder = 1;
    //     //         _lastSpriteRenderer.FadeOut();
    //     //         _isFaded = false;
    //     //         _lastSpriteRenderer = null;
    //     //     }
    //     //     return;
    //     // }
    //     //
    //     // if(!hit) return;
    //     //
    //     // _lastSpriteRenderer = hit.transform.GetChild(0).GetComponent<SpriteRenderer>();  
    //     //
    //     // playerRenderer.sortingOrder = -1;
    //     //
    //     // _isFaded = true;
    //     //
    //     // if (hit.collider.tag != "FadeableEnviorment") return;
    //     // FadeRenderer.FadeIn(_lastSpriteRenderer);
    //
    // }

    #endregion
}