using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        StartCoroutine(ResetPlayerPositionWhenTrackingIsReady());
    }

    IEnumerator ResetPlayerPositionWhenTrackingIsReady()
    {

        yield return new WaitForSeconds(0.1f); // Add a slight delay to ensure the system is ready

        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);

        // Wait until XR input tracking is initialized
        List<XRInputSubsystem> inputSubsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances(inputSubsystems);

        while (inputSubsystems.Count == 0)
        {
            yield return null; // Wait until the XR subsystem is initialized
        }

        // Small buffer wait to ensure XR camera finishes settling
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;

            Transform cameraOffset = player.transform.Find("Camera Offset");
            if (cameraOffset != null)
            {
                cameraOffset.localPosition = Vector3.zero;
                cameraOffset.localRotation = Quaternion.identity;
            }
        }
    }
}


