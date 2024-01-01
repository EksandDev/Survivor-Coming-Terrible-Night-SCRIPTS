using UnityEngine;

public class Oak : MonoBehaviour, IMineable
{
    [SerializeField] private float _health = 10;
    public float Health 
    {
        get { return _health; }
        set { _health = value; } 
    }

    [SerializeField] private GameObject[] _resources;
    public GameObject[] Resources 
    {
        get { return _resources; }
        set { _resources = value; } 
    }

    private Vector3 _spawnResourcesPosition;
    public Vector3 SpawnResourcesPosition => _spawnResourcesPosition;

    private void Start()
    {
        Vector3 _objectPosition = gameObject.transform.position;
        _spawnResourcesPosition = new Vector3(_objectPosition.x, _objectPosition.y + 3, _objectPosition.z);
    }

    public void Destruction(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            DropItem();
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    public void DropItem()
    {
        for (int i = Random.Range(-1, 1); i < Resources.Length; i++)
        {
            Instantiate(Resources[0], _spawnResourcesPosition, Quaternion.identity);
        }

        for (int i = Random.Range(1, 2); i < Resources.Length; i++)
        {
            Instantiate(Resources[1], _spawnResourcesPosition, Quaternion.identity);
        }
    }
}
