using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Plateforme_mouvement : MonoBehaviour
{
    int columnNumber;
    bool[] enabledFingers;
    float[] platformPositions;
    public int platform_pos;
    // Start is called before the first frame update
    void Start()
    {
        columnNumber = GameObject.Find("GameManager").GetComponent<NoteGenerator>().columnNumber;
        transform.localScale = new Vector3(3 * 5 / columnNumber, 0.3f, 0);
        enabledFingers = new bool[5] { false, false, false, false, false };
        platformPositions = new float[5] { 0, 0, 0, 0, 0 };
        for (int i = 0; i < columnNumber; i++)
        {
            enabledFingers[i] = true;
            platformPositions[i] = (i + 0.5f) * (19.2f / columnNumber) - 9.6f;
        }
        //platform_pos = Convert.ToInt32(Math.Floor((double)columnNumber / 2));
        platform_pos = 0;
        transform.position = new Vector3(platformPositions[platform_pos], -3, 0);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            transform.position += new Vector3(.1f * columnNumber, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.position -= new Vector3(.1f * columnNumber, 0, 0);
        }
        if (((float)Manipulandum_data_aquired.Force_Data[0] > 1 || Input.GetKeyDown("a")) && enabledFingers[0])
        {
            mouvement(transform.position, new Vector3(platformPositions[0], -3, 0));
            platform_pos = 0;
        }
        else if (((float)Manipulandum_data_aquired.Force_Data[1] > 1 || Input.GetKeyDown("z")) && enabledFingers[1])
        {
            mouvement(transform.position, new Vector3(platformPositions[1], -3, 0));
            platform_pos = 1;
        }
        else if (((float)Manipulandum_data_aquired.Force_Data[2] > 1 || Input.GetKeyDown("e")) && enabledFingers[2])
        {
            mouvement(transform.position, new Vector3(platformPositions[2], -3, 0));
            platform_pos = 2;
        }
        else if (((float)Manipulandum_data_aquired.Force_Data[3] < -1 || Input.GetKeyDown("r")) && enabledFingers[3])
        {
            mouvement(transform.position, new Vector3(platformPositions[3], -3, 0));
            platform_pos = 3;
        }
        else if (((float)Manipulandum_data_aquired.Force_Data[4] < -1 || Input.GetKeyDown("b")) && enabledFingers[4])
        {
            mouvement(transform.position, new Vector3(platformPositions[4], -3, 0));
            platform_pos = 4;
        }
    }
    void mouvement(Vector3 origin, Vector3 pos)
    {
        transform.position = pos;
        /*StartCoroutine(moveObject(origin, pos));*/
        /*transform.position = Vector3.MoveTowards(transform.position, pos, .5f);*/
        /*transform.position = Vector3.Lerp(transform.position, pos, .1f);*/
    }
    public IEnumerator moveObject(Vector3 origin, Vector3 pos)
    {
        float totalMovementTime = .2f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (Vector3.Distance(transform.localPosition, pos) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(origin, pos, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }
}
