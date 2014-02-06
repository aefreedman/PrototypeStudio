using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour
{

    public List<Person> people;
    public List<Person> deceased;
    public List<Person> maleHetero;
    public List<Person> femaleHetero;
    public List<Person> maleHomo;
    public List<Person> femaleHomo;
    public List<Person> bi;
    public List<Person> inRelationship;
    public int rows;
    public int columns;
    public int levels;

    public static World Instance()
    {
        return FindObjectOfType<World>();
    }
    
    private void Start()
    {
        float initTime = System.DateTime.Now.Millisecond;

        if (rows * columns * levels > 1000)
        {
            Debug.Break();
        }

        people = new List<Person>();
        deceased = new List<Person>();
        maleHetero = new List<Person>();
        maleHomo = new List<Person>();
        femaleHetero = new List<Person>();
        femaleHomo = new List<Person>();
        bi = new List<Person>();

        PopulateWorld();
        GenerateConnections();

        Debug.Log("Generated world in " + (System.DateTime.Now.Millisecond - initTime) + "ms");


    }

    private void Update()
    {

    }

    /// <summary>
    /// Populates the world with an initial number of people.
    /// </summary>
    /// <param name="val">The number of people to start with</param>
    public void PopulateWorld()
    {
        float spacing = 2.0f;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                for (int k = 0; k < levels; k++)
                {
                    GeneratePerson(new Vector3(i + spacing * i, j + spacing * j, k + spacing * k));
                }
            }
        }
    }

    public void GeneratePerson(Vector3 pos)
    {
        GameObject peopleContainer = GameObject.Find("People");
        GameObject o = Instantiate(Resources.Load(Dictionary.Prefabs.PREFAB_NAMES[Dictionary.Prefabs.type.Person]), pos, Quaternion.identity) as GameObject;
        //o.transform.position = pos;
        o.transform.parent = peopleContainer.transform;
        Person p = o.GetComponent<Person>();
        p.Initialize();
        people.Add(p);
    }

    public void GenerateConnections()
    {
        List<Person> males = new List<Person>();
        List<Person> females = new List<Person>();
        List<Person> fHetLooking = new List<Person>();
        List<Person> mHetLooking = new List<Person>();
        List<Person> fHomoLooking = new List<Person>();
        List<Person> mHomoLooking = new List<Person>();

        foreach (var p in people)
        {
            if (p.sexuality == Person.Sexuality.bi)
            {
                bi.Add(p);
            }
            else if (p.sex == Person.Sex.male)
            {
                males.Add(p);
            }
            else if (p.sex == Person.Sex.female)
            {
                females.Add(p);
            }
        }

        Debug.Log(males.Count + " males / " + females.Count + " females");

        foreach (var a in males)
        {
            if (a.sexuality == Person.Sexuality.hetero)
            {
                maleHetero.Add(a);
                if (a.relationship == true)
                {
                    mHetLooking.Add(a);
                }
            }
            else if (a.sexuality == Person.Sexuality.homo)
            {
                maleHomo.Add(a);
                if (a.relationship == true)
                {
                    mHomoLooking.Add(a);
                }
            }
        }

        foreach (var f in females)
        {
            if (f.sexuality == Person.Sexuality.hetero)
            {
                femaleHetero.Add(f);
                if (f.relationship == true)
                {
                    fHetLooking.Add(f);
                }
            }
            else if (f.sexuality == Person.Sexuality.homo)
            {
                femaleHomo.Add(f);
                if (f.relationship == true)
                {
                    fHomoLooking.Add(f);
                }
            }
        }

        int matchesToMake = 0;
        if (mHetLooking.Count > fHetLooking.Count)
        {
            matchesToMake = fHetLooking.Count;
        }
        else if (fHetLooking.Count > mHetLooking.Count)
        {
            matchesToMake = mHetLooking.Count;
        }
        else if (fHetLooking.Count == mHetLooking.Count)
        {
            matchesToMake = fHetLooking.Count;
        }

        for (int i = 0; i < matchesToMake - 1; i++)
        {
            fHetLooking[i].significantOther = mHetLooking[i];
            mHetLooking[i].significantOther = fHetLooking[i];
        }

        matchesToMake = (mHomoLooking.Count - mHomoLooking.Count % 2) / 2;

        if (matchesToMake > 0)
        {
            for (int i = 0; i < matchesToMake; i += 2)
            {
                mHomoLooking[i].significantOther = mHomoLooking[i + 1];
                mHomoLooking[i + 1].significantOther = mHomoLooking[i];
            }
        }

        matchesToMake = (fHomoLooking.Count - fHomoLooking.Count % 2) / 2;

        if (matchesToMake > 0)
        {
            for (int i = 0; i < matchesToMake; i += 2)
            {
                fHomoLooking[i].significantOther = fHomoLooking[i + 1];
                fHomoLooking[i + 1].significantOther = fHomoLooking[i];
            }
        }

        inRelationship.AddRange(mHetLooking);
        inRelationship.AddRange(fHetLooking);
        inRelationship.AddRange(mHomoLooking);
        inRelationship.AddRange(fHomoLooking);

    }


}