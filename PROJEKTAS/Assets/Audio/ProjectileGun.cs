using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;
    public float power = 0.0f;
    public int damage;
    [SerializeField]
    public float range = 100f;

    public float shootForce, upwardForce;

    // gun parameters
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    // refernce to camera and attackpoint
    public Camera fpsCam;
    public Transform attackPoint;

    // graphics
    public GameObject muzzleFlash;
    //public TexMeshProGUI ammunitionDisplay;


    public bool allowInvoke = true;


    // Start is called before the first frame update
    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        // show amunition
        //if (ammunitionDisplay != null)
        //    ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

    }

    private void MyInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shootForce += 0.1f;

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Shoot();
            shootForce = 4;
        }
        // check if holding mouse btn is allowed
        //if (allowButtonHold)
        //shooting = Input.GetKey(KeyCode.Mouse0);

        //else
       //     shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        // shooting
        //if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
       // {
        //    bulletsShot = 0;
            //Shoot();
       // }
    }

    private void Shoot()
    {
        readyToShoot = false;


        //Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Check if hit anything
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            Enemy1Controller capsule = hit.transform.GetComponent<Enemy1Controller>();
            if (capsule != null)
            {
                capsule.TakeDamage(damage);
            }
        }
        Vector3 targetPoint = hit.point;



        // calculate direction from attack point to target
        Vector3 directionPrecise = targetPoint - attackPoint.position;

        float deltaX = Random.Range(-spread, spread);
        float deltaY = Random.Range(-spread, spread);

        // calculate direction with spread
        //Vector3 directionSpread = directionPrecise + new Vector3(deltaX, deltaY, 0);


        // Spawn the bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        // rotate bullet towards shooting direction
        currentBullet.transform.forward = directionPrecise.normalized;

        // add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(transform.up * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(-transform.up * 9.8f, ForceMode.Force);
        //bullet.GetComponent<RigidBody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        // Invoke resetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        // instantiate muzzle flash
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
        

    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
