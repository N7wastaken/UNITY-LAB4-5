using UnityEngine;

public class LaunchPadTrigger : MonoBehaviour
{
    [SerializeField] private float multiplier = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CC_Movement player = other.GetComponent<CC_Movement>();
        if (player != null)
        {
            player.LaunchMultiplier(multiplier);
        }
    }
}
