using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaystoneController : MonoBehaviour
{

    public BoxCollider2D triggerCollider;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // when player enters waystone activation radius
            GameManager.instance.setIsUIopen(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // when player enters waystone activation radius
            GameManager.instance.setIsUIopen(false);
        }
    }

    private void OnDestroy()
    {
        
    }
}
