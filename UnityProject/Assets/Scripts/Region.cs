using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    private void Update()
    {
        // Set in update to allow for Start in other scripts
        this.gameObject.SetActive(false);
    }

    public enum Axis
    {
        y, z
    }

    public Axis positionAxis = Axis.y;

    public Vector2 PositionMin
    {
        get
        {
            if (positionAxis == Axis.y)
            {
                return transform.position - transform.localScale / 2;
            }
            else
            {
                return new Vector2(transform.position.x, transform.position.z) - new Vector2(transform.localScale.x, transform.localScale.z) / 2;
            }
        }
    }
    public Vector2 Size => transform.localScale;

    public Vector2 PositionMax
    {
        get
        {
            if (positionAxis == Axis.y)
            {
                return transform.position + transform.localScale / 2;
            }
            else
            {
                return new Vector2(transform.position.x, transform.position.z) + new Vector2(transform.localScale.x, transform.localScale.z) / 2;
            }
        }
    }

    public Vector2 Range
    {
        get
        {
            if (positionAxis == Axis.y)
            {
                return transform.localScale;
            }
            else
            {
                return new Vector2(transform.localScale.x, transform.localScale.z);
            }
        }
    }
}