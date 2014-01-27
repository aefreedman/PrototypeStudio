using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour
{

    public List<Person> people;
    public List<Person> maleHetero;
    public List<Person> femaleHetero;
    public List<Person> maleHomo;
    public List<Person> femaleHomo;
    public List<Person> bi;
    public List<Person> inRelationship;
    public Person choiceA;
    public Person choiceB;
    public int StartingPopulation;
    
    private void Start()
    {
        float initTime = System.DateTime.Now.Millisecond;

        people = new List<Person>();
        maleHetero = new List<Person>();
        maleHomo = new List<Person>();
        femaleHetero = new List<Person>();
        femaleHomo = new List<Person>();
        bi = new List<Person>();

        PopulateWorld(StartingPopulation);
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
    public void PopulateWorld(int val)
    {
        for (int i = 0; i < val; i++)
        {
            GeneratePerson();
        }
    }

    public void GeneratePerson()
    {
        GameObject peopleContainer = GameObject.Find("People");
        GameObject o = Instantiate(Resources.Load(Dictionary.Prefabs.PREFAB_NAMES[Dictionary.Prefabs.type.Person])) as GameObject;
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

        for (int i = 0; i < matchesToMake-1; i++)
        {
            fHetLooking[i].significantOther = mHetLooking[i];
            mHetLooking[i].significantOther = fHetLooking[i];
        }

        matchesToMake = 0;
        if (Utilities.IsOdd(mHomoLooking.Count))
        {
            matchesToMake = (mHomoLooking.Count - 1) / 2;
        }
        else
        {
            matchesToMake = mHomoLooking.Count / 2;
        }

        if (matchesToMake > 0)
        {
            for (int i = 0; i < matchesToMake; i++)
            {
                mHomoLooking[i].significantOther = mHomoLooking[mHomoLooking.Count - i];
                mHomoLooking[mHomoLooking.Count - i].significantOther = mHomoLooking[i];
            }
        }

        matchesToMake = 0;
        if (Utilities.IsOdd(fHomoLooking.Count))
        {
            matchesToMake = (fHomoLooking.Count - 1) / 2;
        }
        else
        {
            matchesToMake = fHomoLooking.Count / 2;
        }

        if (matchesToMake > 0)
        {
            for (int i = 0; i < matchesToMake; i++)
            {
                fHomoLooking[i].significantOther = fHomoLooking[mHomoLooking.Count - i];
                fHomoLooking[mHomoLooking.Count - i].significantOther = fHomoLooking[i];
            }
        }

        inRelationship.AddRange(mHetLooking);
        inRelationship.AddRange(fHetLooking);
        inRelationship.AddRange(mHomoLooking);
        inRelationship.AddRange(fHomoLooking);

    }


}