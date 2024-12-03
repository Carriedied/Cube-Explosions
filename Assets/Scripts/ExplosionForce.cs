using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [SerializeField] private float _force = 100f;
    [SerializeField] private float _radius = 5f;

    public void ApplyExplosion(Rigidbody rigidbody, Vector3 explosionCenter)
    {
        if (rigidbody == null) return;

        Vector3 randomPoint = explosionCenter + Random.onUnitSphere * _radius;
        Vector3 forceDirection = (randomPoint - explosionCenter).normalized;

        rigidbody.AddForceAtPosition(forceDirection * _force, randomPoint);
    }
}
