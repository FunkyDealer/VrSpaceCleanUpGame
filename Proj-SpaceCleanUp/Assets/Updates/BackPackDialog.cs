using UnityEngine;

public class BackPackDialog : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
    }

    private void Update()
    {
        if (playerController.getCurrentSpace() < 7) return;
        dialogManager.RunSpeech(3, 1);
        Debug.Log("Passou");
        Destroy(gameObject);
    }
}
