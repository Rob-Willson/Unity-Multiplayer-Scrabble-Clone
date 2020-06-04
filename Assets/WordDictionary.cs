using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordDictionary : MonoBehaviour
{
    private Dictionary<char, Dictionary<string, string>> dictionariesByLetter = new Dictionary<char, Dictionary<string, string>>();

    private void Start ()
    {
        LoadWordsFromFile();

        Debug.Log("temperature: " + IsWordInDictionary("temperature"));
        Debug.Log("zebra: " + IsWordInDictionary("zebra"));
        Debug.Log("Alphabet: " + IsWordInDictionary("Alphabet"));
        Debug.Log("ksdkfk: " + IsWordInDictionary("ksdkfk"));
        Debug.Log("1234: " + IsWordInDictionary("1234"));
    }

    private void LoadWordsFromFile ()
    {
        FileInfo fileInfo = new FileInfo(Path.Combine(Application.streamingAssetsPath, "WordsWithDefinitions.json"));
        if(!File.Exists(fileInfo.FullName))
        {
            Debug.LogError("FAIL: Files doesn't exist, or is invalid. Bug?");
            return;
        }

        string wordsWithDefinitions = File.ReadAllText(fileInfo.FullName);
        Dictionary<string, string>[] arrayOfDictionariesByLetter = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(wordsWithDefinitions);

        for(int i = 0; i < arrayOfDictionariesByLetter.Length; i++)
        {
            char firstLetter = '\0';
            foreach(var item in arrayOfDictionariesByLetter[i])
            {
                if(string.IsNullOrWhiteSpace(item.Key))
                {
                    continue;
                }
                firstLetter = item.Key.ToLower()[0];
                break;
            }

            if(!char.IsLetter(firstLetter))
            {
                Debug.LogError("FAIL: Invalid first letter found.");
                return;
            }

            dictionariesByLetter.Add(firstLetter, new Dictionary<string, string>(arrayOfDictionariesByLetter[i]));
        }
    }

    public bool IsWordInDictionary (string wordToCheck)
    {
        wordToCheck = wordToCheck.ToLower();

        if(string.IsNullOrWhiteSpace(wordToCheck))
        {
            Debug.LogError("FAIL: Passed word is null, empty, or whitespace");
            return false;
        }

        Dictionary<string, string> dictionary = GetDictionaryFromLetter(wordToCheck[0]);
        if(dictionary == null)
        {
            Debug.Log("Couldn't find dictionary by letter: '" + wordToCheck[0] + "'. Bug?");
            return false;
        }

        return dictionary.ContainsKey(wordToCheck);
    }

    private Dictionary<string, string> GetDictionaryFromLetter (char letter)
    {
        if(dictionariesByLetter.TryGetValue(letter, out Dictionary<string, string> dictionaryOfLetter))
        {
            return dictionaryOfLetter;
        }
        return null;
    }

}
