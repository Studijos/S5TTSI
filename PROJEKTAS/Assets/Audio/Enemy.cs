using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
     
    [SerializeField]
    private Vector3 size = new Vector3(16f, 0f, 16f);

    private float health = 50f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    private RaycastHit hit;
    private RaycastHit hit2;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(enemy);
        CreateEnemy();


    }

    private void Start()
    {

    }

    public void CreateEnemy()
    {
        //enemy.transform.localScale += enemy.transform.localScale;
        enemy.transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z * 2);

        GameObject EnemyClone = Instantiate(enemy, new Vector3(enemy.transform.position.x + enemy.transform.localScale.x * 2,
            enemy.transform.position.y + enemy.transform.localScale.y / 2, enemy.transform.position.z), enemy.transform.rotation);
        GameObject EnemyClone1 = Instantiate(enemy, new Vector3(enemy.transform.position.x + enemy.transform.localScale.x * 4,
            enemy.transform.position.y + enemy.transform.localScale.y / 2, enemy.transform.position.z), enemy.transform.rotation);

    }

    public Vector3 GetRandomPosition()
    {
        var volumePosition = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z)
            );

        return transform.position
            + volumePosition
            - size / 2;

    }
}
