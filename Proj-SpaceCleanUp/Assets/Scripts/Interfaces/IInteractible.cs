using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    void Interact(PlayerController player);
    (string, string) getInfo(PlayerController player);

}
