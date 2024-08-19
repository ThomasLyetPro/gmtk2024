using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCellSpawner : MonoBehaviour
{
    [SerializeField] GameObject redCellPrefab;
    [SerializeField] Transform target;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float delayBetweenSpawn = 1f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            var redCell = Instantiate(redCellPrefab, transform);

            spawnPoint.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));
            redCell.transform.position = spawnPoint.position;

            redCell.GetComponent<RedCell>().SetDestination(target);
            yield return new WaitForSeconds(delayBetweenSpawn);
        }
    }
}
