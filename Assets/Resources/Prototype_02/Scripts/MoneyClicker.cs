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
            MMGameManager.instance.mainCamera.GetComponent<ScreenShake>().StartShake(0.1f, 0.1f);
            if (Random.Range(0, 100) == 0)
            {
                MMGameManager.instance.CreateEventMessage("Dolla dolla billz, yall!", Color.yellow, 2.0f);
            }
        }
    }
    

    private void Update()
    {
    }
}