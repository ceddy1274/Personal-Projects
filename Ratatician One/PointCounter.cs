using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    [SerializeField] Point point;

    private void Start () {
        StartCoroutine("CountPoints");
    }
    private IEnumerator CountPoints () {
        while(true) {
            point.Points = EnemyBehavior.score;

            yield return new WaitForSeconds(1);
        }
    }
}
