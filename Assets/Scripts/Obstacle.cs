using System.Collections;
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private AnimationCurve _moveTrajectory;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private float _height;
    [SerializeField] private bool _canMove = true;
    private Collider2D _collider;
    private Camera _camera;

    void Start()
    {
        _collider = GetComponent<Collider2D>(); 
        _camera = Camera.main;
        _transform = GetComponent<Transform>(); 
        _startPos = _transform.position;
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        var delay = new WaitForFixedUpdate();
        float time = 0;
        
        while (true)
        {
            _transform.position = _startPos + (Vector2.up * _moveTrajectory.Evaluate(time) * _height);
            if (_canMove )
                _startPos += Vector2.left * GameManager.Instance.GlobalSpeed;
            time += Time.fixedDeltaTime * _speed;
            yield return delay;
        }
    }
    private void Update()
    {
        
        if (transform.position.x + _collider.bounds.size.x <= -(_camera.orthographicSize * _camera.aspect))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out MoveController player))
        {
            GameManager.Instance.GameOver();
        }
    }

}
