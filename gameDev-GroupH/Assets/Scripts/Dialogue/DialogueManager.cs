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

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Sprite [] characters;

    [SerializeField]
    private Dictionary<string, Sprite> charac_sprites = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start()
    {

        this.dialogue = new Dialogue(this.savedata.get_dialogue_index() , "/Scripts/Dialogue/test.txt");


        string[] temp = new string[] { "Eve", "Dreyar", "Astra", "Joe" };

        for (int i = 0; i < 4; i++)
            charac_sprites.Add(temp[i], characters[i]);


        Debug.Log(this.dialogue.GetParts().Length);

        if (this.dialogue.GetParts().Length > 0)
        {
            this.StartDialogue();
            savedata.set_dialogue_index(this.dialogue.GetIndex());
        }
        else
            gameObject.SetActive(false);
    }

    public void ClueScript(int start , string path)
    {
        gameObject.SetActive(true);

        this.dialogue = new Dialogue(start, path);

        if (this.dialogue.GetParts().Length > 0)
        {
            this.StartDialogue();
            savedata.set_clues_text_index(this.dialogue.GetIndex());
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
            if ( this.displaytext.text == this.parts[this.index][1] ) {
                this.NextLine();
            }
            else
            {
                StopAllCoroutines();
                this.displaytext.text = this.parts[this.index][1];
            }
        }
    }



    public void StartDialogue()
    {
        //Time.timeScale = 0.1f;

        player.canMove = false;
        enemy.SetActive(false);

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

            player.canMove = true;
            enemy.SetActive(true);

        }
    }
}
