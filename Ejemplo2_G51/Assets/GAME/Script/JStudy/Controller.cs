using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class Controller : MonoBehaviour
{



    List<Student> list_students = new List<Student>();

    public TMP_InputField tnameP;
    public TMP_InputField tmailP;
    public TMP_InputField tageP;
    public TMP_InputField tcourseS;
    public TMP_InputField tcodeS;

    public TextMeshProUGUI label;

    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/students.json";

        CargarStudentJson();
        SearchStudent();
        SearchPanel();
    }

    public void AddStudent()
    {
        Student student = new Student(
            tcourseS.text,
            tcodeS.text,
            tnameP.text,
            tmailP.text,
            int.Parse(tageP.text)
        );

        list_students.Add(student);

        Debug.Log("Estudiante Añadido: " +
            student.NameP + ", " +
            student.MailPP + ", " +
            student.Age + ", " +
            student.CourseS + ", " +
            student.CodeS);
    }

    public void SearchStudent()
    {
        foreach (Student s in list_students)
        {
            Debug.Log("Nombre: " + s.NameP +
                      ", Mail: " + s.MailPP +
                      ", Edad: " + s.Age +
                      ", Curso: " + s.CourseS +
                      ", Código: " + s.CodeS);
        }
    }

    public void SearchPanel()
    {
        label.text = "";

        foreach (Student s in list_students)
        {
            label.text +=
                "Nombre: " + s.NameP + "\n" +
                "Mail: " + s.MailPP + "\n" +
                "Edad: " + s.Age + "\n" +
                "Curso: " + s.CourseS + "\n" +
                "Código: " + s.CodeS + "\n\n";
        }
    }


    public void GuardarEstudianteJson()
    {
        StudentListWrapper wrapper = new StudentListWrapper();
        wrapper.students = list_students;

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Guardado en: " + filePath);
    }

    public void CargarStudentJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StudentListWrapper wrapper =
                JsonUtility.FromJson<StudentListWrapper>(json);

            list_students = wrapper.students;
        }
    }


    public void BorrarJson()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Archivo JSON eliminado");
        }

        list_students.Clear();
        label.text = "";
    }



}

[System.Serializable]
public class StudentListWrapper
{
    public List<Student> students;
}










