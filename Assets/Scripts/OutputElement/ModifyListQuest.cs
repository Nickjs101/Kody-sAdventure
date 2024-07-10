using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyListQuest : MonoBehaviour
{
    public enum QuestionType{
        Change,
        Add,
        Remove
    }
    public QuestionType questionType;
    [HideInInspector]
    public QuestionType add = QuestionType.Add;
    [HideInInspector]
    public QuestionType remove = QuestionType.Remove;
    [HideInInspector]
    public QuestionType change = QuestionType.Change;
    public GameObject slot;
    public string Question;
    public string answer;
    public int indexToChange;
    public string[] list;
    public string[] options;

}
