using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControllerGame : MonoBehaviour
{
    [Header("Archivos TXT")]
    public TextAsset multipleTXT;
    public TextAsset trueFalseTXT;
    public TextAsset openTXT;

    List<MultipleQuestion> multipleList = new List<MultipleQuestion>();
    List<TrueFalseQuestion> trueFalseList = new List<TrueFalseQuestion>();
    List<OpenQuestion> openList = new List<OpenQuestion>();

    MultipleQuestion preguntaMultipleActual;
    TrueFalseQuestion preguntaTFActual;
    OpenQuestion preguntaOpenActual;

    [Header("Textos Preguntas")]
    public TMP_Text textPreguntaMultiple;
    public TMP_Text textPreguntaTF;
    public TMP_Text textPreguntaOpen;

    [Header("Paneles")]
    public GameObject panelOpen;
    public GameObject panelMultiple;
    public GameObject panelTrueFalse;
    public GameObject panelResult;

    [Header("Textos Generales")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI difficultyText;

    [Header("Botón Siguiente")]
    public Button nextQuestionButton;

    [Header("Botones Multiple")]
    public GameObject buttonOP1;
    public GameObject buttonOP2;
    public GameObject buttonOP3;
    public GameObject buttonOP4;

    [Header("Dificultad")]
    public TMP_Text textDificultadMultiple;

    [Header("True / False")]
    public TMP_Text textDificultadTF;
    public GameObject buttonVerdadero;
    public GameObject buttonFalso;

    [Header("Open Question")]
    public TMP_Text textDificultadOpen;
    public TMP_InputField inputRespuestaOpen;

    [Header("Resultado")]
    public GameObject panelResultado;
    public TMP_Text textResultado;
    public TMP_Text textVersiculo;


    void Start()
    {
        CargarMultiple();
        CargarTrueFalse();
        CargarOpen();
        ElegirPreguntaAleatoria();
        nextQuestionButton.gameObject.SetActive(false);

        Debug.Log("Multiple: " + multipleList.Count);
        Debug.Log("TrueFalse: " + trueFalseList.Count);
        Debug.Log("Open: " + openList.Count);
    }


    public void ActivarPanel(string tipo)
    {
        panelOpen.SetActive(false);
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);

        if (tipo == "Open")
            panelOpen.SetActive(true);
        else if (tipo == "Multiple")
            panelMultiple.SetActive(true);
        else if (tipo == "TrueFalse")
            panelTrueFalse.SetActive(true);
    }

    void CargarMultiple()
    {
        string[] lineas = multipleTXT.text.Split('\n');

        foreach (string linea in lineas)
        {
            if (linea.Trim() == "") continue;

            string[] datos = linea.Trim().Split('-');

            if (datos.Length < 8)
            {
                Debug.LogWarning("Línea mal formateada: " + linea);
                continue;
            }

            MultipleQuestion pregunta = new MultipleQuestion(
                datos[0].Trim(),
                datos[1].Trim(),
                datos[2].Trim(),
                datos[3].Trim(),
                datos[4].Trim(),
                datos[5].Trim(),
                datos[6].Trim(),
                datos[7].Trim()
            );

            multipleList.Add(pregunta);
        }
    }

    void CargarTrueFalse()
    {
        string[] lineas = trueFalseTXT.text.Split('\n');

        foreach (string linea in lineas)
        {
            if (linea.Trim() == "") continue;

            string[] datos = linea.Split('-');

            TrueFalseQuestion pregunta = new TrueFalseQuestion(
                datos[0],
                datos[1],
                datos[2],
                datos[3]
            );

            trueFalseList.Add(pregunta);
        }
    }
    void CargarOpen()
    {
        string[] lineas = openTXT.text.Split('\n');

        foreach (string linea in lineas)
        {
            if (linea.Trim() == "") continue;

            string[] datos = linea.Split('-');

            OpenQuestion pregunta = new OpenQuestion(
                datos[0],
                datos[1],
                datos[2],
                datos[3]
            );

            openList.Add(pregunta);
        }
    }

    void ElegirPreguntaAleatoria()
    {
        int totalPreguntas = multipleList.Count + trueFalseList.Count + openList.Count;

        int randomIndex = Random.Range(0, totalPreguntas);

        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);

        if (randomIndex < multipleList.Count)
        {
            preguntaMultipleActual = multipleList[randomIndex];

            panelMultiple.SetActive(true);

            textPreguntaMultiple.text = preguntaMultipleActual.Question;

            buttonOP1.GetComponentInChildren<TMP_Text>().text = preguntaMultipleActual.Option1;
            buttonOP2.GetComponentInChildren<TMP_Text>().text = preguntaMultipleActual.Option2;
            buttonOP3.GetComponentInChildren<TMP_Text>().text = preguntaMultipleActual.Option3;
            buttonOP4.GetComponentInChildren<TMP_Text>().text = preguntaMultipleActual.Option4;

            textDificultadMultiple.text = "Dificultad: " + preguntaMultipleActual.Dificultty;


        }
        else if (randomIndex < multipleList.Count + trueFalseList.Count)
        {
            int index = randomIndex - multipleList.Count;

            preguntaTFActual = trueFalseList[index];

            panelTrueFalse.SetActive(true);

            textPreguntaTF.text = preguntaTFActual.question;
            textDificultadTF.text = "Dificultad: " + preguntaTFActual.difficulty;

            buttonVerdadero.GetComponentInChildren<TMP_Text>().text = "Verdadero";
            buttonFalso.GetComponentInChildren<TMP_Text>().text = "Falso";
        }

        else
        {
            int index = randomIndex - multipleList.Count - trueFalseList.Count;
            preguntaOpenActual = openList[index];

            panelOpen.SetActive(true);

            textPreguntaOpen.text = preguntaOpenActual.question;
            textDificultadOpen.text = "Dificultad: " + preguntaOpenActual.difficulty;

            inputRespuestaOpen.text = ""; // limpiar campo
        }

    }

    public void ValidarMultiple(int numeroOpcion)
    {
        string respuestaJugador = "";

        if (numeroOpcion == 1)
            respuestaJugador = preguntaMultipleActual.Option1;
        else if (numeroOpcion == 2)
            respuestaJugador = preguntaMultipleActual.Option2;
        else if (numeroOpcion == 3)
            respuestaJugador = preguntaMultipleActual.Option3;
        else if (numeroOpcion == 4)
            respuestaJugador = preguntaMultipleActual.Option4;

        panelResultado.SetActive(true);

        if (respuestaJugador == preguntaMultipleActual.Answer)
        {
            textResultado.text = "Correcto";
        }
        else
        {
            textResultado.text = "Incorrecto";
        }

        textVersiculo.text = "Justificación: " + preguntaMultipleActual.Versiculo;

        nextQuestionButton.gameObject.SetActive(true);


    }

    public void ValidarTrueFalse(bool respuestaJugador)
    {
        panelResultado.SetActive(true);

        bool respuestaCorrecta = preguntaTFActual.answer.ToLower() == "verdadero";

        if (respuestaJugador == respuestaCorrecta)
        {
            textResultado.text = "Correcto";
        }
        else
        {
            textResultado.text = "Incorrecto";
        }

        textVersiculo.text = "Justificación: " + preguntaTFActual.versiculo;

        nextQuestionButton.gameObject.SetActive(true);
    }
    public void ValidarOpen()
    {
        if (preguntaOpenActual == null)
            return;

        panelResultado.SetActive(true);

        string respuestaJugador = inputRespuestaOpen.text.Trim().ToLower();
        string respuestaCorrecta = preguntaOpenActual.answer.Trim().ToLower();

        if (respuestaJugador == respuestaCorrecta)
        {
            textResultado.text = "Correcto";
        }
        else
        {
            textResultado.text = "Incorrecto";
        }

        textVersiculo.text = "Justificación: " + preguntaOpenActual.versiculo;

        nextQuestionButton.gameObject.SetActive(true);
    }
    public void SiguientePregunta()
    {
    
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);

  
        nextQuestionButton.gameObject.SetActive(false);


        panelResultado.SetActive(false);

        ElegirPreguntaAleatoria();
    }

}
