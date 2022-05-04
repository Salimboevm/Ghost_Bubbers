using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SharedInfo : MonoBehaviour
{
    [SerializeField] private GameObject _playerGO;

    #region Lists
    [SerializeField] private GameObject[] _objectsGO; // All potentially possable objects
    [SerializeField] private int _numberOfPossesableObjects = 2; // How many we want to possess
    private List<PossessableObject> _objects; // Actually possessable objects
    private List<PossessableObject> _freeObjects; // No longer possessable objects
    private List<PossessableObject> _targetedObjects; // Objects which have a ghost gunning for it
    private List<PossessableObject> _possessedObjects; // Objects with a ghost in them
    #endregion

    [SerializeField] private AIGhost[] _ghosts;

    #region Unused
    //private List<AIGhost> _freeGhosts;
    //private List<AIGhost> _capturedGhosts; 
    #endregion

    public static AI_SharedInfo _instance;

    private void Awake()
    {
        #region Singleton
        if (_instance == null)
            _instance = this;
        else
            Destroy(this); 
        #endregion

        _objects = new List<PossessableObject>();
        _objectsGO = GameObject.FindGameObjectsWithTag("PossessableObject");

        _freeObjects = new List<PossessableObject>();
        _targetedObjects = new List<PossessableObject>();
        _possessedObjects = new List<PossessableObject>();

        List<int> chosenObjects = new List<int>();
        if (_numberOfPossesableObjects > _objectsGO.Length)
            _numberOfPossesableObjects = _objectsGO.Length;

        for (int m = 0; m < _numberOfPossesableObjects; m++)
        {
            int random = -1;
            if (chosenObjects.Count > 0)
            {
                do
                {
                    random = Random.Range(0, _numberOfPossesableObjects);
                    foreach (int y in chosenObjects)
                    {
                        if (y == random)
                        {
                            random = -1;
                            break;
                        }
                    }
                } while (random == -1);
            }

            _objects.Add(_objectsGO[random].GetComponent<PossessableObject>());
        }

        int i = 0;
        foreach (PossessableObject pObj in _objects)
        {
            pObj._objectID = i;
            i++;
            _freeObjects.Add(pObj);
        }

        i = 0;
        foreach (AIGhost ghost in _ghosts)
        {
            ghost.SetID(i);
            i++;
        }

        
    }


    void Start()
    {
        #region Unused
        //_freeGhosts = new List<AIGhost>();
        //_capturedGhosts = new List<AIGhost>(); 

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
        #region Testing PuzzleSolved Script
        if (_possessedObjects.Count > 0)
        {
            foreach (PossessableObject pObj in _possessedObjects)
            {
                if (pObj._puzzleSolved)
                    AI_EventsManager._instance.ObjectCleared(pObj._objectID);
            }
        } 
        #endregion
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

        _targetedObjects.Remove(_objects[objectID]);
        _possessedObjects.Add(_objects[objectID]);

        _ghosts[ghostID].gameObject.SetActive(false);
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
    public List<PossessableObject> GetTargetedObjects() { return _targetedObjects; }
    public List<PossessableObject> GetPossessedObjects() { return _possessedObjects; }
    public List<PossessableObject> GetAllObjects() { return _objects; }
    public AIGhost[] GetGhostList() { return _ghosts; }
    public GameObject GetPlayerGO() { return _playerGO; }
}
