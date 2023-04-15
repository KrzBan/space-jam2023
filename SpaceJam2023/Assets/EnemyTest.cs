using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    private Material mat;

    [Range(-1.0f, 1.0f)]
    public float cutoffPoint = 1.0f;

    private void Awake() {
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update() {
        mat.SetFloat("_Cutoff_Height", cutoffPoint);
    }
}
