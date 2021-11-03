using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationClass : MonoBehaviour
{
    // All classes inherited from this class can be activated from other scripts
    // This is called Polymorphism
    // The derived class should override the protected functions

    public bool inverted;

    // If someone has better names for the functions, rename it.
    public void Activate()
    {
        if (inverted) Deact();
        else Act();
    }
    protected virtual void Act() { }

    public void Deactivate()
    {
        if (inverted) Act();
        else Deact();
    }
    protected virtual void Deact() { }
}
