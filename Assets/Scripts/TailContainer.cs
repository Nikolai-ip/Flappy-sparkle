using System.Collections;
using UnityEngine;

public class TailContainer : MonoBehaviour
{
    [SerializeField] private Silhouette _playerSilhouette;

    private PoolMono<Silhouette> _pool;
    [SerializeField] private int _capacity;
    [SerializeField] private bool _poolAutoExpand;
    [SerializeField] private float _createFrequency;
    private Transform _head;
    public bool CanCreateTail { get; set; }
    void Start()
    {
        _pool = new PoolMono<Silhouette>(_playerSilhouette, _capacity, transform);
        _pool.AutoExpand = _poolAutoExpand;
        _head = FindObjectOfType<MoveController>().transform;
        StartCoroutine(CreateTail());   
    }
    private IEnumerator CreateTail()
    {
        var delay = new WaitForSeconds(_createFrequency);
        while (true) 
        {
            if (CanCreateTail)
                CreateSilhouette();
            yield return delay; 
        }
    }

    public void CreateSilhouette()
    {
        var silhouette = _pool.GetFreeElement();
        silhouette.transform.position = _head.position;
    }

}
