using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class sounds : MonoBehaviour
{
    AudioSource C5;
    AudioSource D5;
    AudioSource E5;
    AudioSource F5;
    AudioSource G5;
    AudioSource[] notes;
    AudioSource[] notesOctave;
    int[] nextNote;
    GameObject[] NextNote;
    int columnNumber;
    public int noteCounter;
    int noteCounterSave;
    const float FADE_TIME_SECONDS = 5;

    // Start is called before the first frame update
    void Start()
    {
        columnNumber = GameObject.Find("GameManager").GetComponent<NoteGenerator>().columnNumber;
        noteCounter = 0;
        noteCounterSave = 0;
        nextNote = GameObject.Find("GameManager").GetComponent<NoteGenerator>().nextNote;
        NextNote = GameObject.Find("GameManager").GetComponent<NoteGenerator>().notes;
        notes = GameObject.Find("GameManager").GetComponents<AudioSource>();
        notesOctave = GameObject.Find("Octave").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float Y = Math.Abs((nextNote[noteCounter] + 0.5f) * (19.2f / columnNumber) - 9.6f - transform.position.x);
        if (noteCounter == noteCounterSave)
        {
            noteCounterSave++;
            PutSound(nextNote[noteCounter], -0.005 * Y + 0.080);
            PutSound(nextNote[noteCounter], 0, true);
        }
        if (Y < 1.5 * 5 / columnNumber)
        {
            PutVolume(nextNote[noteCounter], -0.005 * Y + 0.080, true);
        }
        else
        {
            PutVolume(nextNote[noteCounter], 0, true);
        }
        PutVolume(nextNote[noteCounter], -0.005 * Y + 0.080);
    }

    void PutSound(int noteIndex, double Volume, bool octave = false)
    {
        for (int i = 0; i < notes.Length; i++)
        {
            if (octave)
            {
                if (i == noteIndex)
                {
                    notesOctave[i].time = 0f;
                    notesOctave[i].volume = (float)Volume;
                    notesOctave[i].Play();
                }
                else
                {
                    notesOctave[i].Stop();
                    notesOctave[i].time = 0f;
                }
            }
            else
            {
                if (i == noteIndex)
                {
                    notes[i].time = 0f;
                    notes[i].volume = (float)Volume;
                    notes[i].Play();
                }
                else
                {
                    notes[i].Stop();
                    notes[i].time = 0f;
                }
            }            
        }
    }
    void PutVolume(int noteIndex, double Volume, bool octave = false)
    {
        for (int i = 0; i < notes.Length; i++)
        {
            if (i == noteIndex)
            {
                //StartCoroutine(FadeIn(i, (float)Volume, octave));
                //StartCoroutine(StartFade(i, .1f, (float)Volume, octave));
                notes[i].volume = (float)Volume;
            }
        }
    }
    public IEnumerator StartFade(int noteIndex, float duration, float targetVolume, bool octave)
    {
        float currentTime = 0;
        float start = notes[noteIndex].volume;
        float startOctave = notesOctave[noteIndex].volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (octave)
            {
                notesOctave[noteIndex].volume = Mathf.Lerp(startOctave, targetVolume, currentTime / duration);
            }
            else
            {
                notes[noteIndex].volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            }            
            yield return null;
        }
        yield break;
    }
    IEnumerator FadeIn(int noteIndex, float targetVolume, bool octave)
    {
        float timeElapsed = 0;

        if (octave)
        {
            while (notes[noteIndex].volume < targetVolume)
            {
                notes[noteIndex].volume = Mathf.Lerp(0, 1, timeElapsed / FADE_TIME_SECONDS);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (notesOctave[noteIndex].volume < targetVolume)
            {
                notesOctave[noteIndex].volume = Mathf.Lerp(0, 1, timeElapsed / FADE_TIME_SECONDS);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }        
    }
    IEnumerator FadeOut(int noteIndex, float targetVolume, bool octave, float delay)
    {
        yield return new WaitForSeconds(delay);
        float timeElapsed = 0;
        
        if (octave)
        {
            while (notesOctave[noteIndex].volume > targetVolume)
            {
                notesOctave[noteIndex].volume = Mathf.Lerp(1, 0, timeElapsed / FADE_TIME_SECONDS);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (notes[noteIndex].volume > targetVolume)
            {
                notes[noteIndex].volume = Mathf.Lerp(1, 0, timeElapsed / FADE_TIME_SECONDS);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
