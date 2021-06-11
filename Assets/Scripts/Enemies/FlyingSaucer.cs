using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed;
    [SerializeField] private int _pointForKill;
    [SerializeField] private Bullet _bullet;

    private Vector3 _moveDirection;

    public int PointForKill => _pointForKill;

    public event System.Action<int> Destroed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection,
                                                 _speed * Time.deltaTime);    
    }

    public void Activate(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
        gameObject.SetActive(true);
    }

    public void ApplyDamage(int damage)
    {
        Destroed?.Invoke(_pointForKill);
        gameObject.SetActive(false);
    }
}
