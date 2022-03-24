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

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        DOTween.Init(); // add settings as param if needed - empty is default
        transform.DOMoveX(transform.position.x + distance, duration).OnComplete(EndCloud).SetEase(aCurve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndCloud()
    {
        EnvCamController.instance.clouds_active--;
        Destroy(this.gameObject);
    }
}
