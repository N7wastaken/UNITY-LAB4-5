using System.Collections.Generic;
using UnityEngine;

public class ObstacleContactDetector : MonoBehaviour
{
    [SerializeField] private string obstacleTag = "Obstacle";


    private readonly HashSet<Collider> contactsThisFrame = new();

    private readonly HashSet<Collider> contactsPrevFrame = new();

    private void LateUpdate()
    {

        contactsPrevFrame.Clear();
        foreach (var c in contactsThisFrame) contactsPrevFrame.Add(c);


        contactsThisFrame.Clear();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider col = hit.collider;
        if (col == null) return;
        if (!col.CompareTag(obstacleTag)) return;


        bool touchingNow = contactsThisFrame.Add(col);
        bool wasTouching = contactsPrevFrame.Contains(col);


        if (touchingNow && !wasTouching)
        {
            Debug.Log($"Start kontaktu z przeszkodą: {col.name}");
        }
    }
}
