using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class GunScript : MonoBehaviour
{
    [Header("General")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletContainer;
    public int currentAmmo;
    private float fireCooldown = 0f;


    [Header("Gun Stats")]
    public float fireRate = 0;
    public float reloadTime = 0f;
    public int magazineSize = 0;
    public float bulletDamage = 0f;
    public float range = 0f;
    public float bulletSpeed = 0f;

    [Header("Upgrades")]

    public float fireRateUpgradeAmount = 0;
    public int magazineSizeUpgradeAmount = 0;
    public float reloadTimeUpgradeAmount = 0f;
    public float damageUpgradeAmount = 0f;

    [Header("Aiming")]
    public Transform player;
    public float aimSpeed = 5f;
    public float offset = 1.5f; 
    private Camera mainCamera;
    private Vector3 mousePosition;
    private Vector3 aimDirection;
    private Vector3 aimOffset;


    void Start()
    {
        currentAmmo = magazineSize;
        mainCamera = Camera.main;
    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
            currentAmmo--;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }


    private IEnumerator Reload()
    {
        if (currentAmmo < magazineSize)
        {
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = magazineSize;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(bulletContainer.transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(GetDamage());
        }
    }

    public float GetDamage()
    {
        float damage = bulletDamage + damageUpgradeAmount;
        return damage;
    }




    private void FixedUpdate()
    {

        mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        aimDirection = (mousePosition - player.position).normalized;
        aimOffset = aimDirection * offset;
        Vector3 desiredPosition = player.position + aimOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * aimSpeed);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
    

    private void SetStatsFromData(GunData data)
    {
        fireRate = data.fireRate;
        reloadTime = data.reloadTime;
        magazineSize = data.magazineSize;
        bulletDamage = data.bulletDamage;
        range = data.range;
        bulletSpeed = data.bulletSpeed;
    }

}

