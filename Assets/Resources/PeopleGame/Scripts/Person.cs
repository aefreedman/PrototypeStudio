using UnityEngine;

public class Person : MonoBehaviour
{
    #region PARAMETERS
    private int WEALTH_MIN = 1;
    private int WEALTH_MAX = 100;
    private int FAMILY_MIN = 1;
    private int FAMILY_MAX = 3;
    private int RELATIONSHIP_CHANCE = 15;
    private int FAMILY_CHANCE = 5;
    #endregion

    #region PROPERTIES
    public enum Sexuality { hetero, homo, bi, asexual };
    public enum Sex { male, female, herma };
    public enum Gender { masculine, feminine, andro, transM, transF };
    #endregion

    // list of sprites
    public string name;
    public Sex sex;
    public Sexuality sexuality;
    public Gender gender;
    public int wealth;
    public int age;
    public int intelligence;
    public int attractiveness;
    public bool relationship;
    public bool family;
    public int numberOfFamily;
    public bool alive;
    public Person significantOther;
    public int masculinity;
    public int femininity;
    
    private void Start()
    {

    }

    public void Initialize()
    {
        alive = true;

        if (Random.Range(0, 100) < RELATIONSHIP_CHANCE)
        {
            relationship = true;
            if (Random.Range(0, 100) < FAMILY_CHANCE)
            {
                family = true;
                numberOfFamily = Random.Range(FAMILY_MIN, FAMILY_MAX);
            }
            else
            {
                family = false;
            }
        }
        else
        {
            relationship = false;
        }

        wealth = WealthSolver();
        GenerateOrientation();
    }

    private void Update()
    {
    }

    private int WealthSolver()
    {
        int w = 0;
        float x = Random.Range(0.0f, 100.0f);
        //float a = (100 - ((x * x) / 100));
        //float b = a * a * a * a * a;
        //float c = b / 100000000;

        float a = x * x * x * x * x * x;
        float c = a / 10000000000;
        w = Mathf.FloorToInt(c);
        return w;
    }

    private void GenerateOrientation()
    {
        int x = Random.Range(0, 100);
        if (x < 55)
        {
            sex = Sex.male;
        }
        else
        {
            sex = Sex.female;
        }
        int hermaChance = Random.Range(0, 10000);
        if (hermaChance == 0)
        {
            sex = Sex.herma;
        }

        sexuality = Sexuality.hetero;
        x = Random.Range(0, 100);
        if (x < 20)
        {
            sexuality = Sexuality.homo;
        }
        else if (x >= 20 && x < 30)
        {
            sexuality = Sexuality.bi;
        }
        else if (x >= 30 && x < 32)
        {
            sexuality = Sexuality.asexual;
        }

        int r = Random.Range(0, 100);
        masculinity = r * r / 100;
        r = Random.Range(0, 100);
        femininity = r * r / 100;

        if (Mathf.Abs(masculinity - femininity) < 10)
        {
            gender = Gender.andro;
        }
        else if (masculinity > femininity)
        {
            gender = Gender.masculine;
        }
        else if (femininity > masculinity)
        {
            gender = Gender.feminine;
        }
    }

    public void Kill()
    {
        alive = false;
    }
}