using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GlassesToFindPossessedItems : WeaponTypes
{
    //self desc
    private bool _isPlayerWearingGlasses;
    //list of possesed objects 
    [SerializeField]
    AI_SharedInfo _possesedObjects;

    MeshRenderer[] _possesedObjectsColor;

    //dynamic array of colors
    //Should be initialized when player starts to use glasses and deinitialized after done
    Color[] _originalObjectColor; 
    
    private void Start()
    {
        InputFromPlayer.Instance.GetUseGlassesButtonPressed(RayGlassesToFindGhost);//call event
    }
    /// <summary>
    /// call when player uses glasses
    /// activates parent object
    /// </summary>
    protected override void RayGlassesToFindGhost()
    {
        //if there is no possesed objects do not run this code
        if (_possesedObjects.GetPossessedObjects().Count == 0)
            return;
        //if glasses are active do not run this part of the code
        if (!_isPlayerWearingGlasses)
        {
            GetMeshOfPossesedObjects();
            GetOriginalColor();//gets original color of possesed objects 
        }
        _isPlayerWearingGlasses = !_isPlayerWearingGlasses;//activate/deactivate glasses
        ChangeColorOfPosessedObjects();
    }
    void GetMeshOfPossesedObjects()
    {
        _possesedObjectsColor = new MeshRenderer[_possesedObjects.GetPossessedObjects().Count];

        for (int i = 0; i < _possesedObjects.GetPossessedObjects().Count; i++)
        {
            _possesedObjectsColor[i] = _possesedObjects.GetPossessedObjects()[i]._object.GetComponent<MeshRenderer>();
        }
    }
    /// <summary>
    /// func to get original color of objects
    /// </summary>
    private void GetOriginalColor()
    {
        _originalObjectColor = new Color[_possesedObjects.GetPossessedObjects().Count];//get length of possesed objects
        
        //loop through possesed objects and get their color
        for (int i = 0; i < _possesedObjects.GetPossessedObjects().Count; i++)
        {
            _originalObjectColor[i] = _possesedObjectsColor[i].material.color;
        }
    }
    /// <summary>
    /// self desc
    /// </summary>
    private void ChangeColorOfPosessedObjects()
    {
        //is player using glasses 
        if(_isPlayerWearingGlasses)
        {
            ChangePossesedObjectsColor();
        }
        //is player is not using glasses
        else
        {
            ResetPossesdObjects();
        }
    }
    /// <summary>
    /// changes color of possesed objects
    /// </summary>
    private void ChangePossesedObjectsColor()
    {
        foreach (var possesedObject in _possesedObjectsColor)
        {
            possesedObject.material.color = Color.red;
        }
    }
    /// <summary>
    /// changes color of possesed objects to original color
    /// resizes an array of original color objects
    /// </summary>
    private void ResetPossesdObjects()
    {
        for (int i = 0; i < _possesedObjects.GetPossessedObjects().Count; i++)
        {
            _possesedObjectsColor[i].material.color = _originalObjectColor[i];
        }
        ResizeArrays();
    }
    private void ResizeArrays()
    {
        Array.Resize<Color>(ref _originalObjectColor, 0);
        Array.Resize<MeshRenderer>(ref _possesedObjectsColor, 0);
    }
}
