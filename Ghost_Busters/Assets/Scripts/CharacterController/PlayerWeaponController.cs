using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
enum WeaponTypesEnum
{
    idle,
    trap,
    vacuum,
    rifle
}
public class PlayerWeaponController : MonoBehaviour
{

    InputFromPlayer playerInputInstance;
    [SerializeField]
    private GameObject[] weapons;
    [SerializeField]
    GameObject currentWeapon;
    int weaponNumber = 0;
    [SerializeField]
    WeaponTypesEnum weaponTypes;

    internal WeaponTypesEnum WeaponTypes { get => weaponTypes;private set => weaponTypes = value; }

    private void Start()
    {
        playerInputInstance = InputFromPlayer.Instance;
        currentWeapon = weapons[weaponNumber];
        WeaponTypes = (WeaponTypesEnum)weaponNumber;

    }
    private void Update()
    {
        ChangeWeapon();

    }
    void ChangeWeapon()
    {
        weapons[weaponNumber].SetActive(false);//deactivate current weapon
        CheckAndCalculateWeaponNumber();//calculate weapon number
        currentWeapon = weapons[weaponNumber];//change current weapon
        currentWeapon.SetActive(true);//activate current weapon
        WeaponTypes = (WeaponTypesEnum)weaponNumber;//change weapon type
    }
    void CheckAndCalculateWeaponNumber()
    {
        if (ReturnScrollValueIsUp())
        {
            if (weaponNumber < weapons.Length - 1)
                weaponNumber += 1;
            else
                weaponNumber = 0;

        }
        if (ReturnScrollValueIsDown())
        {
            if (weaponNumber > 0)
                weaponNumber -= 1;
            else
                weaponNumber = weapons.Length-1;
        }
    }
    bool ReturnScrollValueIsUp()
    {
        return (playerInputInstance.GetScrollWheelValue().y > 0);
    }
    bool ReturnScrollValueIsDown()
    {
        return (playerInputInstance.GetScrollWheelValue().y < 0);
            
    }
    bool ReturnRifleWeaponType()
    {
        if (WeaponTypes == WeaponTypesEnum.rifle)
            return true;
        return false;
    }
    bool ReturnVacuumWeaponType()
    {
        if (WeaponTypes == WeaponTypesEnum.vacuum)
            return true;
        return false;
    }
    bool ReturnTrapWeaponType()
    {
        if (WeaponTypes == WeaponTypesEnum.trap)
            return true;
        return false;
    }
}
