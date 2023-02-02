using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteGenerator : MonoBehaviour
{
    public GameObject note;
    public GameObject _do;
    public GameObject _re;
    public GameObject _mi;
    public GameObject _fa;
    public GameObject _sol;
    public int noteNumber;
    public GameObject[] notes;
    public int columnNumber;
    public int[] nextNote;
    // Start is called before the first frame update
    void Start()
    {
        notes = new GameObject[5] { _do, _re, _fa, _mi, _sol };
        float positionX = 0;
        float positionY = 5.5f;
        Vector3 position;
        int rand = 0;
        nextNote = new int[noteNumber];
        System.Random aleatoire = new System.Random();
        for(int i = 0; i < noteNumber; i++)
        {
            rand = aleatoire.Next(columnNumber);
            nextNote[i] = rand;
            positionX = ((float)rand + 0.5f) * (19.2f / columnNumber) - 9.6f; //(float)aleatoire.Next(columnNumber) * 3.84f - 7.68f; 
            position = new Vector3(positionX, positionY, 0);
            SpawnNote(position, notes[rand]);
            positionY += 7.5f;
        }
    }

    void SpawnNote(Vector3 pos, GameObject Note)
    {
        GameObject newNote = Instantiate(Note, pos, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
