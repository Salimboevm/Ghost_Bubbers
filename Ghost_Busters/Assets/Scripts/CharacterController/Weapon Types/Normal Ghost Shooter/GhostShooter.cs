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
    private Camera _playerCamera;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Transform _shootPosition;

    [Header("Projectile Variables")]
    [SerializeField]
    private GameObject _projectile;
    private uint _numberOfBulletsLeft = MAX_NUMBER_OF_BULLETS;
    private void Start()
    {
        //check and get camera, if not provided use main camera else use given one 
        _playerCamera = _playerCamera ? _playerCamera : Camera.main;

        if (weaponController.WeaponTypes != WeaponTypesEnum.rifle)
            return;
        
        InputFromPlayer.Instance.GetShootButtonStarted(ShootGhost);
    }
    
    protected override void ShootGhost()
    {
        if (_numberOfBulletsLeft.Equals(0)) return;
        OnShoot();
    }
    Vector3 GetRaycastHitDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(_shootPosition.position, _shootPosition.forward, out hit,_distance,_layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    void OnShoot()
    {
        Transform projectileTransform = Instantiate(_projectile,_shootPosition.position, Quaternion.identity).transform;
        Vector3 shootingDirection = (GetRaycastHitDirection() - _shootPosition.position).normalized;

        projectileTransform.GetComponent<ProjectilePhysicsShoot>().Setup(shootingDirection);
        _numberOfBulletsLeft--;
    }
    
}
