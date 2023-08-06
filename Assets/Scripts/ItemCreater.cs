using UnityEngine;

public class ItemCreater : MonoBehaviour
{
    float _time = 0;
    private Camera _camera;
    [SerializeField] private float _createNextItemInterval;
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private bool _loopCreate;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] bool _canCreate = true;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _createNextItemInterval && _canCreate)
        {
            if (_loopCreate) 
                _time = 0;
            else
                _canCreate = false;
            CreateRandomItem();
        }
    }

    private void CreateRandomItem()
    {
        var pos = new Vector2(_camera.orthographicSize * _camera.aspect, Random.Range(_minY, _maxY));
        int randomIndex = Random.Range(0, _itemPrefabs.Length);
        Instantiate(_itemPrefabs[randomIndex], pos, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, _maxY), 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, _minY), 0.1f);
    }
}
