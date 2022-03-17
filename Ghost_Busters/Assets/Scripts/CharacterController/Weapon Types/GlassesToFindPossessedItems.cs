using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesToFindPossessedItems : WeaponTypes
{
    //is player wearing glasses 
    private bool _isPlayerWearingGlasses;

    private void Start()
    {
        InputFromPlayer.Instance.GetUseGlassesButtonPressed(RayGlassesToFindGhost);
    }
    protected override void RayGlassesToFindGhost()
    {
        _isPlayerWearingGlasses = true;
    }
    private void ShowPossessedItems(bool value)
    {
        value = true;
        //change material color
        
    }
}
