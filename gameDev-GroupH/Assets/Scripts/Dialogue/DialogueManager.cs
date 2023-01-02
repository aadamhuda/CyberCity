using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI displaytext;
    public TextMeshProUGUI charactername;
    public float textspeed;

    public Dialogue dialogue;

    public SaveData savedata;

    [SerializeField]
    string[] lines;

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

    // Start is called before the first frame update
    void Start()
    {



        string[] temp = new string[] { "Nadiya", "Dreyar", "Astra", "Joe" , "Distant Voice" };

        for (int i = 0; i < 5 ; i++)
            charac_sprites.Add(temp[i], characters[i]);


        if (this.savedata.get_dialogue_index() == 0)
        {
            this.PauseAll();
            this.Script(0, "/Scripts/Dialogue/test.txt", "dialogue");
        }
        else
            gameObject.SetActive(false);

    }
    public void Script(int start , string path, string save)
    {
        gameObject.SetActive(true);

        this.dialogue = new Dialogue(start, path);

        if (this.dialogue.GetParts().Length > 0)
        {
            this.StartDialogue();
            Debug.Log(this.dialogue.GetIndex());
            if (save == "clue")
                savedata.set_clues_text_index(this.dialogue.GetIndex());
            else if (save == "dialogue")
                savedata.set_dialogue_index(this.dialogue.GetIndex());
            else if (save == "save")
                savedata.set_save_index(this.dialogue.GetIndex());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.displaytext.text == this.parts[this.index][1])
            {
                this.NextLine();
            }
            else
            {
                StopAllCoroutines();
                this.displaytext.text = this.parts[this.index][1];
            }

        }


    }

    public void PauseAll()
    {
        player.canMove = false;
        enemy.StopAll();
        this.paused = true;
    }


    public void StartDialogue()
    {
        //Time.timeScale = 0.1f;



        this.lines = dialogue.GetSentences();
        this.parts = this.dialogue.GetParts();
        this.displaytext.text = string.Empty;
        this.charactername.text = string.Empty;

        this.index = 0;
        StartCoroutine(this.TypeLine());
    }


    IEnumerator TypeLine()
    {
        this.img.sprite = charac_sprites[this.parts[index][0]];
        this.charactername.text = this.parts[index][0];
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
            this.index++;
            this.displaytext.text = string.Empty;
            StartCoroutine(this.TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            if (this.paused)
            {
                player.canMove = true;
                enemy.ResumeAll(); 
            }


        }
    }
}
