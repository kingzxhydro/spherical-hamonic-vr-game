using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollisionScript : MonoBehaviour
{
    public GameObject superpositionPrefab; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnedHarmonic"))
        {
            // Destroy both harmonics
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Instantiate the superposed harmonic at (0,0,0)
            GameObject newSuperposed = Instantiate(superpositionPrefab, Vector3.zero, Quaternion.identity);

            // Allow the spawning of a harmonic again
            GameObject spawner = GameObject.Find("HarmonicSpawner");
            PrefabSpawner allowSpawning = spawner.GetComponent<PrefabSpawner>();

            if (allowSpawning != null)
            {
                allowSpawning.ClearSpawnedHarmonic();
            }

            // Get the material from the new superposed harmonic
            GameObject superManager = GameObject.FindWithTag("SuperposedHarmonic");
            GameObject superSphere = superManager.transform.GetChild(0).gameObject;
            SuperpositionManager updater = superSphere.GetComponent<SuperpositionManager>();

            // Update shader
            if (updater != null)
            {
                updater.UpdateShader();

                Debug.Log("Shader update called on collision");
            }

        }
    }



}
