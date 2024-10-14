using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    public static CubeManager Instance { get; private set; }

    private List<GameObject> _cubes = new List<GameObject>();
    private int _minValueNewCubes = 2;
    private int _maxValueNewCubes = 7;
    private float _explosionForce = 100f;
    private float _explosionRadius = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnCubes(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        int numberOfCubes = Random.Range(_minValueNewCubes, _maxValueNewCubes);

        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, position, Random.rotation);
            newCube.transform.localScale = newScale;
            newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            Rigidbody rb = newCube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }

            newCube.GetComponent<Cube>().SetSplitChance(newSplitChance);

            AddCube(newCube);
        }
    }

    public void AddCube(GameObject cube)
    {
        _cubes.Add(cube);
    }

    public void RemoveCube(GameObject cube)
    {
        _cubes.Remove(cube);
    }
}
