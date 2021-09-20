using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    Gun gun;
    public GameObject bullet;
    public Transform firepoint;
    public float firerate = 0.6f;
    bool delay = false;
    void Start()
    {
       gun = GetComponent<Gun>(); 
    }

    
    void Update()
    {
        if(gun.shooting)
            Shoot();
    }

    void Shoot(){
        if(delay)
            return;

        Instantiate(bullet, firepoint.position, firepoint.rotation);
        StartCoroutine("ShootDelay");
    }

    IEnumerator ShootDelay(){
        delay = true;
        yield return new WaitForSeconds(firerate);
        delay = false;
    }
}
