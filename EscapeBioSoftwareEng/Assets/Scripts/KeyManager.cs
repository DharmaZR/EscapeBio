using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [Header("Key Settings")]
    public GameObject keyPrefab;
    public KeyData keyData;

    [Header("Spawn Boundaries")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float spawnY;

    private GameObject spawnedKey;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        keyData.hasKey = false;
        SpawnKey();
    }

    public void SpawnKey()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);
        spawnedKey = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
    }

    public void CollectKey()
    {
        keyData.hasKey = true;
        Destroy(spawnedKey);
        Debug.Log("Key collected!");
    }
}