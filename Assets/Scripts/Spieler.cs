using UnityEngine;

public class Spieler : MonoBehaviour
{
    float eingabeFaktor = 10;

    // Update is called once per frame
    void Update()
    {
        /* Eingabe speichern */
        float xEingabe = Input.GetAxis("Horizontal");
        float yEingabe = Input.GetAxis("Vertical");

        // neue Position bestimmen
        float xNeu = transform.position.x + xEingabe * eingabeFaktor * Time.deltaTime;
        float yNeu = transform.position.y + yEingabe + eingabeFaktor * Time.deltaTime;
        transform.position = new Vector3(xNeu, yNeu, 0);
    }
}
