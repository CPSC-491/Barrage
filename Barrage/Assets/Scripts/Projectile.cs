using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;    //this sets the projectiles hitbox

    [Header("Attributes")]
    [SerializeField] private float projectileSpeed = 5f; //how fast projectiles shoot out
    [SerializeField] private int projetileDmg = 1;

    private Transform target;

    public void SetTarget(Transform _target) 
    { 
        target = _target;
    }
    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDmg(projetileDmg);
        Destroy(gameObject);
    }
}
