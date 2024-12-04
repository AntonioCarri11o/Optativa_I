using StarterAssets;
using UnityEngine;
using UnityEngine.Windows;

public class Camera : MonoBehaviour
{
    public float speedCameraRotation = 50f;
    private float _yaw;
    private float _pitch;
    public float sensitivity = 10f;
    public Vector2 pitchLimits = new Vector2(-30f, 60f);

    private GameObject _player;
    StarterAssetsInputs _input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _input = _player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Look()
    {
        _yaw = _input.look.x * sensitivity * Time.deltaTime;
        _pitch = _input.look.y * sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, pitchLimits.x, pitchLimits.y);
        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }
}
