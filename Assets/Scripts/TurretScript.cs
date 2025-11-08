using UnityEngine;
public class TurretDetectionAndShooting : MonoBehaviour
{
    public Transform turretHead;     
    public Transform player;         
    public float detectionRange = 20f;
    public LayerMask obstacleMask;   
    public GameObject projectilePrefab; 
    public Transform firePoint;        
    public float fireForce = 20f;
    public float fireRate = 0.25f;         
    public float rotationSpeed = 5f;
    private bool playerVisible = false;
    private float nextFireTime = 0f;
    public TimerHUD timerHUD;

    void Update()
    {
        DetectPlayer();

        if (playerVisible)
        {
            RotateTowardPlayer();
            UpdateFireRate();
            ShootAtPlayer();
            Debug.Log(timerHUD.currentTime);
        }
    }

    void DetectPlayer()
    {
        playerVisible = false;

        Vector3 directionToPlayer = player.position - turretHead.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            if (Physics.Raycast(turretHead.position, directionToPlayer.normalized, out RaycastHit hit, detectionRange, ~0, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    playerVisible = true;
                    Debug.DrawLine(turretHead.position, hit.point, Color.green);
                }
                else
                {
                    Debug.DrawLine(turretHead.position, hit.point, Color.red);
                }
            }
        }
    }

    void RotateTowardPlayer()
    {
        Vector3 direction = (player.position - turretHead.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                Vector3 aimDirection = (player.position + Vector3.up * 1.0f - firePoint.position).normalized;
                rigid.linearVelocity = aimDirection * fireForce;
            }
            Destroy(bullet, 3f); 
        }
    }

    void UpdateFireRate()
    {
        if (timerHUD != null)
        {
            if (timerHUD.currentTime > 40)
            {
                fireRate = 0.5f;
                fireForce = 20f;
            }
            else if (timerHUD.currentTime > 20)
            {
                fireRate = 0.75f;
                fireForce = 30f;
            }
            else if (timerHUD.currentTime > 10)
            {
                fireRate = 1f;
            }
            else
            {
                fireRate = 2f;
                fireForce = 40f;
            }

        }
    }
}
