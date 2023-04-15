using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour {

    public float x = 1;
    public float y = 1;

    public Vector3 RestrictPosition(Vector3 position) {
        var pos = transform.position;
        return new Vector3(
            Mathf.Clamp(position.x, pos.x - x / 2, pos.x + x / 2),
            Mathf.Clamp(position.y, pos.y - y / 2, pos.y + y / 2),
            position.z );
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(x, y, 0));
    }
}
