using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaypointPlatform : MonoBehaviour
{

    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    [SerializeField] private float speed = 2f;
    [SerializeField] private float arriveDistance = 0.05f;
    [SerializeField] private bool startOnPlay = true;

    private Rigidbody rb;
    private int index = 0;
    private int dir = 1;
    private bool running;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        running = startOnPlay;
    }

    private void FixedUpdate()
    {
        if (!running) return;
        if (waypoints == null || waypoints.Count < 2) return;
        if (waypoints[index] == null) return;

        Vector3 goal = waypoints[index].position;
        Vector3 next = Vector3.MoveTowards(rb.position, goal, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        if (Vector3.Distance(next, goal) <= arriveDistance)
        {
            index += dir;


            if (index >= waypoints.Count)
            {
                dir = -1;
                index = waypoints.Count - 2;
            }

            else if (index < 0)
            {
                dir = 1;
                index = 1;
            }
        }
    }

    public void SetRunning(bool value) => running = value;
}
