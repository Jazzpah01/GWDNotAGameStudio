using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractant{
    void Interact();
    bool InRange { get; set; }
}