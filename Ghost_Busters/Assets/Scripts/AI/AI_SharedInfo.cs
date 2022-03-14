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

    public static AI_SharedInfo _instance;

    private void Awake()
    {
        if( _instance == null )
            _instance = this;
        else
            Destroy(this);
    }


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

    #region testing
    private void Update()
    {
        if (_possessedObjects.Count > 0)
        {
            foreach (PossessableObject pObj in _possessedObjects)
            {
                if (pObj._puzzleSolved)
                    AI_EventsManager._instance.ObjectCleared(pObj._objectID);
            } 
        }
    }
    #endregion

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
        _possessedObjects.Remove(_objects[objectID]);
        PossessableObject possessedObject = _objects[objectID];
        possessedObject._possessed = false;
        //possessedObject._puzzleSolved = true;

        #region Ghost Re-emerging
        GameObject ghost = _ghosts[possessedObject._ghostID].gameObject;
        ghost.transform.position = possessedObject._object.transform.position;
        ghost.SetActive(true); 
        #endregion

        possessedObject._ghostID = -1;
    }

    public List<PossessableObject> GetFreeObjects() { return _freeObjects; }
}
