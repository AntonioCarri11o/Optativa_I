using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float _lastShootTime;
    public LayerMask layers;
    public LineRenderer lineRenderer;
    public float lineDuration = 0.002f;
    private ParticleSystem _muzzleFlash;

    private void Awake()
    {
        _lastShootTime = Time.time;
        _muzzleFlash = GetComponent<ParticleSystem>();
    }
    public void onShoot (Transform cinemachineCameraTarget)
    {
        if(Time.time - _lastShootTime < fireRate)
        {
            return;
        }
        _lastShootTime = Time.time;
        _muzzleFlash.Play();

        Ray ray = new Ray(cinemachineCameraTarget.position, cinemachineCameraTarget.forward);
        RaycastHit hit;
        Vector3 targetPoint = transform.position + cinemachineCameraTarget.forward * 100f; // Punto máximo de alcance
        Debug.DrawLine(transform.position, targetPoint, Color.red, 1.0f);
        //StartCoroutine(ShowShootEffect(targetPoint));

    }


    private IEnumerator ShowShootEffect(Vector3 targetPoint)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPoint);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
}
