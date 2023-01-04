using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


[System.Serializable]
public class Dialogue
{
    public string name;
    public string[] sentences;

    private string[][] parts;

    private int index;

    public Dialogue (int index, string [] script)
    {
        this.index = index;
        this.sentences = script;
        this.ReadFromFile();
    }

    public string [] GetSentences() { return this.sentences; }
    public string [][] GetParts() { return this.parts; }

    public int GetIndex() { return this.index; }

    public void ReadFromFile()
    {

        int begginning = this.index;
        int count = FindLength();

        string[][] temp = new string[count - begginning][];
        int counter = 0;

        for (int i = begginning; i < count; i++)
        {
            temp[counter] = this.sentences[i].Split(char.Parse("_"));
            counter++;

        }

        this.parts = temp;

    }

    private int FindLength()
    {
        int count = this.index;

        for (int i = this.index; i < this.sentences.Length; i++)
        {
            count++;
            if (this.sentences[i] == "------")
            {
                Debug.Log("Why Sally sell seashells on the seashore ; " + i);
                this.index = i+1;
                return count-1;
            }
        }
        return count;
    }

}
