using UnityEngine;

public class MoneyClicker : MonoBehaviour
{

    public GameObject moneyMaker;
    private MoneyMaker makerInstance;
    public float clickingMakesThisMuch;
    public float clickingAdd;
    public float clickingMultiplier;
    
    private void Start()
    {
        makerInstance = moneyMaker.GetComponent<MoneyMaker>();
    }
    
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            makerInstance.MakeMoney(transform.position, clickingMakesThisMuch);
            clickingMakesThisMuch += clickingAdd;
            clickingMakesThisMuch *= clickingMultiplier;
        }
    }
    

    private void Update()
    {
    }
}