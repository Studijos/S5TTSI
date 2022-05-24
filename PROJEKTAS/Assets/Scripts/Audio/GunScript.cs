using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    public float damage = 10f;

    [SerializeField]
    public float range = 100f;

    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy box = hit.transform.GetComponent<Enemy>();
            if(box != null)
            {
                box.TakeDamage(damage);
            }
        }
        
    }
}
