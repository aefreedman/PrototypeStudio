using UnityEngine;

public class MoneyClicker : MonoBehaviour
{

    public GameObject moneyMaker;
    private MoneyMaker makerInstance;
    public float clickingMakesThisMuch;
    
    private void Start()
    {
        makerInstance = moneyMaker.GetComponent<MoneyMaker>();
    }
    
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            makerInstance.MakeMoney(transform.position, clickingMakesThisMuch);
        }
    }
    

    private void Update()
    {
    }
}