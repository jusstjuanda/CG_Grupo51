using UnityEngine;

public class MultipleQuestion
{
    private string question;
    private string option1;
    private string option2;
    private string option3;
    private string option4;
    private string answer;
    private string versiculo;
    private string dificultty;

    public MultipleQuestion()
    {
    }

    public MultipleQuestion(string question, string option1, string option2, string option3, string option4, string answer, string versiculo, string dificultty)
    {
        this.question = question;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
        this.option4 = option4;
        this.answer = answer;
        this.versiculo = versiculo;
        this.dificultty = dificultty;
    }

    public string Question { get => question; set => question = value; }
    public string Option1 { get => option1; set => option1 = value; }
    public string Option2 { get => option2; set => option2 = value; }
    public string Option3 { get => option3; set => option3 = value; }
    public string Option4 { get => option4; set => option4 = value; }
    public string Answer { get => answer; set => answer = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Dificultty { get => dificultty; set => dificultty = value; }
}