using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    //1 for up, -1 for down
    public int LeverInput;
    public bool maxHeight;

    //Lever -> Lift Gate
    public void GateLever(GameObject Gate)
    {
        if ((LeverInput == 1) && !maxHeight)
        {

        }
    }
}
