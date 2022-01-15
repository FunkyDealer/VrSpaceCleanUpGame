using System.Collections;
using UnityEngine;

public class ComputerScreen : MonoBehaviour
{
    private PlayerController _playerController;
    private bool _canMove;
    
    
    [SerializeField] private Transform interactionTransform;
    void Start()
    {
        _canMove = false;
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        var temp = new Vector3();
        if (_canMove)
        {
            _playerController.gameObject.transform.position = Vector3.SmoothDamp(_playerController.gameObject.transform.position, interactionTransform.position, ref temp, 0.1f, 100f);
        }

        if (interactionTransform.position == _playerController.gameObject.transform.position)
        {
            _canMove = false;
        }
    }

    //Stops and allows the player to move
    public void LockPlayer()
    {
        _playerController.ChangeMovement(false);
        _canMove = true;
    }

    public void UnlockPlayer()
    {
        StartCoroutine(StopUsingScreenCoroutine());
    }

    private IEnumerator StopUsingScreenCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        _playerController.ChangeMovement(true);
        _canMove = false;
    }
}
