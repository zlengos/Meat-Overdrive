using Assets.Scripts;
using UnityEngine;

public class Laser : Traps
{
    [SerializeField] private float _rayLength = 100;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    private Transform _transform;

    private const float cooldownTimer = 5;
    private float timer;

    private void Awake() => _transform = GetComponent<Transform>();

    private void Update()
    {
        Shoot();
        timer -= Time.deltaTime;
    }

    protected void Shoot()
    {
        if (Physics2D.Raycast(_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
            Draw2DRay(laserFirePoint.position, _hit.point);

            if (_hit.collider.CompareTag("Player") && timer < 0)
            {
                Debug.Log("killplayer called");
                KillPlayer();
                timer = cooldownTimer;
            }
            

            if (timer > 0)
                lineRenderer.endColor = Color.red;
            else
                lineRenderer.endColor = Color.white;
        }
        else
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * _rayLength);
    }

    private void Draw2DRay(Vector2 startPosition, Vector2 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public override void Killed()
    {
        Debug.Log("Killed by laser");
    }
}
