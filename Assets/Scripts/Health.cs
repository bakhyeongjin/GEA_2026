
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public static int killCount;

    public int maxHp = 3;
    public int currentHp;
    public Slider hpSlider;      // 플레이어만 연결

    public GameObject dropPrefab;
    public float dropChance = 0.3f;


    public float invincibleTime = 0.5f;
    private float lastHitTime = -999f;

    public Renderer playerRenderer;
    public float blinkInterval = 0.1f;

    public GameObject hitEffect;




    void Start()
    {
        currentHp = maxHp;
        UpdateBar();
    }

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0) return;

        // 플레이어만 피격 후 0.5초 무적
        if (CompareTag("Player"))
        {
            if (Time.time < lastHitTime + invincibleTime)
                return;

            lastHitTime = Time.time;
            StartCoroutine(Blink());
        }

        currentHp -= damage;
        Debug.Log(name + " HP: " + currentHp);

        UpdateBar();

        Vector3 hitPos = transform.position + Vector3.up;

        if (hitEffect != null)
        {
            Instantiate(hitEffect, hitPos, Quaternion.identity);
        }

        if (currentHp <= 0) Die();
    }


    public void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp);
        UpdateBar();
    }

    void UpdateBar()
    {
        if (hpSlider != null)
            hpSlider.value = (float)currentHp / maxHp;
    }

    void Die()
    {
        if (CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }

        else
        {
            killCount++;

            if (dropPrefab != null && Random.value < dropChance)
            {
                Vector3 pos = transform.position + Vector3.up * 0.5f;
                Instantiate(dropPrefab, pos, Quaternion.identity);
            }

            Destroy(gameObject);
        }

    }
    IEnumerator Blink()
    {
        if (playerRenderer == null)
            yield break;

        float elapsed = 0f;

        while (elapsed < invincibleTime)
        {
            playerRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);

            playerRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);

            elapsed += blinkInterval * 2f;
        }

        playerRenderer.enabled = true;
    }

}

