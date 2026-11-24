using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyManager.Instance.CollectKey();
        }
        else if (other.CompareTag("NoKeySpawn"))
        {
            KeyManager.Instance.goodLocation = false;
        }
    }
}