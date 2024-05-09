using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Troop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform troopRotatePoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private string projectileSFX = "BasicShoot";
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float troopRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float pps = 1f; //projectiles per second
    [SerializeField] private int baseUpgradeCost = 100;

    private float ppsBase;
    private float troopRangeBase;

    private Transform target;
    private float timeToFire;

    private int level = 1;

    private void Start()
    {
        ppsBase = pps;
        troopRangeBase = troopRange;
        upgradeButton.onClick.AddListener(Upgrade);
    }

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

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.money) return;

        LevelManager.main.SpendMoney(CalculateCost());
        level++;

        pps = CalculatePPS();
        troopRange = CalculateRange();

        CloseUpgradeUI();
        Debug.Log("New BPS: " + pps);
        Debug.Log("New range " + troopRange);
        Debug.Log("new cost: " + CalculateCost());
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculatePPS()
    {
        return ppsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange()
    {
        return troopRangeBase * Mathf.Pow(level, 0.4f);
    }
}
