using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            bool isElite = collision.transform.GetComponent<EnemyBehaviour>().IsElite;
            PlayerManager.Instance.CurrentHealth -= isElite ? 20 : 10;
        }
    }
}
