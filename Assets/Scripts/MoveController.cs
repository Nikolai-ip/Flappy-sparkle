using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _riseVelocity;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void OnRiseButtonPressed() 
    {
        _rb.velocity = Vector2.up * _riseVelocity;
    }
}
