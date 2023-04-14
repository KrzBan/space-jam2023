using UnityEngine;
using System.Collections;

public class ConsumableSpawner : MonoBehaviour {

    public GameObject consumablePrefab; // the object to be spawned
    public float spawnTime = 5f; // the time interval between spawns

    void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn () {
        Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);

        int min = 0;
        int max = 1;
        int randomInt = UnityEngine.Random.Range(min, max); // generates a random integer between min (inclusive) and max (exclusive)

        GameObject newConsumable = GameObject.Instantiate(consumablePrefab, randomPosition, Quaternion.identity);
        Consumable consumableComponent = newConsumable.GetComponent<Consumable>();
        consumableComponent.SetRandomConsumableType();


    }
}