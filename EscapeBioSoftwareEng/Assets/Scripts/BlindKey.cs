using UnityEngine;

internal class BlindKey : KeyAffects
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<PlayerController>().OpenDoor();
    }
}
