
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private DamageFlash _damageFlash;

    private void Start() => _damageFlash = GameObject.FindWithTag("PlayerBody").GetComponent<DamageFlash>();

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        bool isElite = collision.transform.GetComponent<EnemyBehaviour>().IsElite;
        PlayerManager.Instance.CurrentHealth -= isElite ? 20 : 10;
        _damageFlash.CallDamageFlash();
    }
}
