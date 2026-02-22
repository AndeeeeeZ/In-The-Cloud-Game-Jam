using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private IntValue score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI commentText;

    private void Start()
    {
        scoreText.text = score.value.ToString();
        UpdateComment();
    }

    private void UpdateComment()
    {
        string comment;

        int s = score.value;
        if (s < 10)
        {
            comment = "HELLO??? LOCK IN BRO";
        }
        else if (s < 10)
        {
            comment = "You can do better...";
        }
        else if (s < 300)
        {
            comment = "Okay...?";
        }
        else if (s < 500)
        {
            comment = "Seems pretty good";
        }
        else if (s < 1000)
        {
            comment = "Impressive.";
        }
        else if (s < 3000)
        {
            comment = "WOW!";
        }
        else if (s < 10000)
        {
            comment = "HOW DID YOU EVEN DO THAT???";
        }
        else
        {
            comment = "You are too good. DAMN.";
        }
        commentText.text = comment;
    }
}
