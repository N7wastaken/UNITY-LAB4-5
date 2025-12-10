using UnityEngine;

public class RandomObjectsSpawner : MonoBehaviour
{

    [SerializeField] private GameObject prefab;


    [Min(1)]
    [SerializeField] private int count = 20;

    [SerializeField] private Material[] materials = new Material[5];


    [Tooltip("Dodatkowa wysokość ponad górną powierzchnią platformy.")]
    [SerializeField] private float spawnYOffset = 0.5f;

    [Tooltip("Jeśli true - losowa rotacja wokół osi Y.")]
    [SerializeField] private bool randomYaw = true;

    private Bounds platformBounds;

    private void Awake()
    {
        platformBounds = GetPlatformBounds();
    }

    private void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("Brak przypisanego prefab w Inspectorze.");
            return;
        }

        if (materials == null || materials.Length < 5)
        {
            Debug.LogWarning("Uzupełnij tablicę materials 5 różnymi materiałami (Inspector).");
        }

        for (int i = 0; i < count; i++)
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        Vector3 pos = GetRandomPointOnPlatform(platformBounds);

        Quaternion rot = randomYaw
            ? Quaternion.Euler(0f, Random.Range(0f, 360f), 0f)
            : Quaternion.identity;

        GameObject go = Instantiate(prefab, pos, rot);


        Material m = GetRandomMaterial();
        if (m != null)
        {

            var renderers = go.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                r.sharedMaterial = m;
            }
        }
    }

    private Bounds GetPlatformBounds()
    {

        Collider col = GetComponent<Collider>();
        if (col != null) return col.bounds;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null) return rend.bounds;

        Debug.LogError("Obiekt platformy nie ma ani Collidera, ani Renderera - nie da się wyznaczyć Bounds.");
        return new Bounds(transform.position, Vector3.zero);
    }

    private Vector3 GetRandomPointOnPlatform(Bounds b)
    {
        float x = Random.Range(b.min.x, b.max.x);
        float z = Random.Range(b.min.z, b.max.z);

        float y = b.max.y + spawnYOffset;

        return new Vector3(x, y, z);
    }

    private Material GetRandomMaterial()
    {
        if (materials == null || materials.Length == 0) return null;

        for (int tries = 0; tries < 10; tries++)
        {
            var m = materials[Random.Range(0, materials.Length)];
            if (m != null) return m;
        }

        return null;
    }
}
