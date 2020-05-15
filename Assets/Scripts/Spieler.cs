using UnityEngine;
using UnityEngine.UI;

public class Spieler : MonoBehaviour
{
    public GameObject gewinn;
    float eingabeFaktor = 10;
    int anzahlPunkte;
    public Text punkteAnzeige;
    int anzahlLeben = 3;
    public Text lebenAnzeige;
    float zeitStart;
    bool spielGestartet = false;
    public Text zeitAnzeige;
    public Text zeitAltAnzeige;
    public Text infoAnzeige;

    // Methode verarbeitet Kollisionen mit anderen Spielobjekten
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Gewinn")
        {
            ++anzahlPunkte;
            if (anzahlPunkte < 6)
            {
                punkteAnzeige.text = "Punkte: " + anzahlPunkte;
                if (anzahlPunkte == 1)
                {
                    infoAnzeige.text = "Du hast bereits einen Punkt!";
                }
                else
                {
                    infoAnzeige.text = "Du hast bereits " + anzahlPunkte + " Punkte!";
                    Invoke("NaechsterGewinn", 2);
                }
            }
            else
            {
                gameObject.SetActive(false);
                gewinn.SetActive(false);
                punkteAnzeige.text = "Gewonnen";
                infoAnzeige.text = "Du hast gewonnen!";
                PlayerPrefs.SetFloat("zeitAlt", Time.time - zeitStart);
                PlayerPrefs.Save();
            }

            float xNeu = Random.Range(-8.0f, 8.0f);
            // Aenderung der Höhe je nach Punktestand
            float yNeu;
            if (anzahlPunkte < 2)
            { yNeu = -2.7f; }
            else if (anzahlPunkte < 4)
            { yNeu = 0.15f; }
            else { yNeu = 3; }
            gewinn.transform.position = new Vector3(xNeu, yNeu, 0);
        }
        else if (coll.gameObject.tag == "Gefahr")
        {
            --anzahlLeben;
            lebenAnzeige.text = "Leben: " + anzahlLeben;
            gameObject.SetActive(false);
            if (anzahlLeben > 0)
            {
                infoAnzeige.text = "Du hast nur noch " + anzahlLeben + " Leben!";
                Invoke("NaechstesLeben", 2);
            }
            else
            {
                gewinn.SetActive(false);
                lebenAnzeige.text = "Verloren";
                infoAnzeige.text = "Du hast verloren!";
            }
        }

    }

    void NaechstesLeben()
    {
        transform.position = new Vector3(0, -4.4f, 0);
        gameObject.SetActive(true);
        infoAnzeige.text = "";
    }

    void NaechsterGewinn()
    {
        float xNeu = Random.Range(-8.0f, 8.0f);
        // Aenderung der Höhe je nach Punktestand
        float yNeu;
        if (anzahlPunkte < 2)
        { yNeu = -2.7f; }
        else if (anzahlPunkte < 4)
        { yNeu = 0.15f; }
        else { yNeu = 3; }
        gewinn.transform.position = new Vector3(xNeu, yNeu, 0);
        gewinn.SetActive(true);
        infoAnzeige.text = "";
    }
    void Start()
    {
        float zeitAlt = 0;
        if (PlayerPrefs.HasKey("zeitAlt"))
        {
            zeitAlt = PlayerPrefs.GetFloat("zeitAlt");
            zeitAltAnzeige.text = string.Format("Alt: {0,6:0.0}" + " sec.", zeitAlt);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /* Eingabe speichern */
        float xEingabe = Input.GetAxis("Horizontal");
        float yEingabe = Input.GetAxis("Vertical");
        if (yEingabe < 0)
        {
            return;
        }

        // neue Position bestimmen
        float xNeu = transform.position.x + xEingabe * eingabeFaktor * Time.deltaTime;
        if (xNeu > 8.3f)
        {
            xNeu = 8.3f;
        }
        if (xNeu < -8.3f)
        {
            xNeu = -8.3f;
        }
        float yNeu = transform.position.y + yEingabe + eingabeFaktor * Time.deltaTime;
        transform.position = new Vector3(xNeu, yNeu, 0);


        if (!spielGestartet && (xEingabe != 0 || yEingabe != 0))
        {
            spielGestartet = true;
            zeitStart = Time.time;
            infoAnzeige.text = "";
        }
        if (spielGestartet)
        {
            zeitAnzeige.text = string.Format("Zeit: {0,6:0.0} " + "sec.", Time.time - zeitStart);
        }
    }
}
