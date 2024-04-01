// Linq allows us to access the last thing/term in a list easily
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    // (script referenced from youtube tutorial: https://www.youtube.com/watch?v=tdXXW0ln_LU)

    private List<string> originalWords = new List<string>()
    {
        "valerie", "arman", "martha", "joshua", "karissa", "samuel", "max", 
        "ashley", "aleks", "sadie", "ethan", "kai", "tifany", "stella", "nikki",
        "jamie", "jace", "ryan", "gahge", "rogue", "tyler", "charlotte"
    };

    private List<string> workingWords = new List<string>();

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // grab one of the strings in the list
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            // move string to random number in list
            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> List)
    {
        for(int i = 0;i < List.Count;i++)
        {
            List[i] = List[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if(workingWords.Count != 0)
        {
            // grabs a new word from the list and deletes it from said list
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }

        Debug.Log(ListToText(workingWords));

        return newWord;
    }

    // for debug purposes:
    private string ListToText(List<string> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember.ToString() + "";
        }

        return result;
    }
}
