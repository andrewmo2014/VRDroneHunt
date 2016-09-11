using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;

    public AudioSource rocketLaunchSound;
    public AudioSource machineGunSound;

    public GameObject rocketSpawnSpot;
    public GameObject rocketPrefab;

    public GameObject bulletPuffPrefab;
    public GameObject machineGun;

    public float machineGunFireRate = 0.1f;
    public float rocketLauncherFireRate = 1f;
    public GameObject machineGunMuzzleFlash;

    private float timeUntilNextShotMachineGun = 0f;
    private float timeUntilNextShotRocketLauncher = 0f;

    private bool fireMachineGun = false;
    private bool fireRocketLancher = false;

    // Use this for initialization
    void Start () {
        leftController.TriggerClicked += LeftController_TriggerClicked;
        rightController.TriggerClicked += RightController_TriggerClicked;

        leftController.TriggerUnclicked += LeftController_TriggerUnclicked;
        rightController.TriggerUnclicked += RightController_TriggerUnclicked;
    }

    private void RightController_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        fireRocketLancher = false;
    }

    private void RightController_TriggerClicked(object sender, ClickedEventArgs e)
    {
        machineGunMuzzleFlash.SetActive(true);
        fireRocketLancher = true;
    }

    private void LeftController_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        fireMachineGun = false;  
    }

    private void LeftController_TriggerClicked(object sender, ClickedEventArgs e)
    { 
        fireMachineGun = true;
    }

    // Update is called once per frame
    void Update () {
        machineGunMuzzleFlash.SetActive(leftController.triggerPressed);
        timeUntilNextShotMachineGun -= Time.deltaTime;
        timeUntilNextShotRocketLauncher -= Time.deltaTime;
       if (fireMachineGun)
       {
         
            if(timeUntilNextShotMachineGun <= 0f)
            {
                timeUntilNextShotMachineGun = machineGunFireRate;
                fireBullet();
            }
       }
       if(fireRocketLancher)
       {
            
            if (timeUntilNextShotRocketLauncher <= 0f)
            {
                timeUntilNextShotRocketLauncher = rocketLauncherFireRate;
                fireRocket();
            }
        }
    }

    private void fireRocket()
    {
        GameObject newRocket = (GameObject)Instantiate(rocketPrefab, rocketSpawnSpot.transform.position, rocketSpawnSpot.transform.rotation);
        Destroy(newRocket, 8f);
        rocketLaunchSound.Play();
    }

    private void fireBullet()
    {
        RaycastHit info;

        if(Physics.Raycast(machineGun.transform.position, machineGun.transform.forward * 1000f,out info, 1000f))
        {

            if(info.collider.tag == "Terrain" || info.collider.tag == "Target")
            { 
                Vector3 hitSpot = info.point;   
                GameObject bulletPuff = (GameObject)Instantiate(bulletPuffPrefab, hitSpot, Quaternion.identity);
                bulletPuff.GetComponent<Rigidbody>().AddExplosionForce(1f, hitSpot, 1f);
                Destroy(bulletPuff, 1f);
            }

        }
        machineGunSound.Play();
    }
}
