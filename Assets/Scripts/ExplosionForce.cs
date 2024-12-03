using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    private float _force = 100;
    private float _radius = 5;

    public void ApplyExplosion(Vector3 explosionCenter)
    {
        Collider[] _overlappedColliders = Physics.OverlapSphere(explosionCenter, _radius);
        Rigidbody rigidbody;

        for (int i = 0; i < _overlappedColliders.Length; i++)
        {
            rigidbody = _overlappedColliders[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddExplosionForce(_force, explosionCenter, _radius);
            }
        }
    }
}
