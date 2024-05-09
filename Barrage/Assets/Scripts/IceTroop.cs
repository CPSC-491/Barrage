using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTroop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float troopRange = 3f;
    [SerializeField] private float aps = 0.25f; //attacks per second //attacks every 4s
    [SerializeField] private float freezeTime = 1f;

    private float timeToFire;

    private void Update()
    {  
        timeToFire += Time.deltaTime;
            if (timeToFire >= 1f / aps)
            {
                FreezeEnemies();
                timeToFire = 0f;
            }
    }

    private void FreezeEnemies() 
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, troopRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.updateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
        AudioManager.Instance.PlaySFX("IceShoot");
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);
        em.ResetSpeed();
    }
}
