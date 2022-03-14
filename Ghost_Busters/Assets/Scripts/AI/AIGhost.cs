using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIGhost : MonoBehaviour
{
    [SerializeField] int _id = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetID() { return _id; }
    public void SetID(int id) { _id = id; }
}
