using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _globalSpeed;
    private float _speedCopyValue;
    public float GlobalSpeed => _globalSpeed;
    [SerializeField] private float _minPlayerOuterRadiusLight;
    [SerializeField] private float _maxPlayerOuterRadiusLight;
    [SerializeField] private float _decreaseGlobalLightRate;
    [SerializeField] private Light2D _playerGlobalLight;
    [SerializeField] private GameObject _spaceBarIcon;
    private TailContainer _tail;
    private Rigidbody2D _playerRigidbody;
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool _gameIsStopped;
    public bool GameIsStopped=> _gameIsStopped;
    private float _playerOriginGravityScale;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        _speedCopyValue = _globalSpeed;
        _playerRigidbody = FindObjectOfType<MoveController>().GetComponent<Rigidbody2D>();
        _tail = FindObjectOfType<TailContainer>();
        StopGame(); 
    }
    public void StartGame()
    {
        _spaceBarIcon.SetActive(false);
        _globalSpeed = _speedCopyValue;
        _playerRigidbody.gravityScale = _playerOriginGravityScale;
        _gameIsStopped = false;
        _tail.CanCreateTail = true;
        StartCoroutine(DecreasePlayerLight());
    }
    public void StopGame()
    {
        _spaceBarIcon.SetActive(true);
        _gameIsStopped = true;
        _globalSpeed = 0;
        _playerOriginGravityScale = _playerRigidbody.gravityScale;
        _playerRigidbody.gravityScale = 0;
        _tail.CanCreateTail = false;   
        StopAllCoroutines();
    }
    private IEnumerator DecreasePlayerLight()
    {
        var delay = new WaitForFixedUpdate();
        while (true) 
        {
            _playerGlobalLight.pointLightOuterRadius -= _decreaseGlobalLightRate;
            _playerGlobalLight.pointLightOuterRadius = Mathf.Clamp(_playerGlobalLight.pointLightOuterRadius, _minPlayerOuterRadiusLight, _maxPlayerOuterRadiusLight);
            yield return delay;
        }
    }
    public void OnPlayerTakePowerUpItem(float increaseValue)
    {
        _playerGlobalLight.pointLightOuterRadius += increaseValue;
    }
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
