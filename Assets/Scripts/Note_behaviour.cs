using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_behaviour : MonoBehaviour
{
    AudioSource pianoSound;
    bool plateformeTouched = false;
    int columnNumber;
    bool touched;
    // Start is called before the first frame update
    void Start()
    {
        touched = true;
        columnNumber = GameObject.Find("GameManager").GetComponent<NoteGenerator>().columnNumber;
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        pianoSound.time = 0f;
    }

    private void Update()
    {
        if (!plateformeTouched)
        {
            this.gameObject.transform.position -= new Vector3(0, 0.015f * columnNumber, 0); //0.32f Si t'es chaud.
        }
    }
    public void PlayForTime(float time)
    {
        pianoSound.Play();
        Invoke("StopAudio", time);
    }

    private void StopAudio()
    {
        /*pianoSound.Stop();*/
        this.gameObject.SetActive(false);

    }

    void OnCollisionEnter2D(Collision2D infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.tag == "Plateforme" && touched)
        {
            touched = false;
            PlayForTime(0.75f);
            plateformeTouched = true;
            GameObject.Find("Plateforme").GetComponent<sounds>().noteCounter++;
        }
        if (infoCollision.gameObject.tag == "Plateforme_line" && touched)
        {
            touched = false;
            GameObject.Find("Plateforme_line").GetComponent<AudioSource>().volume = 0.1f;
            GameObject.Find("Plateforme_line").GetComponent<AudioSource>().Play();
            GameObject.Find("Plateforme").GetComponent<sounds>().noteCounter++;
        }
    }
}
