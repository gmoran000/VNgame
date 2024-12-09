using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    //Text Component
    public TextMeshPro Text;
    public TextMeshPro CharaName;
    public SpriteRenderer TextBox;
    //The SpriteRenderer that draws the speaking character
    public SpriteRenderer Character;
    //The SpriteRenderer that draws the background
    public SpriteRenderer Background;
    public SpriteRenderer Effect;

    //A list of all the character sprites
    //I need to make this variables so I can reference them
    public Sprite Moondae;

    //public Sprite GaryFrown;
    //public Sprite GaryWink;
    
    //A list of all the background sprites
    public Sprite OutsideBG;
    public Sprite TextBoxBG;
    public Sprite BathroomBG;
    public Sprite RoomBG;
    public Sprite BubbleBG;
    public Sprite PopUpBG;
    
    //A list of all the lines of dialogue
    //These will be read out by the characters in order
    //DialogueLine is a custom class at the bottom of this script
    public List<DialogueLine> Lines;
    //Which line of dialogue am I currently on?
    public int Index = 0;
    public int CurrentLetter = 0;
    public float TextTimer = 0;
    

    void Start()
    {
        //Here I make sure to imprint the first line of dialogue
            //on the text/sprite renderers
        ImprintLine();
    }

    private void Update()
    {
        //If I hit space. . .
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Set the current line of dialogue to the next one
            Index++;
            CurrentLetter = 0;
            //And redo all the text and art to match it
            //ImprintLine();
        }
        
        TextTimer -= Time.deltaTime;
        if (TextTimer <= 0)
        {
            CurrentLetter++;
            ImprintLine();
            TextTimer = 0.03f;
        }
        
    }

    //Makes all the text and art match the dialogue line we're currently on
    public void ImprintLine()
    {
        //If we've hit the end of the script. . .
        if (Index >= Lines.Count)
        {
            //End the game
            SceneManager.LoadScene("TBC");
            return;
        }

        //Find which line of dialogue we're currently on
        DialogueLine current = Lines[Index];
        //Set the text to match that line's dialogue text
        int shownLetters = Mathf.Min(CurrentLetter, current.Text.Length);
        Text.text = current.Text.Substring(0, shownLetters);
        CharaName.text = current.CharaName;
        //Find the character art the line of dialogue requests
        Character.sprite = GetCharacter(current.Character);
        //Find the background art the line of dialogue requests
        Background.sprite = GetBackground(current.Background);
        Effect.sprite = GetEffect(current.Effect);
        TextBox.sprite = GetTextBox(current.TextBox);

    }

    //Convert the text description of a character to a sprite
    public Sprite GetCharacter(string who)
    {
        //If the dialogue line calls for "Gary", use this sprite
        if (who == "") return null;
        if (who == "Moondae") return Moondae;
        
        //if (who == "Gary Frown") return GaryFrown;
        //If Character is left blank, just don't change anything
        return Character.sprite;
    }
    
    //Convert the text description of a background to a sprite
    public Sprite GetBackground(string where)
    {
        //If the dialogue line calls for "Outside", use this sprite
        if (where == "") return null;
        if (where == "Motel Room Ceiling") return OutsideBG;
        if (where == "Bathroom") return BathroomBG;
        if (where == "Room") return RoomBG;
        //If Background is left blank, just don't change anything
        return Background.sprite;
    }
    
    public Sprite GetEffect(string what)
    {
        //If the dialogue line calls for "Gary", use this sprite
        if (what == "") return null;
        if (what == "ACK") return BubbleBG;
        if (what == "Pop") return PopUpBG;
        
        //if (who == "Gary Frown") return GaryFrown;
        //If Character is left blank, just don't change anything
        return Effect.sprite;
    }
    
    public Sprite GetTextBox(string what)
    {
        //If the dialogue line calls for "Gary", use this sprite
        if (what == "") return null;
        if (what == "Box") return TextBoxBG;
        
        //if (who == "Gary Frown") return GaryFrown;
        //If Character is left blank, just don't change anything
        return TextBox.sprite;
    }
   
    
}

//This line makes a class appear in the Unity Inspector
  //when used as a variable type
[System.Serializable]
public class DialogueLine
{
    //A custom class that just records dialogue, a character, and a background
    //Think of it almost like a Vector3, but for story instead of position
    public string Text;
    public string CharaName;
    public string TextBox;
    public string Character;
    public string Background;
    public string Effect;
}
