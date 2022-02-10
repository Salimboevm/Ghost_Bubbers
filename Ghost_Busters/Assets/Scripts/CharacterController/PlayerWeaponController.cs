using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    InputFromPlayer playerInputInstance;
    [SerializeField]
    private GameObject[] weapons;
    [SerializeField]
    GameObject currentWeapon;
    int weaponNumber = 0;
    private void Start()
    {
        playerInputInstance = InputFromPlayer.Instance;
        currentWeapon = weapons[0];
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
        if (playerInputInstance.GetAndReturnScrollWheelValue().y > 0)
            return true;
        return false;
    }
    bool ReturnScrollValueIsDown()
    {
        if (playerInputInstance.GetAndReturnScrollWheelValue().y < 0)
            return true;
        return false;
    
    }
}
