using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<UnitMover> templates;
    [SerializeField] private int spawnSize;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int spawnPositionRange;
    
    private List<UnitMover> spawnedUnits = new List<UnitMover>();
    private float timeToSpawn;
    
    private void Start()
    {
        InitializeSpawn();
    }

    private void Update()
    {
        ActiveSelfChecker();
    }

    private void InitializeSpawn()
    {
        for (var i = 0; i < spawnSize; i++)
        {
            var newUnit = Instantiate(GetRandomUnit(), RandomPosition(), Quaternion.identity);
            newUnit.gameObject.SetActive(false);
            spawnedUnits.Add(newUnit);
        }
    }

    private void ActiveSelfChecker()
    {
        timeToSpawn += Time.deltaTime;

        foreach (var unit in spawnedUnits)
        {
            if (!unit.gameObject.activeSelf && timeToSpawn >= spawnDelay)
            {
                unit.transform.position = RandomPosition();
                unit.gameObject.SetActive(true);
                timeToSpawn = 0f;
            }
        }
    }

    private UnitMover GetRandomUnit()
    {
        return templates[Random.Range(0, templates.Count)];
    }
    
    private Vector2 RandomPosition()
    {
        return new Vector2(transform.position.x, Random.Range(-spawnPositionRange, spawnPositionRange));
    }
}
