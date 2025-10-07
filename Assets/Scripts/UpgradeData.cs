using System;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    public string upgradeName;
    public string description;
    public int icon;
    public float effectAmount;
    public int purchasedLevel = 0;
    public string affectedStat;
}

