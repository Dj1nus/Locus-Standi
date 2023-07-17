using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Image _background;

    private Camera _camera;
    private float _timeToHide = 3f;
    private bool _isHidden;
    private Vector3 _defaultScale;

    public void UpdateHealthBat(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;

        if (_isHidden)
        {
            gameObject.transform.localScale = _defaultScale;
            _isHidden = false;
        }

        StopAllCoroutines();
        StartCoroutine(HideRoutine());
    }

    IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(_timeToHide);

        gameObject.transform.localScale = Vector3.zero;
        _isHidden = true;
    }

    void Start()
    {
        _camera = Camera.main;
        _defaultScale = gameObject.transform.localScale;

        gameObject.transform.localScale = Vector3.zero;
        _isHidden = true;
    }

    void Update()
    {
        if (!_isHidden)
        {
            transform.LookAt(_camera.transform.position);
        }
    }
}
