using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public float rotSpeed = 1.0f;

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
}
