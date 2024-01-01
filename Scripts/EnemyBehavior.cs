using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _enemyHealth = 10;
    [SerializeField] private float _enemyDamage = 2;
    [SerializeField] private float _enemyMoveSpeed = 5;

    private Rigidbody _rb;
    private GameObject _player;
    private NavMeshAgent _agent;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
