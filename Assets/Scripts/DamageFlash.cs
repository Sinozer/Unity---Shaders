using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour 
{
    [SerializeField] private float _flashTime = 0.25f;

    private Material _material;
    private Coroutine _coroutine;
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void CallDamageFlash()
    {
        _coroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;
            float currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / _flashTime);

            _material.SetFloat(FlashAmount, currentFlashAmount);

            yield return null;
        }
    }
}