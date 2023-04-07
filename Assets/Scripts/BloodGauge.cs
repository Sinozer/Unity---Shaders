using UnityEngine;
using UnityEngine.UI;

public class BloodGauge : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _fillArea;
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Datas")]
    [SerializeField] private int _gauge;

    private void Update()
    {
        _slider.value = _gauge;
        bool isActive = _gauge == 0 ? false : true;
        _fillArea.SetActive(isActive);
        ParticleSystem.EmissionModule emission = _particleSystem.emission;
        emission.rateOverTime = _gauge;
    }
}
