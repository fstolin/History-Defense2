using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTargetLocator : MonoBehaviour
{
    [SerializeField] int damageDone = 1;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem weapons;
    [SerializeField] ParticleSystem weaponEffect;
    [SerializeField] GameObject rangeSphere;

    Transform target;
    Transform weaponSystem;
    GameObject mySphere;


    public int GetDamageDone()
    {
        return damageDone;
    }

    private void Start()
    {
        mySphere = Instantiate(rangeSphere, this.transform);
        mySphere.SetActive(false);
        weaponSystem = this.transform.Find("BallistaTopMesh");
    }

    private void Update()
    {
        // RANGE DISPLAY
        if (Input.GetKeyDown(KeyCode.X)) SwithcRangeIndicator();

        // GAME LOGIC
        FindClosestTarget();
                
        // Check that gameObejct is active, not null, and in the towers range.
        if (target != null
            && target.gameObject.activeInHierarchy
            && IsTargetInDistance(target))
        {
            AimWeapon();
            ShootTheWeapon();
        } else
        {
            StopShooting();
        }
            
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        // The distance to the current target
        float currentDistance = 0f;

        foreach (Enemy enemy in enemies)
        {
            // Only look for active enemies
            if (!enemy.gameObject.activeInHierarchy) continue;
            
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);

            if (distance < currentDistance || currentDistance == 0)
            {
                target = enemy.transform;
                currentDistance = distance;
            }
        }
    }

    // Checks whether the target is in distance of the tower
    private bool IsTargetInDistance(Transform target)
    {
        float distance = Vector3.Distance(target.transform.position, this.transform.position);
        if (distance > towerRange)
        {
            return false;
        } else
        {
            return true;
        }
    }

    // Aims the weapon at the enemy TODO: LERP bigger angles
    private void AimWeapon()
    {
        weaponSystem.transform.LookAt(target);
    }

    // Fires the weapon, enables fire particle efects
    private void ShootTheWeapon()
    {
        var em = weapons.emission;
        em.enabled = true;
        if (!weapons.isEmitting) weapons.Play();
    }

    // Stops the tower from shooting
    private void StopShooting()
    {
        var em = weapons.emission;
        em.enabled = false;
    }

    // Enables / disables range indicators. TODO: transfer this functionality to a new script
    private void SwithcRangeIndicator()
    {
        mySphere.SetActive(!mySphere.activeSelf);
    }

}
