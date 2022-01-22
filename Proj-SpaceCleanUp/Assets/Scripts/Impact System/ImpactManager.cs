using UnityEngine;
using Random = UnityEngine.Random;

public class ImpactManager : MonoBehaviour
{
    private Vector3 _spawnPosition;

    [SerializeField] [Range(0, 100)] private float spawnDistance;
    [SerializeField] [Range(0, 100)] private float maxZDistance;
    [SerializeField] private GameObject impactObject;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private PlayerController playerController;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SpawnImpact();
        }
    }

    private void SpawnImpact()
    {
        var position = playerPosition.position + playerPosition.right * ((Random.Range(0, 2) * 2 - 1) * spawnDistance);
        var temp = Instantiate(impactObject, _spawnPosition, Quaternion.identity, null);

        var aux = Random.Range(0, maxZDistance) * (Random.Range(0, 2) * 2 - 1);
        position = new Vector3(position.x, position.y, position.z + aux);
        _spawnPosition = position;

        var tempImpact = temp.GetComponent<ImpactBehaviour>();
        tempImpact.SetPlayerController(playerController);
    }
}
