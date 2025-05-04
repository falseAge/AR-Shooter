using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private AudioSource _shotAudio;
    [SerializeField] private Animator _animator;

    private Queue<Bullet> _bulletPool = new Queue<Bullet>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bulletTemplate, transform);
            bullet.gameObject.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Shoot");
            _shotAudio.Play();
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPool.Count == 0) return;

        Bullet bullet = _bulletPool.Dequeue();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        bullet.SetDirection(_shootPoint.up);
        bullet.gameObject.SetActive(true);
        bullet.SetActionOnDestroy(ReturnBulletToPool);
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(transform);
        _bulletPool.Enqueue(bullet);
    }
}