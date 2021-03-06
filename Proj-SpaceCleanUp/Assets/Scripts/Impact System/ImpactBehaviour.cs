using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ImpactBehaviour : MonoBehaviour
{
    private Vector3 _direction;
    private Vector3 _targetPosition;
    private Vector3 _lineTargetPosition;
    private LineRenderer _lineRenderer;
    private PlayerController _playerController;

    [SerializeField]
    private float maxLifeTime;
    [SerializeField]
    private float objectSpeed;
    [SerializeField]
    private float outerLine;
    [SerializeField]
    private float innerLine;

    [SerializeField]
    LayerMask interactMask;


    [SerializeField]
    float minRotationSpeed = 0.1f;
    [SerializeField]
    float maxRotationSpeed = 1;

    float rotationSpeed = 0.5f;
    Vector3 rotationAxis = Vector3.up;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.SetPosition(0, new Vector3( 999,  999, 999));
        _lineRenderer.SetPosition(1, new Vector3(999, 999, 1000));


        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void Start()
    {
        StartCoroutine(StartMovingCoroutine());
        StartCoroutine(DieCoroutine());
        _lineTargetPosition = calculateImpactPosition();

        rotationAxis = new Vector3(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361));
        gameObject.transform.Rotate(rotationAxis, Random.Range(0, 360));
    }

    private Vector3 calculateImpactPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _direction, out hit, 1000, interactMask))
        {
            if (hit.collider)
            {
                return hit.point;
            }
        }


        return transform.position + _direction * 1000;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        //Moves in the same direction until death.
        transform.position += _direction * objectSpeed;

        gameObject.transform.Rotate(rotationAxis, rotationSpeed);

        DrawLine();
    }

    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void DrawLine()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _lineTargetPosition);
    }

    //Keeps traveling for X second until death.
    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(maxLifeTime);
        Destroy(gameObject); //Destroy the GameObject to which the script belongs.
    }
    
    //Makes sure all values are set by waiting until it happens.
    private IEnumerator StartMovingCoroutine()
    {
        yield return new WaitUntil(() =>
        {
            if (_targetPosition == null) return false;
            
            DefinePositionInTorus();
            _direction = Vector3.Normalize(_targetPosition - transform.position);
            return true;
        });
    }

    //Choose a position from a torus around the player. This method is called in StartMovingCoroutine.
    private void DefinePositionInTorus()
    {
        var alpha = Random.Range(-180, 180);    //Get a random angle for the calculation
        
        var sin = Math.Sin(alpha);           //Get the Y position
        var cos = Math.Cos(alpha);           //Get the X position (Z in this situation)

        var maxRangeZ = outerLine * sin;     //Calculate max range for Z axis
        var maxRangeY = outerLine * cos;     //Calculate max range for Y axis

        var minRangeZ = innerLine * sin;     //Calculate min range for Z axis
        var minRangeY = innerLine * cos;     //Calculate min range for Z axis

        var zPos = Random.Range((float)minRangeZ, (float)maxRangeZ);    //Random value in Z axis given limits
        var yPos = Random.Range((float)minRangeY, (float)maxRangeY);    //Random value in Y axis given limits

        var playerPos = _playerController.gameObject.transform.position;
        _targetPosition.Set(playerPos.x, playerPos.y + yPos, playerPos.z + zPos);   //Set final positions
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController.loseHealth(10);
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("FlyingDebri") && !other.gameObject.CompareTag("GameController"))
        {
            Destroy(gameObject);
        }

        
    }
}
