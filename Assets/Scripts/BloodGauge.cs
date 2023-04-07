using UnityEngine;
using UnityEngine.UI;

public class BloodGauge : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _fillArea;

    private float _gauge;

    private void Update()
    {
        _slider.value = _gauge;


        if (_gauge == 0)
        {
            _fillArea.SetActive(false);
        }
        else
        {
            _fillArea.SetActive(true);
        }
    }
}
