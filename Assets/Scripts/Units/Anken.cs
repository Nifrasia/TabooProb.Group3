using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum Gender
{
    male,
    female
}

public class Anken : Unit
{

    private int id;
    public int ID { get { return id; } set { id = value; } }
    [SerializeField] private int charSkinID;
    public int CharSkinID { get { return charSkinID; } set { charSkinID = value; } }
    public GameObject[] charSkin;

    [SerializeField]private float sp = 200;
    public float SP { get { return sp; } set { sp = value; } }

    [SerializeField] private float hunger = 220;
    public float Hunger { get {  return hunger; } set {  hunger = value; } }


    [SerializeField] private int charFaceID;
    public int CharFaceID { get { return charFaceID; } set { charFaceID = value; } }
    public Sprite[] charFacePic;


    [SerializeField] private string staffName;
    public string StaffName { get { return staffName; } set { staffName = value; } }

    [SerializeField] private Gender staffGender = Gender.male;
    public Gender StaffGender { get { return staffGender; } set { staffGender = value; } }

    [SerializeField] private bool hired = false;
    public bool Hired { get { return hired; } set { hired = value; } }

  
    public void InitiateCharID(int i)
    {
        charSkinID = i;
        charFaceID = i;
    }

    public void SetGender()
    {
        if (charSkinID == 1 || charSkinID == 4)
        {
            staffGender = Gender.female;
        }
    }
    public void ChangeCharSkin()
    {
        for (int i = 0; i < charSkin.Length; i++)
        {
            if (i == charSkinID)
            {
                charSkin[i].SetActive(true);
            }
            else
            {
                charSkin[i].SetActive(false);
            }
        }

    }
}
