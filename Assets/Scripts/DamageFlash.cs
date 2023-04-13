using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour {
    //[SerializeField] private Color flashColor = ;
    [SerializeField] private float _flashTime = 0.25f;

    private Material _material;

    private Coroutine _coroutine;

    private void Start() {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void CallDamageFlash() {
        _coroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher() {
        //_material.SetColor("_FlashColor",flashColor);

        float currentFlashAmount, elapsedTime = 0f;
        
        while (elapsedTime < _flashTime) {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / _flashTime);
            
            _material.SetFloat("_FlashAmount", currentFlashAmount);
            
            yield return null;
        }
    }
}
