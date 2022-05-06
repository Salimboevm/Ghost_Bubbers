using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDirectionCube : MonoBehaviour
{
    [SerializeField] private AIGhost _ghost;

    // Update is called once per frame
    void Update()
    {
        if (_ghost != null && _ghost.isActiveAndEnabled && _ghost.GetAgent().destination != null)
            transform.position = _ghost.GetAgent().destination;
    }

    public void SetGhost(AIGhost ghost) { _ghost = ghost; }
}
