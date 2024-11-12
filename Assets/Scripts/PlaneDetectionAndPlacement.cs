using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetectionAndPlacement : MonoBehaviour
{
    public GameObject dogPrefab; // Assign your dog prefab here
    private GameObject spawnedDog; // To keep track of the spawned dog
    private ARPlaneManager arPlaneManager; // For plane detection

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Only place the dog if no dog has been spawned yet
        if (spawnedDog == null && args.added.Count > 0)
        {
            ARPlane plane = args.added[0];
            SpawnDogOnPlane(plane);
        }
    }

    private void SpawnDogOnPlane(ARPlane plane)
    {
        // Spawn the dog at the center of the detected plane with a horizontal rotation
        spawnedDog = Instantiate(dogPrefab, plane.center, Quaternion.LookRotation(Vector3.forward, Vector3.up));

        // Set each plane's tracking state to Limited so they remain visible but stop further plane tracking
        foreach (var arPlane in arPlaneManager.trackables)
        {
            arPlane.gameObject.SetActive(false); // Optional: Hide planes if they should be invisible
        }

        arPlaneManager.enabled = false; // Disable ARPlaneManager to stop detecting new planes
    }
}
