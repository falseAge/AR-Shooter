using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

    private System.Action<Bullet> _onDestroyCallback;
    private Vector3 _direction;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    public void SetActionOnDestroy(System.Action<Bullet> callback)
    {
        _onDestroyCallback = callback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
            _onDestroyCallback?.Invoke(this);
        }
    }

    private void OnBecameInvisible()
    {
        _onDestroyCallback?.Invoke(this);
    }
}