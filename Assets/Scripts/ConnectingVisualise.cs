﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectingVisualise : MonoBehaviour
{
    Color colorStart = Color.white;
    Color colorEnd = Color.black;
    float duration = 1.0f;
    Renderer rend;
    public float period = 0.0f;
    float timePassed = 0;
    public float time = 0;
    public bool isRunning = false;
    public bool connected = false;
    public bool power = false;

    void Start()
    {
        if (!isRunning)
        {
            rend = GetComponent<Renderer>();
            timePassed = 0;
            StartCoroutine(ConnectingSequance());       
        }
    }

    void Update()
    {
        CheckTextStatus();
    }

    //Main animation loop for changing colours
    IEnumerator ConnectingSequance()
    {
        time = Random.Range(3, 8);
        Debug.Log(time);

        if (connected)
        {
            StartCoroutine(AlreadyConnectedAnimation());
            yield break;
        }

        isRunning = true;
        while (timePassed < time && isRunning)
        {

            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);

            timePassed += Time.deltaTime;

            yield return null;
        }
        //Added a 75% chance of connecting (simulates a possible connection error)
        connected = (Random.value < 0.75f);
        isRunning = false;
        Debug.Log(connected);
        CheckTextStatus();
        if (connected)
        {
            rend.material.color = Color.green;
        }
        else
            rend.material.color = Color.red;
    }

    void OnEnable()
    {
        Start();
    }

    void OnDisable()
    {
        if (connected)
        {
            Start();
        }
        else
        {
            rend.material.color = Color.gray;
            connected = false;
        }
    }

    IEnumerator AlreadyConnectedAnimation()
    {
        rend.material.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = Color.grey;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = Color.grey;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = Color.green;
    }

    void CheckTextStatus()
    {
        string statusText;
        GameObject textBox = GameObject.Find("StatusTextComponent");
        if (textBox != null)
        {
           
            if (isRunning && !connected)
            {
                statusText = "Connecting";
            }
            else if (connected)
            {
                statusText = "Connected";
            }
            else if (!connected)
            {
                statusText = "No connection/No power";
            }
            else
                statusText = "No power";

            textBox.GetComponent<TextMeshPro>().text = statusText;
            Debug.Log(textBox.GetComponent<TextMeshPro>().text);
        }
        
    }

    /*void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
    }*/
}
