using System;
using System.Collections;
using UnityEngine;

public class ImpactBehaviour : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _direction;
    private PlayerController playerController;
    
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float objectSpeed;
    private void Start()
    {
        StartCoroutine(StartMovingCoroutine());
        StartCoroutine(DieCoroutine());
    }

    private void Update()
    {
        //Moves in the same direction until death.
        transform.position += _direction * objectSpeed * Time.deltaTime;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void DrawLine()
    {
        
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
            _direction = Vector3.Normalize(_targetPosition - transform.position);
            return true;
        });
    }

    public void SetTargetPosition(Vector3 p) => _targetPosition = p;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.loseHealth(10);
            Debug.LogWarning("Player hit!");
        }
    }
}
