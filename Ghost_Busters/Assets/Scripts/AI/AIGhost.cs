using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Written By Jonáš Èerný, SID 1823654
/// </summary> 



public class AIGhost : MonoBehaviour
{
    public enum State
    {
        ChoosingDestination,
        GoingToDestination,
        RunAround,
    }
    

    private AI_SharedInfo _sharedInfo;
    private GameObject _playerGO;
    [SerializeField] private float _runAwayDistance = 5f;
    [SerializeField] private float _avoidanceRunAwayDistance = 4f;
    [SerializeField] private float _fleeingDistance = 6f;
    [SerializeField] private float _possessableDistance = 2f;

    private bool _idleWalking = false;

    private PossessableObject _currentTarget;

    private int _id = 0;

    private NavMeshAgent _agent;
    private Transform _mainNavigationTarget;

    private State _state;


    // Start is called before the first frame update
    private void Start()
    {
        _sharedInfo = AI_SharedInfo._instance;
        _agent = GetComponent<NavMeshAgent>();
        _state = State.RunAround;
        _playerGO = _sharedInfo.GetPlayerGO();
    }
    private void OnEnable()
    {
        _idleWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.ChoosingDestination:
                if (_sharedInfo.GetFreeObjects().Count > 0)
                {
                    int objectID = FindClosestPosObj();
                    if (objectID != -1)
                    {
                        _currentTarget = _sharedInfo.GetAllObjects()[objectID];
                        _sharedInfo.GetFreeObjects().Remove(_currentTarget);
                        _sharedInfo.GetTargetedObjects().Add(_currentTarget);
                        _mainNavigationTarget = _currentTarget._object.transform;

                        _state = State.GoingToDestination;
                    }
                    else
                        _state = State.RunAround;
                }
                else
                    _state = State.RunAround;
                break;

            case State.GoingToDestination:
                if (_currentTarget != null && !_currentTarget._possessed)
                {
                    #region Player Avoidance
                    //If the player is in radius, try to avoid him but go to the avoidance position
                    if (Vector3.Distance(transform.position, _playerGO.transform.position) <= _runAwayDistance)
                        _agent.SetDestination(AvoidObject(this.gameObject, _playerGO, _mainNavigationTarget, _runAwayDistance, _avoidanceRunAwayDistance));
                    else
                        _agent.SetDestination(_mainNavigationTarget.position);
                    #endregion
                }
                else if (_sharedInfo.GetFreeObjects().Count > 0)
                {
                    _state = State.ChoosingDestination;
                    break;
                }
                else
                {
                    _state = State.RunAround;
                    break;
                }



                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_mainNavigationTarget.position.x, 0, _mainNavigationTarget.position.z)) < _possessableDistance)
                {
                    if (_currentTarget != null)
                    {
                        AI_EventsManager._instance.ObjectPossessed(_currentTarget._objectID, _id);
                        _currentTarget = null;
                    }
                }

                break;

            case State.RunAround:
                if(_sharedInfo.GetFreeObjects().Count < 1)
                {
                    if (Vector3.Distance(transform.position, _playerGO.transform.position) <= _runAwayDistance)
                    {
                        _idleWalking = false;

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
                    else
                    {
                        if (!_idleWalking)
                        {
                            _agent.SetDestination(RandomNavmeshLocation(5f, transform));
                            _idleWalking = true;
                        }
                        if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_agent.destination.x, 0, _agent.destination.z)) < (_agent.stoppingDistance + 0.5f))
                            _idleWalking = false;
                    }
                }
                else
                {
                    _idleWalking = false;
                    _state = State.ChoosingDestination;
                }
                break;
        }
    }

    private int FindClosestPosObj()
    {
        List<PossessableObject> freeObjects = _sharedInfo.GetFreeObjects();
        List<AIGhost> ghosts = _sharedInfo.GetGhostList();

        int closestObject = -1;

        if (freeObjects.Count > 0)
        {

            float smallestDistance = 20000f;
            float successfullSmallDist = 0f;

            for (int i = 0; i < freeObjects.Count; i++)
            {
                float tempDist = Vector3.Distance(transform.position, freeObjects[i]._object.transform.position);

                if (tempDist < smallestDistance)
                {
                    smallestDistance = tempDist;

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

                    if (!isTargeted)
                    {
                        closestObject = freeObjects[i]._objectID;
                        successfullSmallDist = smallestDistance;
                        //Debug.Log("A target for the Ghost with the ID: " + _id + " was found, it is the object with the ID: " + freeObjects[i]._objectID);
                    }
                    else
                    {
                        smallestDistance = successfullSmallDist;
                        //Debug.LogWarning("A target for the Ghost with the ID: " + _id + " was not found, reseting smallest distance to 20000f");
                    }
                }
            }

            if (closestObject != -1)
            {
                return closestObject;
            }
            else
            {
                _state = State.RunAround;
                //Debug.LogWarning("No free object was found for Ghost with the ID: " + _id + ", which wasn't already targeted by another ghost!");
                return -1;
            }
        }
        else
        {
            _state = State.RunAround;
            //Debug.LogWarning("There are no longer any Free Objects left for Ghost with the ID: " + _id);
            return -1;
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

    #region Getters and Setters
    public int GetID() { return _id; }
    public void SetID(int id) { _id = id; }
    public PossessableObject GetCurrentTarget() { return _currentTarget; }
    public NavMeshAgent GetAgent() { return _agent; } 
    #endregion
}
