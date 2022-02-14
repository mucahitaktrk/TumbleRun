using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float _move = 0.5f;
    private float _lastFrameFingerPostionX;
    private float _moveFactorX;

    [SerializeField] private float _boundrey = 0;
    private Rigidbody _playerRigidbody;

    private bool _gamePlay = false;
    private Animator _playerAnimator;

    public List<GameObject> obs;
    private bool _isFinish =false;
    private int sayac = 0;

    private int _skor = 0;
    [SerializeField] private TextMeshProUGUI _skorText;

    private int level;
    [SerializeField] private GameObject nextLevel;

    [SerializeField] private GameObject[] confettiPrafab;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();

        obs = new List<GameObject>(GameObject.FindGameObjectsWithTag("ObstalceTag"));

        nextLevel.SetActive(false);
    }


    void Update()
    {
        _skorText.text = "SKOR : " + _skor;

        if (Input.GetMouseButton(0))
        {
            _gamePlay = true;
        }
        if (_gamePlay)
        {
            if (!_isFinish)
            {
                InputSystem();
                MoverSystem();
                SpeedSystem();
                _playerAnimator.SetBool("Idle", _gamePlay);
            }
            else
            {
                _playerRigidbody.velocity = Vector3.zero;
                nextLevel.SetActive(true);

                confettiPrafab[0].SetActive(true);
                confettiPrafab[1].SetActive(true);
            }

        }

        if (sayac < obs.Count)
        {
            if (Vector3.Distance(transform.position, obs[sayac].transform.position) <= 5.0f)
            {
                _playerAnimator.SetTrigger("Jump");
                sayac++;
            }
        }

        _playerAnimator.SetBool("Finish", _isFinish);
    }

    private void InputSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPostionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPostionX;
            _lastFrameFingerPostionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
        }

        float xBondrey = Mathf.Clamp(value: transform.position.x, min: -_boundrey, max: _boundrey);
        transform.position = new Vector3(xBondrey, transform.position.y, transform.position.z);
    }

    private void MoverSystem()
    {
        float swaerSystem = Time.fixedDeltaTime * _move * 0.8f * _moveFactorX;
        transform.Translate(swaerSystem, 0, 0);
    }

    private void SpeedSystem()
    {
        if (!_isFinish)
        {
            _playerRigidbody.velocity = new Vector3(0, 0, 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SphereTag")
        {
            Destroy(other.gameObject);
            _skor++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _isFinish = true;
            transform.DOMoveX(0, 1.0f);
            transform.DORotate(new Vector3(0, 180, 0), 4f);
        }
    }

    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene(level);
    }
}
