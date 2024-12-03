using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _initialCube;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubesToSpawn = 2;
    [SerializeField] private int _maxCubesToSpawn = 6;

    private readonly List<Cube> _activeCubes = new List<Cube>();

    private ExplosionForce _explosionForceHandler;

    private void Start()
    {
        _initialCube.Split += HandleCubeSplit;
    }

    private void Awake()
    {
        _explosionForceHandler = new ExplosionForce();
    }

    public void AddCube(Cube cube)
    {
        cube.Split += HandleCubeSplit;
        cube.Destroyed += HandleCubeDestroyed;

        _activeCubes.Add(cube);
    }

    private void HandleCubeSplit(Cube splitCube)
    {
        splitCube.Split -= HandleCubeSplit;
        splitCube.Destroyed -= HandleCubeDestroyed;

        _activeCubes.Remove(splitCube);

        SpawnCubes(splitCube.transform.position,
                   splitCube.transform.localScale / splitCube.DecreaseValue,
                   splitCube.SplitChance / splitCube.DecreaseChance);
    }

    private void HandleCubeDestroyed(Cube destroyedCube)
    {
        destroyedCube.Split -= HandleCubeSplit;
        destroyedCube.Destroyed -= HandleCubeDestroyed;

        _activeCubes.Remove(destroyedCube);
    }

    private void SpawnCubes(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        int numberOfCubes = Random.Range(_minCubesToSpawn, _maxCubesToSpawn + 1);

        for (int i = 0; i < numberOfCubes; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, position, Random.rotation);

            newCube.transform.localScale = newScale;
            newCube.CubeRenderer.material.color = new Color(Random.value, Random.value, Random.value);

            if (newCube.CubeRigidbody != null)
            {
                _explosionForceHandler.ApplyExplosion(newCube.CubeRigidbody, position);
            }

            newCube.Initialize(newSplitChance);

            AddCube(newCube);
        }
    }
}
