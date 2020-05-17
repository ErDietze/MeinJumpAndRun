using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gefahren : MonoBehaviour
{
    float xStart;
    float xAenderung;
    // Start is called before the first frame update
    void Start()
    {
        xStart = transform.position.x;
        xAenderung = Random.Range(1.0f, 2.0f) * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        float xNeu = transform.position.x + xAenderung;
        transform.position = new Vector3(xNeu, transform.position.y, 0);
        if ((xNeu > xStart + 2) || (xNeu < xStart - 2))
        {
            xAenderung = -1 * xAenderung;
        }
    }
}
