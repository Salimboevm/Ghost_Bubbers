using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : WeaponTypes
{
    private bool _isGhostTrapped;
    private float _secondsOfVacuumTime;

    protected override void VacuumGhost()
    {
        _secondsOfVacuumTime -= 1f * Time.deltaTime;
        base.VacuumGhost();
    }
}
