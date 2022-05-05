using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesToFindPossessedItems : WeaponTypes
{
    //is player wearing glasses 
    private bool _isPlayerWearingGlasses;
    [SerializeField]
    AI_SharedInfo _possesedObjects;
    private void Start()
    {
        InputFromPlayer.Instance.GetUseGlassesButtonPressed(RayGlassesToFindGhost);
    }
    protected override void RayGlassesToFindGhost()
    {
        _isPlayerWearingGlasses = true;
        ChangeColorOfPosessedObjects();
    }
    private void ChangeColorOfPosessedObjects()
    {
        for (int i = 0; i < _possesedObjects.GetPossessedObjects().Count; i++)
        {
            _possesedObjects.GetPossessedObjects()[i]._object.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        print("rendered");
    }
}
