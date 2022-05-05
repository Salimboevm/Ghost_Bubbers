using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Written By Jonáš Èerný, SID 1823654
/// </summary> 



public class AIGhost : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// State Machine
    /// </summary>
    public enum State
    {
        ChoosingDestination,
        GoingToDestination,
        RunAround,
    }


    #region Dependencies
    private AI_SharedInfo _sharedInfo;
    private GameObject _playerGO;
    #endregion

    #region Modifiable Variables
    [SerializeField] private float _runAwayDistance = 5f; // Distance of 1 "step" when Running away from a player
    [SerializeField] private float _avoidanceRunAwayDistance = 4f; // Distance of 1 "step" when avoiding a player
    [SerializeField] private float _fleeingRadius = 6f; // Radius around the player at which ai starts to run away

    [SerializeField] private float _possessableDistance = 2f; // Distance from which a ghost can possess an object
    #endregion


    private int _id = 0;

    #region navigation variables
    private bool _idleWalking = false;

    private PossessableObject _currentTarget;

    private NavMeshAgent _agent;
    private Transform _mainNavigationTarget;
    #endregion

    private State _state; // Enum/State Machine

    #region Audio Variables
    private LocalAudioManager _loAdMan;
    private bool _makeSound = false;
    private bool _notChasing = true;
    private List<string> _ghostSounds = new List<string>() { "CatchMe", "GetMe", "Laugh" };
    #endregion 
    #endregion

    #region Set Up
    private void Start()
    {
        _sharedInfo = AI_SharedInfo._instance;
        _playerGO = _sharedInfo.GetPlayerGO();

        _loAdMan = GetComponent<LocalAudioManager>();

        _agent = GetComponent<NavMeshAgent>();


        _state = State.RunAround;
    }
    #endregion

    #region Making sure everything works
    private void OnEnable()
    {
        _idleWalking = false;
    } 
    #endregion


    void Update()
    {
        switch (_state)
        {
            case State.ChoosingDestination:
                #region If there are possessable objects
                if (_sharedInfo.GetFreeObjects().Count > 0)
                {

                    #region If we find a target
                    int objectID = FindClosestPosObj();
                    if (objectID != -1) // AKA if we found an object
                    {
                        _currentTarget = _sharedInfo.GetAllObjects()[objectID]; // Setting our target

                        #region Updating Lists
                        _sharedInfo.GetFreeObjects().Remove(_currentTarget);
                        _sharedInfo.GetTargetedObjects().Add(_currentTarget);
                        #endregion

                        _mainNavigationTarget = _currentTarget._object.transform; // Setting the actual position to go to

                        _state = State.GoingToDestination;
                    }
                    #endregion
                    else
                        _state = State.RunAround;
                } 
                #endregion
                else
                    _state = State.RunAround;

                #region Audio bools
                _makeSound = true;
                _notChasing = true; 
                #endregion
                break;

            case State.GoingToDestination:
                #region If we have a target, which is not possessed
                if (_currentTarget != null && !_currentTarget._possessed)
                {
                    #region Player Avoidance
                    //If the player is in radius, try to avoid him but go to the avoidance position
                    if (Vector3.Distance(transform.position, _playerGO.transform.position) <= _runAwayDistance)
                    {
                        PlayGetMe(); // Audio cue

                        _agent.SetDestination(AvoidObject(this.gameObject, _playerGO, _mainNavigationTarget, _runAwayDistance, _avoidanceRunAwayDistance));
                    }
                    #region Going To target
                    else
                    {
                        _notChasing = true; //Audio Unblocker

                        _agent.SetDestination(_mainNavigationTarget.position);
                    }
                    #endregion
                    #endregion
                } 
                #endregion
                #region ChoosingDestination
                else if (_sharedInfo.GetFreeObjects().Count > 0)
                {
                    _state = State.ChoosingDestination;
                    break;
                }
                #endregion
                #region RunningAround
                else
                {
                    _state = State.RunAround;
                    break;
                }
                #endregion

                #region AudioBools
                _makeSound = true;
                _notChasing = true;
                #endregion

                #region "Possess" target once close enough
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_mainNavigationTarget.position.x, 0, _mainNavigationTarget.position.z)) < _possessableDistance)
                {
                    if (_currentTarget != null)
                    {
                        AI_EventsManager._instance.ObjectPossessed(_currentTarget._objectID, _id);
                        _currentTarget = null;
                    }
                } 
                #endregion

                break;

            case State.RunAround:
                #region Running Around Aimlessly
                if (_sharedInfo.GetFreeObjects().Count < 1)
                {
                    #region Make Random Noise
                    if (_makeSound)
                        StartCoroutine(WaitToTalk(_ghostSounds));
                    #endregion

                    #region Run Away if player is too close
                    if (Vector3.Distance(transform.position, _playerGO.transform.position) <= _runAwayDistance)
                    {
                        _idleWalking = false;

                        PlayGetMe();

                        #region Unfinished checking if running outisde of navmesh
                        ////Calculate the vector pointing from Player to the Ghost
                        //Vector3 dirFromPlayer = transform.position - _playerGO.transform.position;

                        ////Calculate the new position (with the direction from the Player)
                        //Vector3 newPos = transform.position + dirFromPlayer;

                        //Debug.Log("New Position: " + newPos + "\n" + "Destination: " + _agent.destination + "\n" + "Destination Length: " + _agent.remainingDistance + "\n");

                        //if (!SetDestination(newPos))
                        //    newPos = transform.position + dirFromPlayer.normalized;

                        ////Change the direction of fleeing if it isn't on the NavMesh
                        //else
                        //{
                        //} 
                        #endregion

                        #region Wall avoidance - need to have at least invisible walls to work
                        bool isDirSafe = false;

                        //We will need to rotate the direction away from the player if straight to the opposite of the player is a wall
                        float vRotation = 0;

                        while (!isDirSafe)
                        {
                            //Calculate the vector pointing from Player to the Ghost
                            Vector3 dirFromPlayer = transform.position - _playerGO.transform.position;

                            //Calculate the new position (with the direction from the Player to the Ghost)
                            Vector3 newPos = transform.position + (dirFromPlayer.normalized * _fleeingDistance);

                            //Rotate the direction of the Ghost to move
                            newPos = Quaternion.Euler(0, vRotation, 0) * newPos;

                            //Shoot a Raycast out to the new direction with 5f length (as example raycast length) and see if it hits an obstacle
                            bool isHit = Physics.Raycast(transform.position, newPos, out RaycastHit hit, 3f, LayerMask.GetMask("Wall"));

                            if (hit.transform == null)
                            {
                                //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                                _agent.SetDestination(newPos);
                            }

                            //Change the direction of fleeing if it hits a wall by 20 degrees
                            if (isHit && hit.transform.CompareTag("Wall"))
                            {
                                vRotation += 20;
                                isDirSafe = false;
                            }
                            else
                            {
                                //If the Raycast to the flee direction doesn't hit a wall then the Ghost is good to go to this direction
                                _agent.SetDestination(newPos);
                                isDirSafe = true;
                            }
                            #endregion
                        }
                    }
                    #endregion
                    #region Wander around
                    else
                    {
                        _notChasing = true;

                        if (!_idleWalking)
                        {
                            _agent.SetDestination(RandomNavmeshLocation(5f, transform));
                            _idleWalking = true;
                        }
                        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_agent.destination.x, 0, _agent.destination.z)) < (_agent.stoppingDistance + 0.5f))
                            _idleWalking = false;
                    }
                    #endregion
                } 
                #endregion
                #region Choosing a target
                else
                {
                    _idleWalking = false;
                    _state = State.ChoosingDestination;
                } 
                #endregion
                break;
        }
    }

    #region Legacy Code
    /// <summary>
    /// Unused, find out if a given destination is ont he NavMesh
    /// </summary>
    /// <param name="targetDestination"></param>
    /// <returns></returns>
    private bool SetDestination(Vector3 targetDestination)
    {
        if (NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, 2.3f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Navigation Functions
    /// <summary>
    /// Finds closest possible possessable object
    /// </summary>
    /// <returns></returns>
    private int FindClosestPosObj()
    {
        #region Dependencies
        List<PossessableObject> freeObjects = _sharedInfo.GetFreeObjects();
        List<AIGhost> ghosts = _sharedInfo.GetGhostList();
        #endregion

        int closestObject = -1; // Making sure we find a closest object and if not we return -1

        #region If there are any possessable objects left
        if (freeObjects.Count > 0)
        {
            #region Finding the closest untargeted object
            float smallestDistance = 20000f;
            float successfullSmallDist = 0f;

            for (int i = 0; i < freeObjects.Count; i++)
            {
                float tempDist = Vector3.Distance(transform.position, freeObjects[i]._object.transform.position);

                #region If this is the closest object so far
                if (tempDist < smallestDistance)
                {
                    smallestDistance = tempDist;

                    #region Checking if other ghosts are not targeting the same object
                    bool isTargeted = false;
                    foreach (AIGhost ghost in ghosts)
                    {
                        if (ghost != this)
                        {
                            if (ghost.GetCurrentTarget() != null)
                            {
                                if (freeObjects[i]._objectID == ghost.GetCurrentTarget()._objectID)
                                {
                                    isTargeted = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region If an object is not targeted by another ghost
                    if (!isTargeted)
                    {
                        closestObject = freeObjects[i]._objectID;
                        successfullSmallDist = smallestDistance;
                        //Debug.Log("A target for the Ghost with the ID: " + _id + " was found, it is the object with the ID: " + freeObjects[i]._objectID);
                    }
                    #endregion
                    else
                    {
                        smallestDistance = successfullSmallDist; // If the current smallest distance was a failure we put the last successful distance here instead

                        //Debug.LogWarning("A target for the Ghost with the ID: " + _id + " was not found, reseting smallest distance to 20000f");
                    }
                }
                #endregion
            }
            #endregion

            #region If Successful
            if (closestObject != -1)
            {
                return closestObject;
            }
            #endregion
            #region Otherwise run around
            else
            {
                _state = State.RunAround;
                //Debug.LogWarning("No free object was found for Ghost with the ID: " + _id + ", which wasn't already targeted by another ghost!");
                return -1;
            }
            #endregion
        }
        #endregion
        #region If not, Run around
        else
        {
            _state = State.RunAround;
            //Debug.LogWarning("There are no longer any Free Objects left for Ghost with the ID: " + _id);
            return -1;
        }
        #endregion
    }

    /// <summary>
    /// Finds a random point, located on the NavMesh in a given radius from given center
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Vector3 RandomNavmeshLocation(float radius, Transform center)
    {
        bool pointFound = false;
        Vector3 finalPosition = Vector3.zero;

        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
                pointFound = true;
            }

        } while (!pointFound);

        return finalPosition;
    }

    /// <summary>
    /// Logic for avoiding an object. 
    /// Returns a Vector3 position for a temporary navigation point
    /// </summary>
    /// <param name="avoider"></param>
    /// <param name="toAvoid"></param>
    /// <param name="targetDestination"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private static Vector3 AvoidObject(GameObject avoider, GameObject toAvoid, Transform targetDestination, float radius, float runDistance)
    {
        #region Directions
        Vector3 directionFromTheToAvoidObjectToTheAvoidee = avoider.transform.position - toAvoid.transform.position;
        Vector3 directionFromAvoiderToTheTargetDestination = targetDestination.position - avoider.transform.position;
        #endregion

        #region PointOnRadius (best direction to run away if not going somewhere
        // point on the radius in the direction from the ToAvoid to the avoider
        Vector3 pointOnRadius = toAvoid.transform.position + (directionFromTheToAvoidObjectToTheAvoidee.normalized * radius);


        // Direction from the avoider to this new point on the radius
        Vector3 dirFromAvoToPoint = pointOnRadius - avoider.transform.position;
        #endregion


        #region Getting the "Avoidance Point" to run to
        // Adding the two directions FROM the AI to get a vector/point between them
        Vector3 halfwayVector = directionFromAvoiderToTheTargetDestination + dirFromAvoToPoint;

        // Geting our target position on the circle, going from the center to the halfway point, normalising it and then multiplying by radius
        Vector3 tempTargetPoint = (avoider.transform.position + halfwayVector.normalized * runDistance);
        #endregion


        #region Distances
        float distanceToTarget = Vector3.Distance(avoider.transform.position, targetDestination.position);
        float distanceToAvoidancePoint = Vector3.Distance(avoider.transform.position, tempTargetPoint);
        float distanceToAvoidObject = Vector3.Distance(avoider.transform.position, toAvoid.transform.position);
        #endregion

        #region Returning correct target
        // If the distance to our actual desination is bigger than the distance to our new "avoidance position"
        // AND 
        // The distance to the To Avoid Object is smaller than our distance to the actual target
        // We return the new point, as it is closer to us
        // OTHERWISE we return the target, as if it is closer than the enemy AND the Avoidance Point, there's no reason to run away
        if (distanceToTarget + 1f > distanceToAvoidancePoint && distanceToAvoidObject < distanceToTarget)
            return tempTargetPoint;
        else
            return targetDestination.transform.position;
        #endregion
    } 
    #endregion

    #region Audio Functions
    /// <summary>
    /// Plays a random sound from a list of noises
    /// </summary>
    /// <param name="sounds"></param>
    /// <returns></returns>
    private IEnumerator WaitToTalk(List<string> sounds)
    {
        _makeSound = false;

        int rand = Random.Range(0, 5);
        if (rand == 0)
            _loAdMan.PlayRandomSFX(sounds);


        yield return new WaitForSeconds(5f);
        _makeSound = true;
    }

    /// <summary>
    /// Plays a "never gonna get me" sound
    /// </summary>
    private void PlayGetMe()
    {
        #region Audio
        if (!_notChasing)
        {
            _notChasing = true;
            _loAdMan.PlaySFX("GetMe");
        }
        #endregion
    } 
    #endregion

    #region Getters and Setters
    public int GetID() { return _id; }
    public void SetID(int id) { _id = id; }
    public PossessableObject GetCurrentTarget() { return _currentTarget; }
    public NavMeshAgent GetAgent() { return _agent; } 
    #endregion
}
