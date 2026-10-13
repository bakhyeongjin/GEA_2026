using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float moveSpeed = 2f;
    public float keepDistance = 8f;
    public float fireRange = 12f;
    public float fireInterval = 2f;
    public float bulletSpeed = 10f;

    Transform player;
    float lastFireTime;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
        lastFireTime = Time.time;
    }

    void Update()
    {
        Debug.Log("EnemyShooter Update 실행 중");

        if (player == null)
        {
            Debug.LogWarning("EnemyShooter: Player를 찾지 못함");
            return;
        }

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0;
        float distance = toPlayer.magnitude;

        Debug.Log("플레이어와 거리: " + distance);

        if (distance > keepDistance)
        {
            float move = moveSpeed * Time.deltaTime;
            transform.position += transform.forward * move;
        }

        bool inRange = distance < fireRange;
        bool ready = Time.time >= lastFireTime + fireInterval;

        Debug.Log("거리: " + distance + " / 사거리 안: " + inRange);
        Debug.Log("발사 준비: " + ready + " / 게임 시간: " + Time.time);

        if (inRange && ready)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 target = player.position + Vector3.up * 1f;
        Vector3 dir = (target - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab,
            firePoint.position, Quaternion.LookRotation(dir));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = dir * bulletSpeed;
    }
}