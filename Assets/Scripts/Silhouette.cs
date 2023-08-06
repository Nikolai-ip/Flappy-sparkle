using UnityEngine;

public class Silhouette : MonoBehaviour
{
    private Transform _transform;
    private SpriteRenderer _sr;
    [SerializeField] private float _lifeTime;
    private Color _startColor;
    private float time = 0;
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _startColor = _sr.color;
        _transform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        ChangeColor(time);
        time += Time.fixedDeltaTime;
        if (time>=_lifeTime)
        {
            Die();
        }

        Vector2 pos = _transform.position;   
        _transform.position = pos + Vector2.left * GameManager.Instance.GlobalSpeed;
    }
    private void Die()
    {
        time = 0;
        gameObject.SetActive(false);
    }
    private void ChangeColor(float currentTime)
    {
        float alpha = (_lifeTime - currentTime)/ _lifeTime;
        _sr.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);
    }
}
