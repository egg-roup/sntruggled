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
    public float cycleDelay = 0.5f;
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
        // Start animation at 4 seconds in, speed it up 2x
        if (boxAnimationClip != null)
        {
            float normalizedTime = 4f / boxAnimationClip.length;
            animator.speed = 2f;
            animator.Play("OpenChest", -1, normalizedTime);
        }

        yield return new WaitForSeconds(0.5f); // sync delay

        GameObject selectedGun = null;

        // 8 preview cycles
        for (int i = 0; i < showcaseCycles; i++)
        {
            selectedGun = GetRandomWeightedGun();

            if (currentShowcaseGun)
                Destroy(currentShowcaseGun);

            Quaternion gunRotation = Quaternion.Euler(0f, 90f, 0f);
            currentShowcaseGun = Instantiate(selectedGun, showcasePoint.position, gunRotation);
            currentShowcaseGun.transform.SetParent(showcasePoint);

            Rigidbody rb = currentShowcaseGun.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            yield return new WaitForSeconds(cycleDelay);
        }

        // Final gun display
        selectedGun = GetRandomWeightedGun();

        if (currentShowcaseGun)
            Destroy(currentShowcaseGun);

        Quaternion finalRotation = Quaternion.Euler(0f, 90f, 0f);
        currentShowcaseGun = Instantiate(selectedGun, showcasePoint.position, finalRotation);
        currentShowcaseGun.transform.SetParent(showcasePoint);

        Rigidbody finalRb = currentShowcaseGun.GetComponent<Rigidbody>();
        if (finalRb != null)
        {
            finalRb.useGravity = false;
            finalRb.isKinematic = true;
        }

        yield return new WaitForSeconds(finalDisplayDuration);

        Destroy(currentShowcaseGun);

        GameObject spawnedGun = Instantiate(selectedGun, spawnPoint.position, finalRotation);
        // Let this one fall — don't make it kinematic

        if (audioSource && gunDropClip)
        {
            audioSource.PlayOneShot(gunDropClip);
        }

        animator.speed = 1f; // reset animation speed
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

        return gunPrefabs[0].gunPrefab; // fallback
    }
}
