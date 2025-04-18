using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR; 

public class BaseGun : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int clipSize = 10;
    public int totalAmmo = 50;
    public float bulletDamage = 10f;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    [Header("References")]
    public Rigidbody gunRb;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private int currentClip;
    public InputActionProperty leftTriggerAction;
    public InputActionProperty rightTriggerAction;


    private bool isHeld = false;


    void Start()
    {
        currentClip = clipSize;

        // Subscribe to grab events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    public void Fire()
    {
            
        //do the clip later      
        // if (currentClip <= 0)
        // {
        //     OnEmpty();
        //     return;
        // }
        //
        //currentClip--;

        // Spawn bullet
        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.1f;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletPrefab.transform.localRotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = firePoint.forward * bulletSpeed;

        // Set bullet damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.damage = bulletDamage;

        //OnShoot();
    }
    //do reload later
    // public void Reload()
    // {
    //     int needed = clipSize - currentClip;
    //     int toReload = Mathf.Min(needed, totalAmmo);
    //     currentClip += toReload;
    //     totalAmmo -= toReload;
    // }

    // Placeholder to be filled in by teammate
    // public virtual void OnShoot()
    // {
    //     Debug.Log("Play shoot audio here.");
    // }

    // Called when no bullets
    public virtual void OnEmpty()
    {
        Debug.Log("Play empty audio here.");
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        gunRb.isKinematic = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        gunRb.isKinematic = false;
    }
    //fix with time
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (!isHeld && currentClip <= 0)
    //     {
    //         if (collision.gameObject.CompareTag("Enemy"))
    //         {
    //             // Deal thrown weapon damage
    //             float damage = bulletDamage * 4f;
    //             DummyTarget dummy = collision.gameObject.GetComponent<DummyTarget>();
    //             if (dummy != null)
    //                 dummy.TakeDamage(damage);
    //         }
    //     }
    // }

    void Update()
    {
        
        if (!isHeld) return;
        //Fire();
        if ((leftTriggerAction.action != null && leftTriggerAction.action.WasPressedThisFrame()) ||
            (rightTriggerAction.action != null && rightTriggerAction.action.WasPressedThisFrame()))
        {
            Fire();
        }

    }
    
    
}
