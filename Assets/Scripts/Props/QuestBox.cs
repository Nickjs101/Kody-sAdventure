using UnityEngine;

public class QuestBox : MonoBehaviour
{
    
    [SerializeField] private GameObject Acitvity;

    public void OpenActivity() {
        if(Acitvity.GetComponent<QuizManager>() != null){
            if(Acitvity.GetComponent<QuizManager>().isCorrect)return;
                LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<StatementComment>() != null && !Acitvity.GetComponent<StatementComment>().isCompleted){
            Acitvity.GetComponent<StatementComment>().TimerOpen();
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<IndentME>() != null){
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }
        if(Acitvity.GetComponent<CatchingGame>() != null){
            Acitvity.SetActive(true);
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<OutputGuess2>() != null){
            Acitvity.SetActive(true);
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<ModifyElement>() != null){
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<OutputElement>() != null){
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<TFquizManaer>() != null){
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }

        if(Acitvity.GetComponent<CompleteCode>() != null){
            LeanTween.scale(Acitvity, Vector3.one, .12f);
        }
            
    }
}
