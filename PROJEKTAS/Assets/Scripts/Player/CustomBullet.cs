using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask enemyMask;
    private Enemy1Controller enemy;
    // damage
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        //enemies = gameObject.GetComponents<Enemy1>();
        //damage = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT");
            enemy = collision.transform.gameObject.GetComponent<Enemy1Controller>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
            //Invoke("Dissapear", 0.05f);
            //DealDamage(collision);
            //Debug.Log(collision.gameObject);
            //DealDamage(collision);
            //Destroy(collision.gameObject);
        }
    }
    
    private void DealDamage(Collision collision)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, 1f, enemyMask);
        for (int i = 0; i < enemiesHit.Length; i++)
        {
            enemiesHit[i].GetComponent<Enemy1Controller>().TakeDamage(damage);
        }
        //enemy.TakeDamage(damage);
        Invoke("Dissapear", 0.05f);
    }

    private void Dissapear()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
