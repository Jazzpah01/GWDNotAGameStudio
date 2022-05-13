using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    private void Update()
    {
        // Set in update to allow for Start in other scripts
        if (!Debugger.Debug)
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
                return new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            }
            else
            {
                return new Vector2(transform.localScale.x, transform.localScale.z);
            }
        }
    }

    public static bool Collision(Region r1, Region r2)
    {
        if (r1.positionAxis != r2.positionAxis)
            return false;

        if (r1.PositionMin.x < r2.PositionMin.x + r2.Size.x &&
            r1.PositionMin.x + r1.Size.x > r2.PositionMin.x &&
            r1.PositionMin.y < r2.PositionMin.y + r2.Size.y &&
            r1.PositionMin.y + r1.Size.y > r2.PositionMin.y)
        {
            return true;
        }

        return false;
    }
}