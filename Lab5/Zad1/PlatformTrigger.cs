using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private HorizontalPlatform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            platform.StartCycle();
    }
}
