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

    string password;
    int level;
    string word;
	// Use this for initialization
	void Start () {
        ShowMenu();
	}

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine("Please enter your password");
    }

    void ShowMenu()
    {
        currentScreen = Screen.Menu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello, Doofus");
        Terminal.WriteLine("Let's guess anagrams? \n");
        Terminal.WriteLine("Select a difficulty by number: \n" +
                           "1) Easy \n" +
                           "2) Medium \n" +
                           "3) Are you CRAZY?! \n");
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
            MenuInput(input);
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



    void MenuInput(string input)
    {
        if (input == "1"){
            level = 1;
            password = "password";
            StartGame();
        }else if (input == "2"){
            level = 2;
            password = "newPassword";
            StartGame();
        }else if (input == "3"){
            level = 3;
            StartGame();

        }else{
            Terminal.WriteLine("Please make a valid input");
        }
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
        if (level == 1){
            word = RandomWord(words1);
            string shuffledWord = Shuffle(word);
            Terminal.WriteLine("Your word is: " + shuffledWord);
        }else if (level == 2){
            word = RandomWord(words2);
            string shuffledWord = Shuffle(word);
            Terminal.WriteLine("Your word is: " + shuffledWord);
        }else if (level == 3){
            word = RandomWord(words3);
            string shuffledWord = Shuffle(word);
            Terminal.WriteLine("Your word is: " + shuffledWord);
        }
    }

    void WordGuess(string input) {
        if (level == 3 && input == word) {
            WinGame();
        }
        else if (input == word)
        {    
            Terminal.WriteLine("Nice! Try the next level.");
            level += 1;
            Words();
        } 
         else {
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
