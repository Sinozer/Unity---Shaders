using UnityEngine;
using UnityEngine.UI;

public class BloodGauge : MonoBehaviour
{
    [Header("Refs")] [SerializeField] private Slider _slider;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Animation _animation;

    [Header("Data")] [SerializeField] private int _gauge;

    private bool _isActive;
    private bool _isHidden;

    private void Update()
    {
        _gauge = PlayerManager.Instance.BloodGauge;
        _gauge = _gauge is >= 0 and <= 100 ? _gauge : (int)_slider.value;
        _isActive = _gauge > 0;

        ParticleSystem.EmissionModule emission = _particleSystem.emission;
        ParticleSystem.ShapeModule shape = _particleSystem.shape;
        emission.rateOverTime = _gauge;
        shape.radius = _gauge / 2f;


        if (_gauge != (int)_slider.value)
        {
            // If the gauge is not equal to the slider value
        }

        _slider.value = _gauge;

        switch (_isActive)
        {
            case true when _isHidden:
                _animation.Play("CanvasFadeIn");
                _isHidden = false;
                break;
            case false when !_isHidden:
                _animation.Play("CanvasFadeOut");
                _isHidden = true;
                break;
        }
    }
}