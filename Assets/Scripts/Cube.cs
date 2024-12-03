using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1.0f;
    [SerializeField] private int _decreaseValue = 2;
    [SerializeField] private int _decreaseChance = 2;

    public float SplitChance => _splitChance;
    public int DecreaseValue => _decreaseValue;
    public int DecreaseChance => _decreaseChance;

    public event Action<Cube> Split;
    public event Action<Cube> Destroyed;

    public Renderer CubeRenderer { get; private set; }
    public Rigidbody CubeRigidbody { get; private set; }

    private void Awake()
    {
        CubeRenderer = GetComponent<Renderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (UnityEngine.Random.value <= _splitChance)
        {
            TriggerSplit();
        }
        else
        {
            DestroySelf();
        }
    }

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }

    private void TriggerSplit()
    {
        Split?.Invoke(this);
        Destroy(gameObject);
    }

    private void DestroySelf()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}