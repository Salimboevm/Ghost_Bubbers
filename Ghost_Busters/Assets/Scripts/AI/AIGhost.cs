using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGhost : MonoBehaviour
{
    public enum State
    {
        ChoosingDestination,
        GoingToDestination,
        RunAround,
    }

    AI_SharedInfo _sharedInfo;
    private PossessableObject _currentTarget;
    bool _initalTargetFound = false;

    private int _id = 0;

    private NavMeshAgent _agent;
    private Transform _navigationTarget;

    private State _state;


    // Start is called before the first frame update
    private void OnEnable()
    {
        _sharedInfo = AI_SharedInfo._instance;
        _agent = GetComponent<NavMeshAgent>();
        _state = State.RunAround;
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
                        _navigationTarget = _currentTarget._object.transform;

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
                    _agent.destination = _navigationTarget.position;
                else if (_sharedInfo.GetFreeObjects().Count > 0)
                    _state = State.ChoosingDestination;
                else
                    _state = State.RunAround;

                if(Vector3.Distance(transform.position, _agent.destination) < _agent.stoppingDistance)
                {
                    if (_currentTarget != null)
                    {
                        AI_EventsManager._instance.ObjectPossessed(_currentTarget._objectID, _id);
                        _currentTarget = null;
                    }
                }

                break;
            case State.RunAround:
                if(/*!_initalTargetFound ||*/ _sharedInfo.GetFreeObjects().Count < 1)
                {

                }
                else
                {
                    _state = State.ChoosingDestination;
                }
                break;
        }
    }

    private int FindClosestPosObj()
    {
        List<PossessableObject> freeObjects = _sharedInfo.GetFreeObjects();
        AIGhost[] ghosts = _sharedInfo.GetGhostList();

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

    public int GetID() { return _id; }
    public void SetID(int id) { _id = id; }
    public PossessableObject GetCurrentTarget() { return _currentTarget; }
}
