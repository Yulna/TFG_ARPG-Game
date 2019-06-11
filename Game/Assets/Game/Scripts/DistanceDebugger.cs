using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDebugger : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 3);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 4);

    }
}
