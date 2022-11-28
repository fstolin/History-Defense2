using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float fireRate = 1f;

    Transform target;
    GameObject enemy;
    Transform weapon;
    bool isFiring;

    private void Start()
    {
        weapon = this.transform.Find("BallistaTopMesh");
    }

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
        //ShootTheWeapon();
    }

    private void FindClosestTarget()
    {
        
    }

    // Aims the weapon specified in Start
    private void AimWeapon()
    {
        weapon.transform.LookAt(target);
    }


    private void ShootTheWeapon()
    {        
        if (!isFiring)
        {
            Vector3 ammoPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            GameObject projectile = Instantiate(ammo, ammoPosition, Quaternion.identity);
            StartCoroutine(StartFiring());
            StartCoroutine(LaunchProjectile(this.transform.position, target.position, projectile));
        }
    }

    // Fires the projectile from the tower
    IEnumerator LaunchProjectile(Vector3 start, Vector3 target, GameObject projectile)
    {
        Debug.Log("fire");
        float flightProgress = 0f;
        // Tune starting heights
        target.y += 3.5f;
        start.y += 3.5f;

        while(flightProgress < 0.95f)
        {
            // Time progress
            flightProgress += Time.deltaTime * projectileSpeed;
            // LERPing the path
            projectile.transform.position = Vector3.Lerp(start, target, flightProgress);
            projectile.transform.LookAt(target);
            // Coroutine stuff
            yield return new WaitForEndOfFrame();
        }
        try { enemy.GetComponent<EnemyHealth>().DamageUnit(1); } catch { }
        Destroy(projectile);
    }

    IEnumerator StartFiring()
    {
        isFiring = true;
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }
}
