using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class Troop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform troopRotatePoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private string projectileSFX = "BasicShoot";

    [Header("Attribute")]
    [SerializeField] private float troopRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float pps = 1f; //projectiles per second

    private Transform target;
    private float timeToFire;

    private void Update()
    {
        if (target == null) {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        } else
        {
            timeToFire += Time.deltaTime;

            if (timeToFire >= 1f / pps)
            {
                Shoot();
                timeToFire = 0f;
            }
        }
    }

    private void Shoot() 
    {
        GameObject projectileObj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        projectileScript.SetTarget(target);
        AudioManager.Instance.PlaySFX(projectileSFX);
    }

    private void FindTarget() 
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, troopRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }

    }
    private void RotateTowardsTarget() 
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        troopRotatePoint.rotation = Quaternion.RotateTowards(troopRotatePoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange() 
    {
       return Vector2.Distance(target.position, transform.position) <= troopRange;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.forward, troopRange);
    }

}
