using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private AudioSource _deathAudio;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Enemy newEnemy = Instantiate(_enemy, GetRandomPlaceInSphere(_spawnRadius), Quaternion.identity);
            Vector3 lookDirection = _target.transform.position - newEnemy.transform.position;
            newEnemy.transform.rotation = Quaternion.LookRotation(lookDirection);
            newEnemy.Dying += OnEnemyDying;

            yield return new WaitForSeconds(_secondsBetweenSpawn);
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _deathAudio.Play();
        enemy.Dying -= OnEnemyDying;
        _target.AddScore();
    }

    private Vector3 GetRandomPlaceInSphere(float radius)
    {
        return Random.insideUnitSphere * radius;
    }
}
