using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CloudController : MonoBehaviour
{
    //public float speed; // flip negative if other direction
    public float distance;
    //public float duration;

    public AnimationCurve aCurve;

    private GameObject player;
    private Vector3 origin;
    private float travelled;
    private float speed;
    private float x_offset;
    private bool movesRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
        origin = transform.position;
        travelled = 0f;
        Debug.Log("cloud spawn at: " + origin);

        //DOTween.Init(); // add settings as param if needed - empty is default
        //transform.DOMoveX(transform.position.x + distance, duration).OnComplete(EndCloud).SetEase(aCurve); // prolly wont work with relative positions
    }

    // Update is called once per frame
    void Update()
    {
        /*  // moved to fixedupdate()
        float velocity = speed * Time.deltaTime;
        travelled += velocity;
        origin.x = player.transform.position.x - 10f;        

        transform.position = new Vector3(origin.x + travelled, transform.position.y, transform.position.z);

        if (transform.position.x > player.transform.position.x + 10f)
        {
            EndCloud();
        }
        */
    }

    private void FixedUpdate()
    {      
        float step = speed * Time.fixedDeltaTime;
        travelled += step;
        origin.x = player.transform.position.x + x_offset;

        transform.position = new Vector3(origin.x + travelled, transform.position.y, transform.position.z);

        if (Mathf.Abs(travelled) > distance)
        {
            EndCloud();
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetXOffset(float x)
    {
        this.x_offset = x;
    }

    public void SetMovesRight(bool movesRight)
    {
        this.movesRight = movesRight;
        if (!movesRight) this.speed *= -1;
    }

    void EndCloud()
    {
        EnvCamController.instance.clouds_active--;
        Destroy(this.gameObject);
    }
}
