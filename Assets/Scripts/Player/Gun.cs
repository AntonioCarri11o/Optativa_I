using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float _lastShootTime;
    public LayerMask layers;
    public LineRenderer lineRenderer;
    public float lineDuration = 0.002f;
    private ParticleSystem _muzzleFlash;
    public int damage;
    public int ammo;
    public int magazineAmmo;
    private int _currentAmmo;
    private bool _isReloading = false;
    public int reloadTime;

    private TextMeshProUGUI _ammo_count_tag;
    private  TextMeshProUGUI _ammo_mag_tag;
    private void Awake()
    {
        _lastShootTime = Time.time;
        _muzzleFlash = GetComponent<ParticleSystem>();
        _currentAmmo = magazineAmmo;
        _ammo_count_tag = GameObject.FindGameObjectWithTag("Ammo_Count").GetComponent<TextMeshProUGUI>();
        _ammo_mag_tag = GameObject.FindGameObjectWithTag("Ammo_Mag").GetComponent<TextMeshProUGUI>();
    }
    public void onShoot(Transform cinemachineCameraTarget)
    {
        if(!_isReloading)
        {
            if (Time.time - _lastShootTime < fireRate)
            {
                return;
            }
            if(_currentAmmo  == 0)
            {
                StartCoroutine(reloadGun());
                return;
            }
            _currentAmmo -= 1;
            _ammo_mag_tag.SetText("" + _currentAmmo);
            _lastShootTime = Time.time;
            _muzzleFlash.Play();

            Ray ray = new Ray(cinemachineCameraTarget.position, cinemachineCameraTarget.forward);
            //Vector3 targetPoint = transform.position + cinemachineCameraTarget.forward * 100f; // Punto máximo de alcance

            if (Physics.Raycast(ray, out RaycastHit gunHit))
            {
                //Debug.DrawLine(ray.origin, ray.direction * 30f, Color.red, 100f);
                if (gunHit.collider.CompareTag("Zombie"))
                {
                    ZombieStats zombieHealth = gunHit.collider.GetComponent<ZombieStats>();
                    zombieHealth.GetDamage(25);
                }
            }
        }
    }

    public IEnumerator reloadGun()
    {
        _isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if(ammo > 0)
        {
            print("r fase 1");
            if (magazineAmmo - _currentAmmo < ammo)
            {
                ammo -= magazineAmmo - _currentAmmo;
                _currentAmmo = magazineAmmo;
                print("r fase 2");
            } else
            {
                _currentAmmo = ammo;
            }
        }
        print("c: " + _currentAmmo);
        _ammo_mag_tag.SetText("" + _currentAmmo);
        _ammo_count_tag.SetText("" + ammo);
        _isReloading = false;
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
