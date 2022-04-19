using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesToFindPossessedItems : WeaponTypes
{
    //is player wearing glasses 
    private bool _isPlayerWearingGlasses;
    public PossessableObject[] _possesedoBJECTS;
    private void Start()
    {
        InputFromPlayer.Instance.GetUseGlassesButtonPressed(RayGlassesToFindGhost);
    }
    protected override void RayGlassesToFindGhost()
    {
        _isPlayerWearingGlasses = true;
        ChangeColorOfPosessedObjects(_possesedoBJECTS);
    }
    private void ChangeColorOfPosessedObjects(PossessableObject[] possessedObjects)
    {
        for (int i = 0; i < possessedObjects.Length; i++)
        {
            possessedObjects[i]._object.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
