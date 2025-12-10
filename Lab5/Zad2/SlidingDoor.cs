using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float openDistance = 3f;
    [SerializeField] private float slideAmount = 2f;
    [SerializeField] private float speed = 4f;

    private Vector3 closedPos;

    private void Awake()
    {
        closedPos = transform.position;
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, closedPos);

        Vector3 targetPos = closedPos;

        if (dist <= openDistance)
        {

            Vector3 toPlayer = (player.position - closedPos).normalized;


            float side = Vector3.Dot(transform.forward, toPlayer);


            float dir = (side >= 0f) ? 1f : -1f;

            targetPos = closedPos + transform.right * (dir * slideAmount);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
