using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 5f;

    private void Update()
    {
        _destroyDelay -= Time.deltaTime;
        if( _destroyDelay < 0)
        {
            Destroy(gameObject);
        }
    }
}
