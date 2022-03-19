using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDirectionCube : MonoBehaviour
{
    public AIGhost _ghost;

    // Update is called once per frame
    void Update()
    {
        if(_ghost.GetAgent().destination != null)
            transform.position = _ghost.GetAgent().destination;
    }
}
