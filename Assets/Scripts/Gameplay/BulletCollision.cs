using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private float multiplicatorDamage = 1f;
    [SerializeField] private List<Color> particleColors;

    private GameManager _gameManager;
    private int powerImpact;
    private void Start()
    {
        powerImpact = 1;
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        // llama al game manager para validar el tipo de impacto en los poweups adquiridos
        powerImpact = _gameManager.GetPowerUp();
        if (multiplicatorDamage > powerImpact)
        {
            
            multiplicatorDamage = powerImpact;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Verifica si la colisión involucra al enemigo
        if (other.CompareTag("Enemy"))
        {
            // Realiza las acciones que deseas cuando las partículas colisionan con el enemigo
            // Por ejemplo, puedes reducir la vida del enemigo o destruirlo
            EnemyHitBox enemy = other.GetComponent<EnemyHitBox>();
            if (enemy != null)
            {
//                Debug.Log("PAW"); // Suponiendo que el enemigo tiene un método para recibir daño
                enemy.SetDamage(powerImpact);
            }
        }
    }
    
}
