using System.Collections.ObjectModel;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public GameObject keyPrefab;
    public KeyData keyData;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float spawnY;

    private GameObject spawnedKey;
    public bool goodLocation = true;
    public KeyAffects KeyAffects;

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
        goodLocation = true;
        int randIndex = Random.Range(1, 4);
        switch(randIndex)
        {
            case 1:
                KeyAffects = ScriptableObject.CreateInstance<BlindKey>();
              
                break;
            case 2:
                KeyAffects = ScriptableObject.CreateInstance<ReverseControlsKey>();
               
                break;
            case 3:
                KeyAffects = ScriptableObject.CreateInstance<DoorKey>();
                break;
        }
    }

    public void CollectKey()
    {
        keyData.hasKey = true;
        Destroy(spawnedKey);
        Debug.Log("Key collected!");
        KeyAffects.ApplyEffect(GameObject.FindGameObjectWithTag("Player"));
        SpawnKey();
    }

    public void Update()
    {
        if (!goodLocation)
        {
            SpawnKey();
        }
    }

    
}