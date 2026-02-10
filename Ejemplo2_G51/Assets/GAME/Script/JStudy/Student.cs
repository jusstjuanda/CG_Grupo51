using UnityEngine;
[System.Serializable]
public class Student : Person
{
    private string courseS;
    private string codeS;

    public Student()
    {
    }

    public Student(string courseS, string codeS, string nameP, string mailP, int ageP) : base (nameP, mailP, ageP)
    {
        this.courseS = courseS;
        this.codeS = codeS;
        this.nameP = nameP;
        this.mailP = mailP;
        this.ageP = ageP;
    }


    public string CourseS { get => courseS; set => courseS = value; }
    public string CodeS { get => codeS; set => codeS = value; }
}
