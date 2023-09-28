using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5.0f; // Velocidad de rotación de la torreta
    [SerializeField]
    private float minRotation = 0.0f;   // Rotación mínima permitida (en grados)
    [SerializeField]
    private float maxRotation = 160.0f; // Rotación máxima permitida (en grados)

    [SerializeField] private GameObject bulletsPosition;
    [SerializeField] private GameObject bullets;

    private bool isMouseDown = false;
    private float timeSinceLastShot = 0.0f;
    private float shotCooldown = 0.5f; // Tiempo de espera entre disparos

    void Update()
    {
        // Obtén la posición actual del mouse en pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Convierte la posición del mouse en un rayo desde la cámara
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Declara una variable para almacenar la rotación deseada
        Quaternion desiredRotation;

        // Lanza un rayo desde la cámara y obtén la rotación deseada
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 turretPosition = transform.position;
            Vector3 direction = targetPosition - turretPosition;

            // Calcula la rotación deseada basada en la dirección del rayo
            desiredRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Si no se golpea nada, la rotación deseada es la rotación actual
            desiredRotation = transform.rotation;
        }

        // Limita la rotación en el eje X y Z
        desiredRotation.eulerAngles = new Vector3(0, desiredRotation.eulerAngles.y, 0);

        // Aplica la rotación suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        // Limita la rotación en el eje Y entre minRotation y maxRotation
        float clampedYRotation = Mathf.Clamp(transform.eulerAngles.y, minRotation, maxRotation);
        transform.eulerAngles = new Vector3(0, clampedYRotation, 0);

        // Actualiza el tiempo transcurrido desde el último disparo
        timeSinceLastShot += Time.deltaTime;

        // Detecta el clic del mouse
        if (Input.GetMouseButtonDown(0) && timeSinceLastShot >= shotCooldown)
        {
            isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        // Si se mantiene presionado el clic del mouse y ha pasado el tiempo de espera, realiza el disparo
        if (isMouseDown && timeSinceLastShot >= shotCooldown)
        {
            Instantiate(bullets, bulletsPosition.transform.position, bulletsPosition.transform.rotation);
            timeSinceLastShot = 0.0f; // Reinicia el contador de tiempo
        }
    }
}
