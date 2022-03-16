using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CloudController : MonoBehaviour
{
    public float speed; // flip negative if other direction
    public float distance;
    public float duration;

    public AnimationCurve aCurve;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init(); // add settings as param if needed - empty is default
        transform.DOMoveX(transform.position.x + distance, duration).OnComplete(EndCloud).SetEase(aCurve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndCloud()
    {
        LevelManager.instance.clouds_active--;
        Destroy(this.gameObject);
    }
}
