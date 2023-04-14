using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType 
{
    Health,
    Shield
}

public class Consumable : MonoBehaviour
{
    Transform player;
    public float speed = 2f;
    public float pickupDistance = 0.5f;
    public float despawnTime = 10f;

    public ConsumableType consumableType;


    private void Awake()
    {
        player = GameManager.instance.playerSpaceship.transform;
        consumableType = ConsumableType.Health;
    }

    void Update()
    {
        despawnTime -= Time.deltaTime;
        // if(despawnTime < 0){
        //     Destroy(gameObject);
        // }
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickupDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if(distance < 0.1f)
        {
            switch (consumableType)
            {
                case ConsumableType.Health:
                    Debug.Log("Health");
                    break;
                default:
                    break;
            }
            
            Destroy(gameObject);
        }
    }

    public void SetRandomConsumableType() {
        ConsumableType[] consumableTypes = (ConsumableType[])System.Enum.GetValues(typeof(ConsumableType));
        ConsumableType randomConsumableType = consumableTypes[Random.Range(0, consumableTypes.Length)];
        consumableType = randomConsumableType;
        Debug.Log(randomConsumableType);
    }
}
