using UnityEngine;

public class BackPackDialog : MonoBehaviour
{
    private DialogManager _dialogManager;
    private PlayerController _playerController;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (_playerController.getCurrentSpace() < 7) return;
        _dialogManager.RunSpeech(3, 1);
        Destroy(gameObject);
    }
}
