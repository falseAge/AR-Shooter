using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private int _maxEnemyCount = 1;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private AudioSource _deathAudio;
    [SerializeField] private float _minEnemySpeed = 1f;
    [SerializeField] private float _maxEnemySpeed = 3f;

    private int _enemyCount;

    private void Start()
    {
        _enemyCount = 0;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (_enemyCount < _maxEnemyCount)
            {
                Enemy newEnemy = Instantiate(_enemy);
                newEnemy.Initialize(transform.position, _spawnRadius);
                newEnemy.SetTargetAndSpeed(_target, Random.Range(_minEnemySpeed, _maxEnemySpeed));
                newEnemy.Dying += OnEnemyDying;

                _enemyCount++;
            }

            yield return new WaitForSeconds(_secondsBetweenSpawn);
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _enemyCount--;
        _deathAudio.Play();
        enemy.Dying -= OnEnemyDying;
        _target.AddScore();
    }
}