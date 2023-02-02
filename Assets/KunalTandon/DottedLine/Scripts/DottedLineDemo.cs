using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineDemo : MonoBehaviour
{
    int columnNumber;

    private void Start()
    {
        columnNumber = GameObject.Find("GameManager").GetComponent<NoteGenerator>().columnNumber;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <= columnNumber; i++)
        {
            Vector2 X = new Vector2(-9.6f + i * 19.2f / columnNumber, 5);
            Vector2 Y = new Vector2(-9.6f + i * 19.2f / columnNumber, -3.5f);
            DottedLine.DottedLine.Instance.DrawDottedLine(X, Y);
        }
    }
}
