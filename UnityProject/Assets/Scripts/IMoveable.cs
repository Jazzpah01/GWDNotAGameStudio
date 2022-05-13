using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMoveable: IEventObject
{
    public float MoveableSpeed { get; set; }
    public void MoveTo(Vector2 position, Action callback = null);
}
