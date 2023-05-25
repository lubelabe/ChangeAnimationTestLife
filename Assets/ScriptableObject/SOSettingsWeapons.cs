using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSettings", menuName = "Settings Weapons")]
public class SOSettingsWeapons : ScriptableObject
{
    public int fireRate;
    public int capacityCharger;
    public int powerOfShoot;
    public int powerForAngle;
}