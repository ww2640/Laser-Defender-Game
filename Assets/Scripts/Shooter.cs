using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;

    [Header("Enemy AI")]
    [SerializeField] float enemyFireRate = 1f;
    [SerializeField] float enemyFireRateVariance = 0.5f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }
    
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                              transform.position, 
                                              Quaternion.identity);
            audioPlayer.PlayShootingClip();
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                if (!useAI)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }
                else
                {
                    rb.velocity = -transform.up * projectileSpeed;
                }
                /*rb.velocity = new Vector2(0, projectileSpeed);*/
            }
            Destroy(instance, projectileLifetime);

            if (!useAI)
            {
                yield return new WaitForSecondsRealtime(fireRate);
            }
            else
            {
                yield return new WaitForSecondsRealtime(
                                        Mathf.Clamp(Random.Range(enemyFireRate - enemyFireRateVariance,
                                        enemyFireRate + enemyFireRateVariance),0.1f,float.MaxValue));
            }
        }
    }
}
