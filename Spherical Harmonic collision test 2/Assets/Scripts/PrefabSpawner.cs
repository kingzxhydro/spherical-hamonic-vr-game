using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject harmonicPrefab;
    private GameObject currentSpawnedHarmonic = null;

    public void SpawnNewHarmonic()
    {
        // Only allow spawning if no current spawned harmonic exists
        if (currentSpawnedHarmonic == null)
        {
            Vector3 newPosition = new Vector3(2, 0, 0);  // Offset from starting harmonic
            currentSpawnedHarmonic = Instantiate(harmonicPrefab, newPosition, Quaternion.identity);

            Debug.Log("New harmonic spawned.");
        }
        else
        {
            Debug.Log("Cannot spawn another harmonic until the current one is used.");
        }
    }

    // Called when the spawned harmonic is used for superposition
    public void ClearSpawnedHarmonic()
    {
        currentSpawnedHarmonic = null;  // Allows spawning again
    }
}


