using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathEffect;

    public event UnityAction<Enemy> Dying;

    private Player _target;
    private float _moveSpeed;
    private Vector3 _orbitAxis;
    private Vector3 _spawnPosition;
    private float _maxDistanceFromSpawn;

    private void Awake()
    {
        _orbitAxis = Random.onUnitSphere;
    }

    public void Initialize(Vector3 spawnPosition, float maxDistance)
    {
        _spawnPosition = spawnPosition;
        _maxDistanceFromSpawn = maxDistance;
        transform.position = spawnPosition + Random.insideUnitSphere * maxDistance;
    }

    private void Update()
    {
        if (_target == null) return;

        transform.RotateAround(_spawnPosition, _orbitAxis, _moveSpeed * Time.deltaTime);

        Vector3 lookDirection = _target.transform.position - transform.position;
        if (lookDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public void SetTargetAndSpeed(Player target, float speed)
    {
        _target = target;
        _moveSpeed = speed;
    }

    public void Die()
    {
        Dying?.Invoke(this);
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}