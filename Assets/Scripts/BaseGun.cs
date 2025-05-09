using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BaseGun : MonoBehaviour
{

    public BulletUIController bulletUI;

    [Header("Ammo Settings")]
    public int clipSize = 20;
    public int totalAmmo = 50;
    public float bulletDamage = 10f;

    [Header("Audio Settings")]
    public AudioClip shootClip;
    public AudioClip emptyClip;
    [Range(0f, 1f)]
    public float shootVolume = 1f;
    [Range(0f, 1f)]
    public float emptyVolume = 1f;

    private AudioSource gunShoot;
    private AudioSource gunEmpty;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    
    [Header("References")]
    public Rigidbody gunRb;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    
    private int currentClip;
    private bool isHeld = false;
    
    void Start()
    {
        currentClip = clipSize;
        
        // Create a test bullet prefab if none is assigned
        if (bulletPrefab == null)
        {
            Debug.LogWarning("No bullet prefab assigned! Creating a simple sphere bullet for testing.");
            CreateSimpleBulletPrefab();
        }
        
        // Make sure firePoint exists
        if (firePoint == null)
        {
            Debug.LogWarning("No fire point assigned! Creating a default one at the front of the gun.");
            CreateDefaultFirePoint();
        }
        
        // Subscribe to grab, release, and activate events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            //grabInteractable.activated.AddListener(OnActivate);
        }
        else
        {
            Debug.LogError("XRGrabInteractable not assigned to gun!");
        }

        // ui update
        if (bulletUI == null)
        {
            GameObject uiObject = GameObject.Find("BulletUI");
            if (uiObject != null)
            {
                bulletUI = uiObject.GetComponent<BulletUIController>();
            }
            else
            {
                Debug.LogWarning("BulletUI object not found in hierarchy.");
            }
        }
        if (bulletUI != null) {
            bulletUI.UpdateAmmo(currentClip, totalAmmo);
        }

        // Setup audio sources
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        // Initialize audio sources
        gunShoot = gameObject.AddComponent<AudioSource>();
        gunEmpty = gameObject.AddComponent<AudioSource>();

        if (shootClip != null)
        {
            gunShoot.clip = shootClip;
            gunShoot.playOnAwake = false;
            gunShoot.spatialBlend = 1f; 
            gunShoot.volume = shootVolume;
        }

        if (emptyClip != null)
        {
            gunEmpty.clip = emptyClip;
            gunEmpty.playOnAwake = false;
            gunEmpty.spatialBlend = 1f;
            gunEmpty.volume = emptyVolume;
        }
    }

    // Public methods to adjust volume at runtime
    public void SetShootVolume(float volume)
    {
        shootVolume = Mathf.Clamp01(volume);
        if (gunShoot != null)
        {
            gunShoot.volume = shootVolume;
        }
    }

    public void SetEmptyVolume(float volume)
    {
        emptyVolume = Mathf.Clamp01(volume);
        if (gunEmpty != null)
        {
            gunEmpty.volume = emptyVolume;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        gunRb.isKinematic = true;
        //Debug.Log("Gun grabbed");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        gunRb.isKinematic = false;
        //Debug.Log("Gun released");
        Vector3 throwDirection = transform.forward; // you can tweak this
        gunRb.AddForce(throwDirection * 5f, ForceMode.VelocityChange);    
    }
    
    // This new method will be called when the user activates the grabbed object
    private void OnActivate(ActivateEventArgs args)
    {
        //Debug.Log("Gun activated - firing!");
        Fire();
    }

    public void Fire()
    {
        if (!isHeld) return;
        
        // Uncomment later when ammo matters
        if (currentClip <= 0)
        {
            OnEmpty();
            return;
        }
        currentClip--;
        
       Debug.Log("Gun firing");

       // ui update
       if (bulletUI != null) {
        bulletUI.UpdateAmmo(currentClip, totalAmmo);
       }
    
        // Visual debug line to see where bullets should go
        Debug.DrawRay(firePoint.position, firePoint.forward * 5f, Color.red, 2f);
        
        // Spawn bullet
        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.1f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, firePoint.rotation * bulletPrefab.transform.localRotation);
        bullet.SetActive(true); // Make sure the bullet is active
        
        //Debug.Log($"Bullet spawned at {spawnPos}, moving in direction {firePoint.forward}");
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
        rb.isKinematic = false;
        rb.velocity = firePoint.forward * bulletSpeed;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Debug.Log($"Bullet fired with velocity: {rb.velocity.magnitude} in direction {rb.velocity.normalized}");
        }
        else
        {
            Debug.LogError("Bullet has no Rigidbody component!");
        }

        // Make sure collider is not a trigger
        Collider col = bullet.GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = false;
        }
        
        // Set bullet damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.damage = bulletDamage;
        else
            Debug.LogError("Bullet prefab is missing Bullet script component!");
        
        OnShoot();
    }
    
    public virtual void OnShoot()
    {
        if (gunShoot != null && shootClip != null)
        {
            gunShoot.pitch = Random.Range(0.95f, 1.05f);
            gunShoot.PlayOneShot(shootClip, shootVolume);
            Debug.Log("Bang!");
        }
    }
    
    public virtual void OnEmpty()
    {
        if (gunEmpty != null && emptyClip != null)
        {
            gunEmpty.PlayOneShot(emptyClip, emptyVolume);
            Debug.Log("you have no more ammo.");
        }
    }

    private void CreateSimpleBulletPrefab()
    {
        // Create a sphere as a simple bullet
        GameObject simpleBullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        simpleBullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Small bullet
        
        // Add Rigidbody
        Rigidbody rb = simpleBullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        // Add Bullet script
        Bullet bulletScript = simpleBullet.AddComponent<Bullet>();
        bulletScript.damage = bulletDamage;
        bulletScript.lifeTime = 5f;
        
        // Set material to red for visibility
        Renderer renderer = simpleBullet.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
        
        // Store this object and hide it
        simpleBullet.SetActive(false);
        bulletPrefab = simpleBullet;
    }
    private void CreateDefaultFirePoint()
    {
        // Create a fire point at the front of the gun
        GameObject newFirePoint = new GameObject("FirePoint");
        newFirePoint.transform.SetParent(transform, false);
        newFirePoint.transform.localPosition = new Vector3(0, 0, 0.5f); // Adjust position as needed
        firePoint = newFirePoint.transform;
    }
    
    // fix with time
    void OnCollisionEnter(Collision collision)
    {
        if (!isHeld && currentClip == 0)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Deal thrown weapon damage
                float damage = bulletDamage * 10f;
                DummyTarget dummy = collision.gameObject.GetComponent<DummyTarget>();
                if (dummy != null)
                    dummy.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
    }
}