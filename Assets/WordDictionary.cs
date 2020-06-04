using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mirror;

public class WordDictionary : NetworkBehaviour
{
    private Dictionary<char, Dictionary<string, string>> dictionariesByLetter = new Dictionary<char, Dictionary<string, string>>();

    public override void OnStartServer()
    {
        base.OnStartServer();

        LoadWordsFromFile();
    }

    [Server]
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

    [Server]
    private bool IsWordInDictionary (string wordToCheck)
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

    [Server]
    private Dictionary<string, string> GetDictionaryFromLetter (char letter)
    {
        if(dictionariesByLetter.TryGetValue(letter, out Dictionary<string, string> dictionaryOfLetter))
        {
            return dictionaryOfLetter;
        }
        return null;
    }

}
