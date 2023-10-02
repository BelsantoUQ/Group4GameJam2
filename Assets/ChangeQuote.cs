using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeQuote : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textQuote;
    string[] quotes = new string[]
    {
        "",
        "So long as there are men, there will be wars. -Albert Einstein",
        "The press is our chief ideological weapon. -Nikita Khrushchev",
        "Mankind must put an end to war, or war will put an end to mankind. -John F. Kennedy",
        "Every tyrant who has lived has believed in freedom -for himself. -Elbert Hubbard",
        "Let your plans be as dark and impenetrable as night, and when you move, fall like a thunderbolt. -Sun Tzu",
        "All warfare is based on deception. -Sun Tzu",
        "A leader leads by example, not by force. -Sun Tzu",
        "When the pin is pulled, Mr. Grenade is not our friend. -U.S. Army Training Notice",
        "A man may die, nations may rise and fall, but an idea lives on. -John F. Kennedy",
        "Nothing in life is so exhilarating as to be shot at without result. -Winston Churchill"
    };
    
    // Start is called before the first frame update
    void Start()
    {
        textQuote.text = "" + quotes[Random.Range(1,10)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
