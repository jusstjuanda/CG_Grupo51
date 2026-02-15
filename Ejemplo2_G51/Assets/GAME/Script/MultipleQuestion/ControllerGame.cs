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
    public GameObject panelIncorrecto;
    public GameObject panelCorrecto;
    public GameObject panelDificil;
    public GameObject panelTerminado;

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
    public string dificultadActual = "Facil";

    [Header("Puntos")]
    public int puntos = 0;
    public TMP_Text textpuntos;
    public TMP_Text textfinal;

    [Header("Audios")]
    public AudioSource WahWahWah;
    public AudioSource Yei;
    public AudioSource MusicaFacil;
    public AudioSource MusicaDificil;
    public AudioSource outroCoscu;
    public AudioSource goku;
    void Start()
    {
        CargarMultiple();
        CargarTrueFalse();
        CargarOpen();
        ElegirPreguntaAleatoria();
        nextQuestionButton.gameObject.SetActive(false);
        MusicaFacil.Play();
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

    bool QuedanPreguntasPorDificultad(string dificultad)
    {
        dificultad = dificultad.Trim().ToLower();

        foreach (var p in multipleList)
            if (p.Dificultty.Trim().ToLower().Equals(dificultad))
                return true;

        foreach (var p in trueFalseList)
            if (p.difficulty.Trim().ToLower().Equals(dificultad))
                return true;

        foreach (var p in openList)
            if (p.difficulty.Trim().ToLower().Equals(dificultad))
                return true;

        return false;
    }


    void ElegirPreguntaAleatoria()
    {
        if (!QuedanPreguntasPorDificultad(dificultadActual))
        {
            if (dificultadActual.Trim().ToLower().Equals("facil"))
            {
                MusicaFacil.Stop();
                MusicaDificil.Play();
                dificultadActual = "dificil";
                panelDificil.SetActive(true);
                goku.Play();

                if (!QuedanPreguntasPorDificultad(dificultadActual))
                {
                    panelTerminado.SetActive(true);
                    MusicaDificil.Stop();
                    outroCoscu.Play();
                    if (puntos > 1250)
                    {
                        textfinal.text = "Has logrado completar el juego con un rendimiento bastante ejemplar pero la princesa esta en otro castillo";
                    }
                    else if(puntos >750)
                    {
                        textfinal.text = "Has logrado completar el juego con un rendimiento regular, pero bueno peor seria no haberlo completado, algo es algo bro tranquilo";
                    }
                    else if (puntos > 250)
                    {
                        textfinal.text = "Has 'logrado' terminar el juego por decirlo menos porque has sacado una puntuacion sinceramente muy mala";
                    }
                    else
                    {
                        textfinal.text = "Decir que has completado el juego es mentira, has acabado el juego pero de intentarlo sinceramente nada, asi que mucho menos acabarlo, te diria que lo puedes hacer mejor pero no creo";
                    }
                    return;
                }
            }
            else
            {
                panelTerminado.SetActive(true);
                    MusicaDificil.Stop();
                    outroCoscu.Play();
                    if (puntos > 1250)
                    {
                        textfinal.text = "Has logrado completar el juego con un rendimiento bastante ejemplar pero la princesa esta en otro castillo";
                    }
                    else if(puntos >750)
                    {
                        textfinal.text = "Has logrado completar el juego con un rendimiento regular, pero bueno peor seria no haberlo completado, algo es algo bro tranquilo";
                    }
                    else if (puntos > 250)
                    {
                        textfinal.text = "Has 'logrado' terminar el juego por decirlo menos porque has sacado una puntuacion sinceramente muy mala";
                    }
                    else
                    {
                        textfinal.text = "Decir que has completado el juego es mentira, has acabado el juego pero de intentarlo sinceramente nada, asi que mucho menos acabarlo, te diria que lo puedes hacer mejor pero no creo";
                    }
                return;
            }
        }

        string dificultadNormalizada = dificultadActual.Trim().ToLower();

        List<string> tiposDisponibles = new List<string>();

        if (multipleList.Exists(p => p.Dificultty.Trim().ToLower().Equals(dificultadNormalizada)))
            tiposDisponibles.Add("Multiple");

        if (trueFalseList.Exists(p => p.difficulty.Trim().ToLower().Equals(dificultadNormalizada)))
            tiposDisponibles.Add("TrueFalse");

        if (openList.Exists(p => p.difficulty.Trim().ToLower().Equals(dificultadNormalizada)))
            tiposDisponibles.Add("Open");

        if (tiposDisponibles.Count == 0)
        {
            Debug.Log("oh no...");
            return;
        }

        string tipoElegido = tiposDisponibles[Random.Range(0, tiposDisponibles.Count)];

        panelMultiple.SetActive(false);
        panelTrueFalse.SetActive(false);
        panelOpen.SetActive(false);
        panelQuestion.SetActive(false);

        if (tipoElegido.Equals("Multiple"))
        {
            List<MultipleQuestion> disponibles =
                multipleList.FindAll(p => p.Dificultty.Trim().ToLower().Equals(dificultadNormalizada));

            if (disponibles.Count == 0) return;

            preguntaMultipleActual =
                disponibles[Random.Range(0, disponibles.Count)];

            multipleList.Remove(preguntaMultipleActual);

            panelMultiple.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaMultipleActual.Question;
            buttonOP1.text = preguntaMultipleActual.Option1;
            buttonOP2.text = preguntaMultipleActual.Option2;
            buttonOP3.text = preguntaMultipleActual.Option3;
            buttonOP4.text = preguntaMultipleActual.Option4;

            textDificultad.text = "Dificultad: " + preguntaMultipleActual.Dificultty;
        }

        else if (tipoElegido.Equals("TrueFalse"))
        {
            List<TrueFalseQuestion> disponibles =
                trueFalseList.FindAll(p => p.difficulty.Trim().ToLower().Equals(dificultadNormalizada));

            if (disponibles.Count == 0) return;

            preguntaTFActual =
                disponibles[Random.Range(0, disponibles.Count)];

            trueFalseList.Remove(preguntaTFActual);

            panelTrueFalse.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaTFActual.question;
            textDificultad.text = "Dificultad: " + preguntaTFActual.difficulty;

            buttonVerdadero.text = "Verdadero";
            buttonFalso.text = "Falso";
        }

        else
        {
            List<OpenQuestion> disponibles =
                openList.FindAll(p => p.difficulty.Trim().ToLower().Equals(dificultadNormalizada));

            if (disponibles.Count == 0) return;

            preguntaOpenActual =
                disponibles[Random.Range(0, disponibles.Count)];

            openList.Remove(preguntaOpenActual);

            panelOpen.SetActive(true);
            panelQuestion.SetActive(true);

            textPregunta.text = preguntaOpenActual.question;
            textDificultad.text = "Dificultad: " + preguntaOpenActual.difficulty;

            inputRespuestaOpen.text = "";
        }
    }
    public void cerrarIncorrecto()
    {
        panelIncorrecto.SetActive(false);
        WahWahWah.Stop();
    }
    public void cerrarCorrecto()
    {
        panelCorrecto.SetActive(false);
        Yei.Stop();
    }
    public void cerrarDificil()
    {
        panelDificil.SetActive(false);
        goku.Stop();
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
            panelCorrecto.SetActive(true);
            Yei.Play();
            puntos = puntos + 50;
            textpuntos.text = $"{puntos}";
        }
        else
        {
            textResultado.text = "La respuesta correcta era: " +preguntaMultipleActual.Answer;
            panelIncorrecto.SetActive(true);
            WahWahWah.Play();
            puntos = puntos - 50;
            textpuntos.text = $"{puntos}";
        }

        textVersiculo.text = "Justificación: " + preguntaMultipleActual.Versiculo;

        nextQuestionButton.gameObject.SetActive(true);


    }

    public void ValidarTrueFalse(bool respuestaJugador)
    {
        panelQuestion.SetActive(false);
        panelResultado.SetActive(true);

        bool respuestaCorrecta = preguntaTFActual.answer.ToLower().Equals("verdadero");

        if (respuestaJugador == respuestaCorrecta)
        {
            textResultado.text = "Correcto";
            panelCorrecto.SetActive(true);
            Yei.Play();
            puntos = puntos + 50;
            textpuntos.text = $"{puntos}";

        }
        else
        {
            textResultado.text = "la respuesta correcta era: "+preguntaTFActual.answer;
            panelIncorrecto.SetActive(true);
            WahWahWah.Play();
            puntos = puntos - 50;
            textpuntos.text = $"{puntos}";

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
            panelCorrecto.SetActive(true);
            Yei.Play();
            puntos = puntos + 50;
            textpuntos.text = $"{puntos}";
        }
        else
        {
            textResultado.text = "La respuesta Correcta era: " + respuestaCorrecta ;
            panelIncorrecto.SetActive(true);
            WahWahWah.Play();
            puntos = puntos - 50;
            textpuntos.text = $"{puntos}";
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
