using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hacker : MonoBehaviour
{

    enum Screen { Menu, Password, Game, Win }
    Screen currentScreen;

    List<string> words1 = new List<string>() { "code", "tree", "apple" };
    List<string> words2 = new List<string>() { "private", "juggalo", "hooplah" };
    List<string> words3 = new List<string>() { "metamorphosis", "juxtaposition", "ideologies" };

    string password = "password";
    int level = 1;
    string word;
	// Use this for initialization
	void Start () {
        ShowMenu();
	}

    void ShowMenu()
    {
        currentScreen = Screen.Menu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello, Doofus");
        Terminal.WriteLine("Let's guess anagrams! \n");
        Terminal.WriteLine("You'll start at level 1, going up a level of difficulty with each successful guess");
        //Terminal.WriteLine("Select a difficulty by number: \n" +
                           //"1) Easy \n" +
                           //"2) Medium \n" +
                           //"3) Are you CRAZY?! \n");
        Terminal.WriteLine("Type 'menu' to go back");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMenu();
        }
        else if (currentScreen == Screen.Menu)
        {
            MenuInput();
        }
        else if (currentScreen == Screen.Password)
        {
            EnterPassword(input);
        }
        else if (currentScreen == Screen.Game)
        {
            WordGuess(input);
        }
        else if (currentScreen == Screen.Win)
        {
            PlayAgain(input);
        }
    }

    public static string RandomWord(List<string> words) {
        System.Random rng = new System.Random();
        int index = rng.Next(words.Count);
        var word = words[index];
        words.RemoveAt(index);
        return word;
    }

    public static string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }



    void MenuInput()
    {
        //bool isValidLevel = (input == "1" || input == "2");
        //if (isValidLevel) 
        //{
        //    level = int.Parse(input);
        //    StartGame();
        //}
        if (Input.GetKeyDown(KeyCode.Return)) {
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please make a valid input");
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("Please enter your password");
    }

    void EnterPassword(string input) {
        if (input == password) { 
            Terminal.WriteLine("Successful Login");
            currentScreen = Screen.Game;
            Words();
        } else {
            Terminal.WriteLine("Incorrect Login Info");
        }
    }

    void Words(){
        Terminal.ClearScreen();

        switch (level)
        {
            case 1:
                word = RandomWord(words1);
                break;
            case 2:
                word = RandomWord(words2);
                break;
            case 3:
                word = RandomWord(words3);
                break;
            default:
                throw new Exception();
                Debug.LogError("Error in word selection");
        }
        string shuffleWord = Shuffle(word);
        Terminal.WriteLine("Your word is: " + shuffleWord);

        //if (level == 1){
        //    word = RandomWord(words1);
        //    string shuffledWord = Shuffle(word);
        //    Terminal.WriteLine("Your word is: " + shuffledWord);
        //}else if (level == 2){
        //    word = RandomWord(words2);
        //    string shuffledWord = Shuffle(word);
        //    Terminal.WriteLine("Your word is: " + shuffledWord);
        //}else if (level == 3){
        //    word = RandomWord(words3);
        //    string shuffledWord = Shuffle(word);
        //    Terminal.WriteLine("Your word is: " + shuffledWord);
        //}
    }

    void WordGuess(string input) {
       
        if (level == 3 && input == word) 
        {
            Terminal.WriteLine("I see you've bested me, you scallywag. Press 'Return'");
            currentScreen = Screen.Win;
        }
        else if (input == word)
        {    
            level += 1;
            Words();
        } 
         else 
        {
            Terminal.WriteLine("Try again :(");
        }
    }

    void WinGame() {
        Terminal.WriteLine("You win, but you're not too smart. Press Return");
        currentScreen = Screen.Win;
    }


    void PlayAgain(string input) {
        Terminal.WriteLine("Play Again? (y or n)");

        if (input == "y") {
            currentScreen = Screen.Game;
            level = 1;
            Words();
        } else if (input == "n") {
            currentScreen = Screen.Menu;
            ShowMenu();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
