using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public HeroHealth heroHealth;
    public HeroMovement heroMovement;
    public GunScript gunScript;


    public void ApplyUpgrade(UpgradeData upgrade, int purchaseLevel)
    {

        switch (upgrade.affectedStat)
        {
            case "maxHealth":
                heroHealth.maxHealth += (int)upgrade.effectAmount * purchaseLevel;
                break;
            case "currentHealth":
                heroHealth.currentHealth += (int)upgrade.effectAmount * purchaseLevel;
                break;
            case "armor":
                heroHealth.armor += (int)upgrade.effectAmount * purchaseLevel;
                break;
            case "speed":
                heroMovement.moveSpeed += upgrade.effectAmount * purchaseLevel;
                break;
            case "fireRate":
                gunScript.fireRate = Mathf.Max(0.1f, gunScript.fireRate + upgrade.effectAmount * purchaseLevel);
                break;
            case "bulletDamage":
                gunScript.bulletDamage += (int)upgrade.effectAmount * purchaseLevel;
                break;
            case "bulletSpeed":
                gunScript.bulletSpeed += upgrade.effectAmount * purchaseLevel;
                break;
            case "bulletRange":
                gunScript.range += upgrade.effectAmount * purchaseLevel;
                break;
            case "magazineSize":
                gunScript.magazineSize += (int)upgrade.effectAmount * purchaseLevel;
                break;
            case "reloadTime":
                gunScript.reloadTime = Mathf.Max(0.1f, gunScript.reloadTime - upgrade.effectAmount * purchaseLevel );
                break;
            default:
                break;
        }
    }
}
