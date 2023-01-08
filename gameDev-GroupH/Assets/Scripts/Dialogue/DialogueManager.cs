using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI displaytext;
    public TextMeshProUGUI charactername;
    public float textspeed;

    public Dialogue dialogue;

    public SaveData savedata;

    string[][] parts;

    [SerializeField]
    private int index;

    public Image img;

    private bool paused;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private EnemyCollider enemy;

    [SerializeField]
    private Sprite [] characters;

    [SerializeField]
    private Dictionary<string, Sprite> charac_sprites = new Dictionary<string, Sprite>();

    [SerializeField]
    private ScriptDialogue startingDialogue;
    public BattleTutorialController tutorialController;

    // Start is called before the first frame update
    public void Start()
    {
        // Sets up characters and their sprites
        string[] temp = new string[] { "Nadiya", "Dreyar", "Astra", "Joe" , "Distant Voice" };

        for (int i = 0; i < 5 ; i++)
                charac_sprites.Add(temp[i], characters[i]);

        // Starts initial dialoge if it hasn't started
        if (this.savedata.get_dialogue_index() == 0)
        {
            if (!savedata.inBattle)
            {
                this.PauseAll();
            }
            this.Script(this.savedata.get_dialogue_index(), startingDialogue);
            this.savedata.set_dialogue_index(this.GetIndex());
        }
        else
            gameObject.SetActive(false);

    }


    public int GetIndex() { return this.dialogue.GetIndex(); }

    public void Script(int start , ScriptDialogue path)
    {
        // Activate dialogue 
        gameObject.SetActive(true);

        // Gets dialogue script
        this.dialogue = new Dialogue(start, path.GetDialogue());

        // Checks for valid length
        if (this.dialogue.GetParts().Length > 0)
        {
            this.StartDialogue();
            Debug.Log(this.dialogue.GetIndex());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void OnSkip(InputValue value)
    {
        if (value.isPressed)
        {
            // Diplays text until end
            if (this.displaytext.text == this.parts[this.index][1])
            {
                this.NextLine();
            }
            // Skips for text to slowly appear and forcefully appear
            else
            {
                StopAllCoroutines();
                this.displaytext.text = this.parts[this.index][1];
            }

        }


    }

    public void PauseAll()
    {
        // Pause player and enemy
        player.canMove = false;
        enemy.StopAll();
        this.paused = true;
    }


    public void StartDialogue()
    {
        // Retrives lines
        this.parts = this.dialogue.GetParts();

        // Empty dialogue box
        this.displaytext.text = string.Empty;
        this.charactername.text = string.Empty;

        // Coutner for lines
        this.index = 0;
        StartCoroutine(this.TypeLine());
    }


    IEnumerator TypeLine()
    {
        // Diaplay sprite and character name
        this.img.sprite = charac_sprites[this.parts[index][0]];
        this.charactername.text = this.parts[index][0];

        // Displays text gradually
        foreach (char c in this.parts[index][1].ToCharArray())
        {
            this.displaytext.text += c;
            yield return new WaitForSeconds(this.textspeed);
        }


    }

    void NextLine()
    {
        if (this.index < this.parts.Length - 1)
        {
            // Goes to next line
            this.index++;
            this.displaytext.text = string.Empty;
            StartCoroutine(this.TypeLine());
        }
        else
        {
            // Deactivates dialogue box
            gameObject.SetActive(false);
            if (savedata.inBattle)
            {
                tutorialController.DeactivateTutorial();
            }

            // Enables movement
            if (this.paused)
            {
                player.canMove = true;
                enemy.ResumeAll(); 
            }


        }
    }
}
