using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SharedInfo : MonoBehaviour
{
    [SerializeField] private PossessableObject[] _objects;
    private List<PossessableObject> _freeObjects;
    private List<PossessableObject> _possessedObjects;

    [SerializeField] private AIGhost[] _ghosts;
    #region Unused
    //private List<AIGhost> _freeGhosts;
    //private List<AIGhost> _capturedGhosts; 
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _possessedObjects = new List<PossessableObject>();
        _freeObjects = new List<PossessableObject>();

        #region Unused
        //_freeGhosts = new List<AIGhost>();
        //_capturedGhosts = new List<AIGhost>(); 
        #endregion

        int i = 0;

        foreach (PossessableObject pObj in _objects)
        {
            pObj._objectID = i;
            i++;
            _freeObjects.Add(pObj);
        }

        #region Unused
        //i = 0;
        //foreach (AIGhost ghost in _ghosts)
        //{
        //    ghost.SetID(i);
        //    i++;
        //    _freeGhosts.Add(ghost);
        //} 
        #endregion
    }

    private void OnEnable()
    {
        AI_EventsManager.OnPossessed += ObjectPossessed;
        AI_EventsManager.OnCleared += ObjectCleared;
    }

    private void OnDisable()
    {
        AI_EventsManager.OnPossessed -= ObjectPossessed;
        AI_EventsManager.OnCleared -= ObjectCleared;
    }

    private void ObjectPossessed(int objectID, int ghostID)
    {
        PossessableObject possessedObject = _objects[objectID];
        possessedObject._possessed = true;
        possessedObject._ghostID = ghostID;

        _freeObjects.Remove(_objects[objectID]);
        _possessedObjects.Add(_objects[objectID]);
    }

    private void ObjectCleared(int objectID)
    {
        PossessableObject possessedObject = _objects[objectID];
        possessedObject._possessed = false;
        //possessedObject._puzzleSolved = true;
        possessedObject._ghostID = -1;

        _possessedObjects.Remove(_objects[objectID]);
    }
}
