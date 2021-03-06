using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriStorage : MonoBehaviour, IInteractible
{
    [SerializeField]
    TextMesh display;

    private int ammount;

    [SerializeField]
    string gameName;
    [SerializeField]
    string description;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(PlayerController player)
    {
        ammount += player.getCurrentSpace();
        player.PlaceTrashInStorage();
        updateDisplay();
    }

    private void updateDisplay()
    {
        display.text = ammount.ToString();
    }

    public (string, string) getInfo(PlayerController player)
    {
        return (gameName, description);
    }
}
