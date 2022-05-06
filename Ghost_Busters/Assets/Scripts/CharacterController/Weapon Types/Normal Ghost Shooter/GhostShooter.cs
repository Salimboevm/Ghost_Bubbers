using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShooter : WeaponTypes
{
    private const uint MAX_NUMBER_OF_BULLETS = 6;
    [Header("Raycast variables")]
    [SerializeField]
    private float _distance = 150f;
    [SerializeField]
    private float _shootForce = 150f;
    [SerializeField]
    private Camera _playerCamera;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Transform _shootPosition;
    Vector3 _direction;

    [SerializeField] Transform _aimDot;

    [Header("Projectile Variables")]
    [SerializeField]
    private GameObject _projectile;
    private uint _numberOfBulletsLeft = MAX_NUMBER_OF_BULLETS;
    private void Start()
    {
        //check and get camera, if not provided use main camera else use given one 
        _playerCamera = _playerCamera ? _playerCamera : Camera.main;

        
        
        InputFromPlayer.Instance.GetShootButtonStarted(ShootGhost);
    }
    private void Update()
    {
        if(Physics.Raycast(_shootPosition.transform.position, _playerCamera.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            _aimDot.position = hit.point;
        }
    }

    protected override void ShootGhost()
    {
        if (weaponController.WeaponTypes != WeaponTypesEnum.rifle)
            return;
        if (_numberOfBulletsLeft.Equals(0)) return;
        OnShoot();
    }
    void OnShoot()
    {
        if (GameManager.instance.GetIsGamePaused())
            return;
        Transform projectileTransform = Instantiate(_projectile,_shootPosition.position, Quaternion.identity).transform;
        RaycastHit hit;
        if (Physics.Raycast(_shootPosition.transform.position, _playerCamera.transform.forward, out hit, Mathf.Infinity))
        {
            projectileTransform.GetComponent<Rigidbody>().AddForce(((hit.point - _shootPosition.position) * 10f), ForceMode.Impulse);
        }

    }
}
