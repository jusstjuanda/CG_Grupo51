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

    [Header("Textos Pregunta")]
    public TMP_Text textPregunta;

    [Header("Paneles")]
    public GameObject panelOpen;
    public GameObject panelMultiple;
    public GameObject panelTrueFalse;
    public GameObject panelQuestion;

    [Header("Botón Siguiente")]
    public Button nextQuestionButton;

    [Header("Botones Multiple")]
    public TMP_Text buttonOP1;
    public TMP_Text buttonOP2;
    public TMP_Text buttonOP3;
    public TMP_Text buttonOP4;

    [Header("Dificultad")]
    public TMP_Text textDificultad;

    [Header("True / False")]
    public TMP_Text buttonVerdadero;
    public TMP_Text buttonFalso;

    [Header("Open Question")]
    public TMP_InputField inputRespuestaOpen;

    [Header("Resultado")]
    public GameObject panelResultado;
    public TMP_Text textResultado;
    public TMP_Text textVersiculo;
    public string dificultadActual = "facil";
    void Start()
    {
        CargarMultiple();
        CargarTrueFalse();
        CargarOpen();
        ElegirPreguntaAleatoria();
        nextQuestionButton.gameObject.SetActive(false); 
    }


    public void ActivarPanel(string tipo)
    {
        panelOpen.SetActive(false);
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelQuestion.SetActive(false);

        if (tipo == "Open")
        {
            panelOpen.SetActive(true);
            panelQuestion.SetActive(true);
        }   
        else if (tipo == "Multiple")
        {
            panelMultiple.SetActive(true);
            panelQuestion.SetActive(true);
        }
        else if (tipo == "TrueFalse")
        {
            panelTrueFalse.SetActive(true);
            panelQuestion.SetActive(true);
        }
            
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

    bool QuedanPreguntasFaciles()
    {
        foreach (var p in multipleList)
            if (p.Dificultty.ToLower() == "facil") return true;

        foreach (var p in trueFalseList)
            if (p.difficulty.ToLower() == "facil") return true;

        foreach (var p in openList)
            if (p.difficulty.ToLower() == "facil") return true;

        return false;
    }

    void ElegirPreguntaAleatoria()
    {
        if (dificultadActual=="facil" && !QuedanPreguntasFaciles())
        {
            dificultadActual = "dificl";
            Debug.Log("⚠️ A partir de ahora se mostrarán preguntas DIFÍCILES");
        }

        int totalPreguntas = multipleList.Count + trueFalseList.Count + openList.Count;

        int randomIndex = Random.Range(0, totalPreguntas);

        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);
        panelQuestion.SetActive(false);

        if (randomIndex < multipleList.Count)
        {
            preguntaMultipleActual = multipleList[randomIndex];

            panelMultiple.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaMultipleActual.Question;

            buttonOP1.text = preguntaMultipleActual.Option1;
            buttonOP2.text = preguntaMultipleActual.Option2;
            buttonOP3.text = preguntaMultipleActual.Option3;
            buttonOP4.text = preguntaMultipleActual.Option4;

            textDificultad.text = "Dificultad: " + preguntaMultipleActual.Dificultty;


        }
        else if (randomIndex < multipleList.Count + trueFalseList.Count)
        {
            int index = randomIndex - multipleList.Count;

            preguntaTFActual = trueFalseList[index];

            panelTrueFalse.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaTFActual.question;
            textDificultad.text = "Dificultad: " + preguntaTFActual.difficulty;

            buttonVerdadero.text = "Verdadero";
            buttonFalso.text = "Falso";
        }

        else
        {
            int index = randomIndex - multipleList.Count - trueFalseList.Count;
            preguntaOpenActual = openList[index];

            panelOpen.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaOpenActual.question;
            textDificultad.text = "Dificultad: " + preguntaOpenActual.difficulty;

            inputRespuestaOpen.text = "";
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

        panelQuestion.SetActive(false);
        panelResultado.SetActive(true);

        if (respuestaJugador.Equals(preguntaMultipleActual.Answer))
        {
            textResultado.text = "Correcto";
        }
        else
        {
            textResultado.text = "Incorrecto \nLa respuesta correcta era: " +preguntaMultipleActual.Answer;
        }

        textVersiculo.text = "Justificación: " + preguntaMultipleActual.Versiculo;

        nextQuestionButton.gameObject.SetActive(true);


    }

    public void ValidarTrueFalse(bool respuestaJugador)
    {
        panelQuestion.SetActive(false);
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
        panelQuestion.SetActive(false);
        panelResultado.SetActive(true);

        string respuestaJugador = inputRespuestaOpen.text;
        string respuestaCorrecta = preguntaOpenActual.answer;

        if (respuestaJugador.Equals(respuestaCorrecta))
        {
            textResultado.text = "Correcto";
        }
        else
        {
            textResultado.text = "Incorrecto \nLa respuesta Correcta era: " + respuestaCorrecta ;
        }

        textVersiculo.text = "Justificación: " + preguntaOpenActual.versiculo;

        nextQuestionButton.gameObject.SetActive(true);
    }
    public void SiguientePregunta()
    {
    
        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);
        panelQuestion.SetActive(false);

  
        nextQuestionButton.gameObject.SetActive(false);


        panelResultado.SetActive(false);

        ElegirPreguntaAleatoria();
    }

}
