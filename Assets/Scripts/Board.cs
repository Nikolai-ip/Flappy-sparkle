using UnityEngine;

public class Board : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out MoveController player))
        {
            GameManager.Instance.GameOver();
        }
    }
}
