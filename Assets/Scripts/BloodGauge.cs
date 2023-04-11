using UnityEngine;
using UnityEngine.UI;

public class BloodGauge : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _sliderObject;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Animation _animation;

    [Header("Datas")]
    [SerializeField] private int _gauge;

    private bool _isActive;
    private bool _isHidden;

    private void Update()
    {
        _slider.value = _gauge;
        _isActive = _gauge > 0;
        ParticleSystem.EmissionModule emission = _particleSystem.emission;
        emission.rateOverTime = _gauge;

        if (_isActive && _isHidden)
        {
            _animation.Play("CanvasFadeIn");
            _isHidden = false;
        }
        else if (!_isActive && !_isHidden)
        {
            _animation.Play("CanvasFadeOut");
            _isHidden = true;
        }
    }
}
