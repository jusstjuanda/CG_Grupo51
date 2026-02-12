using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    public TextAsset preguntasTXT;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI difficultyText;

    public Button boton1;
    public Button boton2;
    public Button boton3;
    public Button boton4;

    void Start()
    {

        string texto = preguntasTXT.text;

        string[] lineas = texto.Split('\n');

        string linea = lineas[0];

        string[] datos = linea.Split('-');

        questionText.text = datos[0];
        boton1.GetComponentInChildren<TextMeshProUGUI>().text = datos[1];
        boton2.GetComponentInChildren<TextMeshProUGUI>().text = datos[2];
        boton3.GetComponentInChildren<TextMeshProUGUI>().text = datos[3];
        boton4.GetComponentInChildren<TextMeshProUGUI>().text = datos[4];
        difficultyText.text = "Dificultad: " + datos[7];
    }
}
