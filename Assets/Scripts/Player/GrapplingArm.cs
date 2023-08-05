using UnityEngine;

public class GrapplingArm : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public ArmRenderer grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform Player;
    public Transform shoulderPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D _springJoint2D;
    public Rigidbody2D _rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [SerializeField] private InputButtonScriptableObject inputToShoot;
    [SerializeField] private InputVectorScriptableObject inputDirection;
    Vector2 directionInput;
    bool isGrappling;
    [SerializeField] float balancingForce;
    void SetIsGrappling( bool value)
    {
        isGrappling = value;
        if (isGrappling)
        {
            StartGrappling();
        }
        else
        {
            StopGrappling();
        }
    }

    void SetInputDirection(Vector2 dir)
    {
        directionInput = dir;
    }

    private void Start()
    {
        grappleRope.enabled = false;
        _springJoint2D.enabled = false;

    }
    private void OnEnable()
    {
        inputDirection.OnValueChanged += SetInputDirection;
        inputToShoot.OnValueChanged += SetIsGrappling;
    }
    private void OnDisable()
    {
        inputDirection.OnValueChanged -= SetInputDirection;
        inputToShoot.OnValueChanged -= SetIsGrappling;
    }

    void StartGrappling()
    {
        SetGrapplePoint();
        Debug.Log("grapple");
    }
    void StopGrappling()
    {
        grappleRope.enabled = false;
        _springJoint2D.enabled = false;
        _rigidbody.gravityScale = 1;
    }
    private void Update()
    {
        if (isGrappling)
        {

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - Player.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    Player.position = Vector2.Lerp(Player.position, targetPos, Time.deltaTime * launchSpeed);
                    
                }
            }
        }
        RotateGun();
    }
    private void FixedUpdate()
    {
        if (isGrappling)
        {
            _rigidbody.AddForce(new Vector2(directionInput.x ,0) * balancingForce);
        }
    }

    void RotateGun()
    {
        shoulderPivot.transform.up = directionInput.normalized;
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = shoulderPivot.position + shoulderPivot.transform.up * 100;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)shoulderPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        _springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            _springJoint2D.distance = targetDistance;
            _springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                _springJoint2D.autoConfigureDistance = true;
                _springJoint2D.frequency = 0;
            }

            _springJoint2D.connectedAnchor = grapplePoint;
            _springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    _springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - Player.position;

                    _springJoint2D.distance = distanceVector.magnitude;
                    _springJoint2D.frequency = launchSpeed;
                    _springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}