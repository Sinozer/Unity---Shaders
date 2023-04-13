using UnityEngine;
using UnityEngine.UI;

public class BloodGauge : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Slider _slider;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Animation _animation;

    [Header("Datas")]
    [SerializeField] private int _gauge;

    private bool _isActive;
    private bool _isHidden;

    private void Update()
    {
        _gauge = _gauge >= 0 && _gauge <= 100 ? _gauge : (int)_slider.value;
        _slider.value = _gauge;
        _isActive = _gauge > 0;

        ParticleSystem.EmissionModule emission = _particleSystem.emission;
        ParticleSystem.ShapeModule shape = _particleSystem.shape;
        emission.rateOverTime = _gauge;
        shape.radius = _gauge / 2;

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
