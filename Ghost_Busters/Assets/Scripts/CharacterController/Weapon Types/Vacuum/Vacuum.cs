using System.Collections;
using UnityEngine;

public class Vacuum : WeaponTypes
{
    private bool _isGhostTrapped;
    [SerializeField]
    private float _secondsOfVacuumTime;
    private float _originalSpeed;
    AIGhost _catchedGhost;

    private void Start()
    {
        if (weaponController.WeaponTypes != WeaponTypesEnum.trap) return;

        InputFromPlayer.Instance.GetUseGlassesButtonPressed(VacuumGhost);
    }

    protected override void VacuumGhost()
    {
        if (GameManager.instance.GetIsGamePaused())
            return;
        //throw vacuum and enable it
    }
    /// <summary>
    /// waits for set time
    /// releases AI
    /// </summary>
    /// <returns></returns>
    private IEnumerator HoldAI()
    {        
        yield return new WaitForSeconds(_secondsOfVacuumTime);
        ReleaseAI();
    }
    /// <summary>
    /// call this function 
    /// when AI has enough power 
    /// and trap is not working
    /// </summary>
    private void ReleaseAI()
    {
        if (_catchedGhost == null)
            return;
        _catchedGhost.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = _originalSpeed;
        _isGhostTrapped = false;
        _catchedGhost.gameObject.SetActive(true);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            CatchAI(other.GetComponent<AIGhost>());
            FreezeAI();
        }
    } 
    /// <summary>
    /// call this function 
    /// when AI had certain number of shots 
    /// and when it has no power to run
    /// </summary>
    /// <param name="ghost"></param>
    public void CatchAI(AIGhost ghost)
    {
        _catchedGhost = ghost;
        _isGhostTrapped = true;
    }

    /// <summary>
    /// Stops AI from moving 
    /// Hides it 
    /// Starts wait time
    /// </summary>
    private void FreezeAI()
    {
        if (_catchedGhost==null)
            return;
        if (_isGhostTrapped)
        {
            _catchedGhost.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
            HideGhostFromScene();
            StartCoroutine(HoldAI());
        }
    }
    /// <summary>
    /// hide gameobject
    /// </summary>
    /// <param name="ghost">AI</param>
    public void HideGhostFromScene()
    {
        if (_catchedGhost == null)
            return;
        _catchedGhost.gameObject.SetActive(false);
    }
}
