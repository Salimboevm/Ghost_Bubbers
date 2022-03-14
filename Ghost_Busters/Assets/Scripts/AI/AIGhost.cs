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

    private int _id = 0;

    private NavMeshAgent _agent;
    private Transform _navigationTarget;

    private State _state;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sharedInfo = AI_SharedInfo._instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.ChoosingDestination:
                int objectID = FindClosestPosObj();
                _currentTarget = _sharedInfo.GetFreeObjects()[objectID];
                _navigationTarget = _currentTarget._object.transform;

                _state = State.GoingToDestination;
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
                        gameObject.SetActive(false); 
                    }
                }

                break;
            case State.RunAround:
                break;
        }
    }

    private int FindClosestPosObj()
    {
        List<PossessableObject> possessableObjects = new List<PossessableObject>();
        possessableObjects = _sharedInfo.GetFreeObjects();

        int closestObject = -1;
        float smallestDistance = 20000f;
        for (int i = 0; i < possessableObjects.Count; i++)
        {
            float tempDist = Vector3.Distance(transform.position, possessableObjects[i]._object.transform.position);

            if (tempDist < smallestDistance)
            {
                smallestDistance = tempDist;
                closestObject = possessableObjects[i]._objectID;
            }
        }
        Debug.Log(closestObject);
        return closestObject;
    }

    public int GetID() { return _id; }
    public void SetID(int id) { _id = id; }
}
