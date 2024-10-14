using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _splitChance = 1.0f;
    private int _decreaseValue = 2;
    private int _decreaseChance = 2;

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            Split();
        }
    }

    private void Split()
    {
        Vector3 position = transform.position;
        Vector3 newScale = transform.localScale / _decreaseValue;
        float newSplitChance = _splitChance / _decreaseChance;

        CubeManager.Instance.SpawnCubes(position, newScale, newSplitChance);

        DestroySelf();
    }

    private void DestroySelf()
    {
        CubeManager.Instance.RemoveCube(gameObject);

        Destroy(gameObject);
    }

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }
}

