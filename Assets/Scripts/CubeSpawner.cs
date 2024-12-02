using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubesToSpawn = 2;
    [SerializeField] private int _maxCubesToSpawn = 6;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 5f;

    private readonly List<Cube> _activeCubes = new List<Cube>();

    public void SpawnCubes(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        Vector3 randomPoint;
        Vector3 forceDirection;

        int numberOfCubes = Random.Range(_minCubesToSpawn, _maxCubesToSpawn + 1);

        for (int i = 0; i < numberOfCubes; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, position, Random.rotation);

            newCube.transform.localScale = newScale;
            newCube.CubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);

            if (newCube.CubeRigidbody != null)
            {
                randomPoint = position + Random.onUnitSphere * _explosionRadius;
                forceDirection = (randomPoint - position).normalized;

                newCube.CubeRigidbody.AddForceAtPosition(forceDirection * _explosionForce, randomPoint);
            }

            newCube.Initialize(newSplitChance);

            AddCube(newCube);
        }
    }

    public void AddCube(Cube cube)
    {
        cube.OnDestroyed += RemoveCube;
        _activeCubes.Add(cube);
    }

    private void RemoveCube(Cube cube)
    {
        cube.OnDestroyed -= RemoveCube;
        _activeCubes.Remove(cube);
    }
}
