using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> Split;
    public event Action<Cube> Destroyed;

    [field:SerializeField] public float SplitChance { get; private set; } = 1.0f;
    [field:SerializeField] public int DecreaseValue { get; private set; } = 2;
    [field:SerializeField] public int DecreaseChance { get; private set; } = 2;

    public Renderer ChoiceCubeColor { get; private set; }
    public Rigidbody CubePhysicsComponent { get; private set; }

    private void Awake()
    {
        ChoiceCubeColor = GetComponent<Renderer>();
        CubePhysicsComponent = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (UnityEngine.Random.value <= SplitChance)
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
        SplitChance = splitChance;
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