using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        // Verifica si la colisión involucra al enemigo
        if (other.CompareTag("Enemy"))
        {
            // Realiza las acciones que deseas cuando las partículas colisionan con el enemigo
            // Por ejemplo, puedes reducir la vida del enemigo o destruirlo
            Debug.Log("PAW"); // Suponiendo que el enemigo tiene un método para recibir daño
            EnemyController enemigo = other.GetComponent<EnemyController>();
            if (enemigo != null)
            {
                Debug.Log("PAW"); // Suponiendo que el enemigo tiene un método para recibir daño
            }
        }
    }
    
}
