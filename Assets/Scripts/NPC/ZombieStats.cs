using System.Collections;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    private Renderer[] renderers;
    public int health;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogError($"No se encontraron Renderers en {gameObject.name}. Verifica el modelo.");
        }
    }

    public void GetDamage(int shootDamage)
    {
        StartCoroutine(FlashRed());

        if(shootDamage >= health)
        {
            Die();
            return;

        }
        health -= shootDamage;
    }
    private void Die()
    {
        health = 0;
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        if (renderers.Length > 0)
        {
            // Cambia a rojo
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = Color.red;
            }

            // Espera
            yield return new WaitForSeconds(0.1f);

            // Regresa al color original
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = Color.white; // Cambia este color según el color original
            }
        }
        else
        {
            Debug.LogWarning("No hay Renderers disponibles para cambiar de color.");
        }

    }
}
