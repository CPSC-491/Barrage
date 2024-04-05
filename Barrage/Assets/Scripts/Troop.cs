using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Troop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform troopRotatePoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float troopRange = 3f;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform target;

    private void Update()
    {
        if (target == null) {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        }
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, troopRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }

    }
    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        troopRotatePoint.rotation = Quaternion.RotateTowards(troopRotatePoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange() {
       return Vector2.Distance(target.position, transform.position) <= troopRange;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.forward, troopRange);
    }

}
