using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private AudioSource _shotAudio;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _shotAudio.Play();
            Instantiate(_bulletTemplate, _shootPoint);
        }
    }
}
