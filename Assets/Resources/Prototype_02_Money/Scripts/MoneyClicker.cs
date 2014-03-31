using UnityEngine;

public class MoneyClicker : MonoBehaviour
{
    public GameObject moneyMaker;
    private MoneyMaker makerInstance;
    public float clickingMakesThisMuch;
    public float clickingAdd;
    public float clickingMultiplier;
    public float maxShake;

    private void Start()
    {
        makerInstance = moneyMaker.GetComponent<MoneyMaker>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (makerInstance.MakeMoney(new Vector3(transform.position.x, transform.position.y, -3), clickingMakesThisMuch))
            {
                IncrementClicker();
                MMGameManager.Instance.mainCamera.GetComponent<ScreenShake>().StartShake(0.1f + maxShake * Mathf.Clamp(MMGameManager.Instance.dollars, 0, 1000000.0f) / 1000000.0f, 0.1f + maxShake * Mathf.Clamp(MMGameManager.Instance.dollars, 0, 1000000.0f) / 1000000.0f);
                if (Random.Range(0, 100) == 0)
                {
                    MMGameManager.Instance.CreateEventMessage("Dolla dolla billz, y'all!", Color.yellow, 2.0f);
                }
            }
        }
    }

    private void IncrementClicker()
    {
        clickingMakesThisMuch += clickingAdd;
        clickingMakesThisMuch *= clickingMultiplier;
    }

    private void Update()
    {
    }
}