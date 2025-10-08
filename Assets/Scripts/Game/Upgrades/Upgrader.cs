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
                gunScript.DeleteUpgradedStats("fireRate");
                gunScript.upgradeFireRate = Mathf.Max(0.1f, gunScript.fireRate + upgrade.effectAmount * purchaseLevel);
                gunScript.AddUpgradeStats("fireRate");
                break;
            case "bulletDamage":
                gunScript.DeleteUpgradedStats("bulletDamage");
                gunScript.upgradeBulletDamage += (int)upgrade.effectAmount * purchaseLevel;
                gunScript.AddUpgradeStats("bulletDamage");  
                break;
            case "bulletSpeed":
                gunScript.DeleteUpgradedStats("bulletSpeed");
                gunScript.upgradeBulletSpeed += upgrade.effectAmount * purchaseLevel;
                gunScript.AddUpgradeStats("bulletSpeed");
                break;
            case "bulletRange":
                gunScript.DeleteUpgradedStats("bulletRange");
                gunScript.upgradeRange += upgrade.effectAmount * purchaseLevel;
                gunScript.AddUpgradeStats("bulletRange");
                break;
            case "magazineSize":
                gunScript.DeleteUpgradedStats("magazineSize");
                gunScript.upgradeMagazineSize += (int)upgrade.effectAmount * purchaseLevel;
                gunScript.AddUpgradeStats("magazineSize");
                break;
            case "reloadTime":
                gunScript.DeleteUpgradedStats("reloadTime");
                gunScript.upgradeReloadTime = Mathf.Max(0.1f, gunScript.reloadTime - upgrade.effectAmount * purchaseLevel );
                gunScript.AddUpgradeStats("reloadTime");
                break;
            default:
                break;
        }
    }
}
