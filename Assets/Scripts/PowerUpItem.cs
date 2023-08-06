using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [SerializeField] private float _maxMagnitudeVelocity;
    [SerializeField] private float _speed;
    [SerializeField] private float _increaseValue;
    private Rigidbody2D _rb;
    private MoveController _player;
    private Transform _tr;
    private Collider2D _collider;
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (_player)
        {
            Vector2 move = (_player.transform.position - _tr.position).normalized * _speed;
            _rb.velocity += move;
            _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -_maxMagnitudeVelocity, _maxMagnitudeVelocity), Mathf.Clamp(_rb.velocity.y, -_maxMagnitudeVelocity, _maxMagnitudeVelocity));
        }
        else
        {
            _rb.velocity -= new Vector2(_speed, _speed);
            _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x,0,_maxMagnitudeVelocity), Mathf.Clamp(_rb.velocity.y, 0, _maxMagnitudeVelocity));
        }
        _tr.position =(Vector2)_tr.position + Vector2.left * GameManager.Instance.GlobalSpeed;

        if (transform.position.x + _collider.bounds.size.x <= -(_camera.orthographicSize * _camera.aspect))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MoveController player))
        {
            _player = player;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MoveController player))
        {
            _player = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out MoveController player))
        {
            GameManager.Instance.OnPlayerTakePowerUpItem(_increaseValue);
            Destroy(gameObject);
        }
    }
}
