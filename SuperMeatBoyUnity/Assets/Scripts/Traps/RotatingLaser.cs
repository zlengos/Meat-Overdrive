using UnityEngine;

public class RotatingLaser : Laser
{
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _rotationDuration = 2f; 

    private float _timer;
    private bool _isRotatingClockwise = true;

    private void Start()
    {
        _timer = _rotationDuration;
    }

    private void Update()
    {
        Shoot();
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _isRotatingClockwise = !_isRotatingClockwise;
            _timer = _rotationDuration;
        }

        Rotate(_isRotatingClockwise);
    }


    private void Rotate(bool Clockwise)
    {
        int ClockwiseNatural = Clockwise ? 1 : -1;
        transform.parent.Rotate(0f, 0f, _rotationSpeed * ClockwiseNatural * Time.deltaTime);
    }

}
