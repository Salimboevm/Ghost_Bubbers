using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SharedInfo : MonoBehaviour
{
    [SerializeField] private GameObject _playerGO;

    #region Objects Lists
    [SerializeField] private GameObject[] _objectsGO; // All potentially possable objects
    [SerializeField] private int _numberOfPossesableObjects = 2; // How many we want to possess

    [SerializeField] private List<PossessableObject> _objects; // Actually possessable objects

    private List<PossessableObject> _freeObjects; // No longer possessable objects
    private List<PossessableObject> _targetedObjects; // Objects which have a ghost gunning for it
    private List<PossessableObject> _possessedObjects; // Objects with a ghost in them
    #endregion

    #region Ghost Variables
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private int _ammountOfGhosts = 3;
    [SerializeField] private Transform[] _ghostSpawnPositions;
    [SerializeField] private List<AIGhost> _ghosts;

    #region DevTools
    [SerializeField] private bool _debug = false;
    [SerializeField] private GameObject _directionCubeGO;
    #endregion
    #endregion

    #region Unused
    //private List<AIGhost> _freeGhosts;
    //private List<AIGhost> _capturedGhosts; 
    #endregion

    public static AI_SharedInfo _instance; // Singleton

    private void Awake()
    {
        #region Singleton
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
        #endregion

        DifficultySetter dS = DifficultySetter._instance;
        if(dS != null)
        {
            _ammountOfGhosts = dS.GetNumOfGhosts();
            _numberOfPossesableObjects = dS.GetNumOfGhosts();
        }

        #region Setting up all lists
        _objectsGO = GameObject.FindGameObjectsWithTag("PossessableObject");
        _objects = new List<PossessableObject>();

        _freeObjects = new List<PossessableObject>();
        _targetedObjects = new List<PossessableObject>();
        _possessedObjects = new List<PossessableObject>();

        _ghosts = new List<AIGhost>();
        #endregion

    }

    

    void Start()
    {
        #region Object Assigning
        #region Set Up / Control for adding objects
        List<int> chosenObjects = new List<int>(); // list of idexes for _objectsGO which have already been added

        if (_numberOfPossesableObjects > _objectsGO.Length) //Checks if we set more objects than we actually have, if we did, fix it
            _numberOfPossesableObjects = _objectsGO.Length;
        #endregion

        #region Adding random objects from the ObjectGO Array to the objects list (possessable objects)
        for (int i = 0; i < _numberOfPossesableObjects; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, _numberOfPossesableObjects);

                if (chosenObjects.Count > 0)
                {
                    foreach (int y in chosenObjects)
                    {
                        if (y == random)
                        {
                            random = -1;
                            break;
                        }
                    }
                }
            } while (random == -1);


            chosenObjects.Add(random);
            _objects.Add(new PossessableObject { _object = _objectsGO[random], _objectID = i });
        }
        #endregion

        #endregion

        #region Assigning Free Objects
        foreach (PossessableObject pObj in _objects)
        {
            _freeObjects.Add(pObj);
        }
        #endregion


        SpawnGhosts(_ammountOfGhosts);
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

    #region Listening to events
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
    #endregion

    #region Events
    /// <summary>
    ///  Function called on event, when object becomes possessed
    /// </summary>
    /// <param name="objectID"></param>
    /// <param name="ghostID"></param>
    private void ObjectPossessed(int objectID, int ghostID)
    {
        #region Assigning values
        PossessableObject possessedObject = _objects[objectID];

        possessedObject._possessed = true;
        possessedObject._ghostID = ghostID;
        #endregion

        #region Fixing Lists
        _targetedObjects.Remove(_objects[objectID]);
        _possessedObjects.Add(_objects[objectID]);
        #endregion

        _ghosts[ghostID].gameObject.SetActive(false); // Making ghost "invisible"/in the item
    }

    /// <summary>
    /// Function activated on event, when object is cleared
    /// </summary>
    /// <param name="objectID"></param>
    private void ObjectCleared(int objectID)
    {
        #region Assigning values
        _possessedObjects.Remove(_objects[objectID]);

        PossessableObject possessedObject = _objects[objectID];
        possessedObject._possessed = false;
        //possessedObject._puzzleSolved = true; 
        #endregion

        #region Ghost Re-emerging
        GameObject ghost = _ghosts[possessedObject._ghostID].gameObject;
        ghost.transform.position = possessedObject._object.transform.position;
        ghost.SetActive(true);
        #endregion

        // Making it known, the object is not possessed by anyone
        possessedObject._ghostID = -1;
    } 
    #endregion

    /// <summary>
    /// Spawns a given number of ghosts and assigns them IDs and them assigns them to the list
    /// </summary>
    /// <param name="ammount"></param>
    public void SpawnGhosts(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            int randInt = Random.Range(0, _ghostSpawnPositions.Length);

            GameObject ghostGO = Instantiate(_ghostPrefab, _ghostSpawnPositions[randInt].position, _ghostSpawnPositions[randInt].rotation);

            AIGhost ghost = ghostGO.GetComponent<AIGhost>();
            ghost.SetID(i);

            _ghosts.Add(ghostGO.GetComponent<AIGhost>());

            if (_debug)
            {
                GameObject directionCube = Instantiate(_directionCubeGO, ghostGO.transform.position, ghostGO.transform.rotation);
                directionCube.GetComponent<TempDirectionCube>().SetGhost(ghost);
            }
        }
    }


    #region Getters/Setters
    public List<PossessableObject> GetFreeObjects() { return _freeObjects; }
    public List<PossessableObject> GetTargetedObjects() { return _targetedObjects; }
    public List<PossessableObject> GetPossessedObjects() { return _possessedObjects; }
    public List<PossessableObject> GetAllObjects() { return _objects; }
    public List<AIGhost> GetGhostList() { return _ghosts; }
    public GameObject GetPlayerGO() { return _playerGO; } 
    #endregion
}
