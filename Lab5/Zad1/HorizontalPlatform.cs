using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float arriveDistance = 0.05f;

    private Rigidbody rb;
    private Vector3 startPos;
    private bool running;
    private bool goingToTarget = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!running || targetPoint == null) return;

        Vector3 goal = goingToTarget ? targetPoint.position : startPos;
        Vector3 next = Vector3.MoveTowards(rb.position, goal, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        if (Vector3.Distance(next, goal) <= arriveDistance)
        {
            if (goingToTarget)
                goingToTarget = false;   // wracamy
            else
                running = false;         // koniec cyklu
        }
    }

    public void StartCycle()
    {
        // start tylko gdy stoimy na dole (żeby nie spamować)
        if (!running)
        {
            running = true;
            goingToTarget = true;
        }
    }
}
