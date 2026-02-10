using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    List<MultipleQuestion> multipleQuestions = new List<MultipleQuestion>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadMultipleQuestions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadMultipleQuestions()
    {

    }
}
