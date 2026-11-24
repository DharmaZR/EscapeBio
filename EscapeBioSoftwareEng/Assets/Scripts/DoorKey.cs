using UnityEngine;

internal class DoorKey : KeyAffects
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<PlayerController>().OpenDoor();
    }
}
