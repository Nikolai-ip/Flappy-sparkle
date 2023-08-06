using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode _risePlayerButton = KeyCode.Space;
    private MoveController _moveController;
    private void Start()
    {
        _moveController = GetComponent<MoveController>();
    }
    private void Update()
    {
        if (Input.GetKey(_risePlayerButton))
        {
            if (GameManager.Instance.GameIsStopped)
            {
                GameManager.Instance.StartGame();
            }
            _moveController.OnRiseButtonPressed();
        }
    }
}
