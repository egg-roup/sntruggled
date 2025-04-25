using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MysteryBoxController : MonoBehaviour
{
    [Header("Gun Showcase")]
    public Transform showcasePoint;
    public List<GunPrefabData> gunPrefabs;
    public int showcaseCycles = 8;
    public float cycleDelay = 0.75f;
    public float finalDisplayDuration = 1.5f;

    [Header("Cost Settings")]
    public int coinCost = 10;
    private ScoreManager scoreManager;

    [Header("Animation")]
    public Animator animator;
    public AnimationClip boxAnimationClip;
    private readonly string openTrigger = "OpenChest";

    [Header("Spawn Point")]
    public Transform spawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip boxOpenClip1;
    public AudioClip boxOpenClip2;
    public AudioClip gunDropClip;

    private GameObject currentShowcaseGun;

    [System.Serializable]
    public class GunPrefabData
    {
        public GameObject gunPrefab;
        public int weight = 1;
    }

    void Start()
    {
        scoreManager = ScoreManager.scoreManager;
    }

    public void TryUseBox()
    {
        if (scoreManager.GetCoinCount() >= coinCost)
        {
            scoreManager.coins -= coinCost;
            scoreManager.SendMessage("UpdateUI");
            StartCoroutine(OpenAndRoll());
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private IEnumerator OpenAndRoll()
    {
        // Skip first 4 seconds and play animation at 2x speed
        if (boxAnimationClip != null)
        {
            float normalizedTime = 4f / boxAnimationClip.length;
            animator.speed = 2f;
            animator.Play("OpenChest", -1, normalizedTime);
        }
        else
        {
            Debug.LogWarning("boxAnimationClip not assigned!");
        }

        // Play staggered box open sounds
        if (audioSource && boxOpenClip1 && boxOpenClip2)
        {
            audioSource.PlayOneShot(boxOpenClip1);
            StartCoroutine(PlayClipWithDelay(boxOpenClip2, boxOpenClip1.length * 0.8f));
        }

        // Wait a short bit to sync start of gun cycling with visible animation (post-skip)
        yield return new WaitForSeconds(0.25f);

        GameObject selectedGun = null;

        // 8 quick showcase cycles
        for (int i = 0; i < showcaseCycles; i++)
        {
            selectedGun = GetRandomWeightedGun();

            if (currentShowcaseGun)
                Destroy(currentShowcaseGun);

            currentShowcaseGun = Instantiate(selectedGun, showcasePoint.position, Quaternion.identity);
            currentShowcaseGun.transform.SetParent(showcasePoint);

            Rigidbody rb = currentShowcaseGun.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            yield return new WaitForSeconds(cycleDelay);
        }

        // Final preview
        selectedGun = GetRandomWeightedGun();

        if (currentShowcaseGun)
            Destroy(currentShowcaseGun);

        currentShowcaseGun = Instantiate(selectedGun, showcasePoint.position, Quaternion.identity);
        currentShowcaseGun.transform.SetParent(showcasePoint);

        Rigidbody finalRb = currentShowcaseGun.GetComponent<Rigidbody>();
        if (finalRb != null)
        {
            finalRb.useGravity = false;
            finalRb.isKinematic = true;
        }

        yield return new WaitForSeconds(finalDisplayDuration);

        Destroy(currentShowcaseGun);

        GameObject spawnedGun = Instantiate(selectedGun, spawnPoint.position, Quaternion.identity);

        // Play drop sound
        if (audioSource && gunDropClip)
        {
            audioSource.PlayOneShot(gunDropClip);
        }

        // Reset animation speed for next use
        animator.speed = 1f;
    }

    private GameObject GetRandomWeightedGun()
    {
        int totalWeight = 0;
        foreach (var data in gunPrefabs)
            totalWeight += data.weight;

        int rand = Random.Range(0, totalWeight);
        int sum = 0;
        foreach (var data in gunPrefabs)
        {
            sum += data.weight;
            if (rand < sum)
                return data.gunPrefab;
        }

        return gunPrefabs[0].gunPrefab;
    }

    private IEnumerator PlayClipWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource && clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
