using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ImpactManager : MonoBehaviour
{
    private Vector3 _spawnPosition;

    [SerializeField] [Range(0, 500)] private float spawnDistance;
    [SerializeField] [Range(0, 500)] private float maxZDistance;
    [SerializeField] private GameObject impactObject;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private PlayerController playerController;


    [SerializeField]
    int baseTimedelay = 10;
    [SerializeField]
    int delayFluctoation = 3;

    [SerializeField]
    private List<GameObject> impactObjects;

    void Start()
    {
        StartCoroutine(startSpawning());
    }


    void Update()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    SpawnImpact();
        //}
    }

    private void SpawnImpact()
    {
        var position = playerPosition.position + playerPosition.right * ((Random.Range(0, 2) * 2 - 1) * spawnDistance);

        GameObject o = impactObjects[Random.Range(0, impactObjects.Count - 1)];

        var temp = Instantiate(o, _spawnPosition, Quaternion.identity, null);

        var aux = Random.Range(0, maxZDistance) * (Random.Range(0, 2) * 2 - 1);
        position = new Vector3(position.x, position.y, position.z + aux);
        _spawnPosition = position;

        var tempImpact = temp.GetComponent<ImpactBehaviour>();
        tempImpact.SetPlayerController(playerController);
    }

    private IEnumerator TimedSpawn()
    {
        while (true) {

            SpawnImpact();


            int extraTime = Random.Range(-delayFluctoation, delayFluctoation + 1);

            yield return new WaitForSeconds(baseTimedelay + extraTime);
        }
    }

    private IEnumerator startSpawning()
    {
        int extraTime = Random.Range(-delayFluctoation, delayFluctoation + 1);

        yield return new WaitForSeconds(baseTimedelay + extraTime);


        StartCoroutine(TimedSpawn());
    }
}
