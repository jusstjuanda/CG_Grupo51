using UnityEngine;
[System.Serializable]
public class Person
{
    public string nameP;
    public string mailP;
    public int ageP;

    public Person()
    {
    }

    public Person(string nameP, string mailP, int ageP)
    {
        this.nameP = nameP;
        this.mailP = mailP;
        this.ageP = ageP;
    }

    public string NameP { get => nameP; set => nameP = value; }
    public string MailPP { get => mailP; set => mailP = value; }
    public int Age { get => ageP; set => ageP = value; }

}
