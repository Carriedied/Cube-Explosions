using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1.0f;
    [SerializeField] private int _decreaseValue = 2;
    [SerializeField] private int _decreaseChance = 2;

    public event System.Action<Cube> OnDestroyed;

    public Renderer CubeRenderer;
    public Rigidbody CubeRigidbody;

    private void Awake()
    {
        CubeRenderer = GetComponent<Renderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            Split();
        }
        //else
        //{
        //    NotifyAndDestroy();
        //}
    }

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }

    private void Split()
    {
        Vector3 position = transform.position;
        Vector3 newScale = transform.localScale / _decreaseValue;

        float newSplitChance = _splitChance / _decreaseChance;

        CubeSpawner cubeSpawner = FindFirstObjectByType<CubeSpawner>();

        if (cubeSpawner != null)
        {
            cubeSpawner.SpawnCubes(position, newScale, newSplitChance);
        }

        NotifyAndDestroy();
    }

    private void NotifyAndDestroy()
    {
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}

