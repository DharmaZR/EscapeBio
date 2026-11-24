using UnityEngine;

internal class ReverseControlsKey : KeyAffects
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<PlayerController>().ReverseControls();
    }
}
