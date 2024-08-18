using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCellSpawner : MonoBehaviour
{
    [SerializeField] GameObject redCellPrefab;
    [SerializeField] Transform target;
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
            redCell.GetComponent<RedCell>().SetDestination(target);
            yield return new WaitForSeconds(delayBetweenSpawn);
        }
    }
}
