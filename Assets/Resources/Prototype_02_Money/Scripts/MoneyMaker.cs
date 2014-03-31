using UnityEngine;

public class MoneyMaker : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject shuttle;
    public GameObject target;
    public float speed;
    public float proxTolerance;
    public GameObject moneyPrefab;
    public float shuttleMakesThisMuch;
    public float shuttleAdd;
    public float shuttleMultiplier;
    private MoneyClicker[] clickers;
    public float maxShake;

    private void Start()
    {
        target = left;
        clickers = GetComponentsInChildren<MoneyClicker>();
    }

    private void Update()
    {
        float newPos = Mathf.Lerp(shuttle.transform.position.x, target.transform.position.x, speed * Time.deltaTime);
        shuttle.transform.position = new Vector3(newPos, shuttle.transform.position.y, shuttle.transform.position.z);

        if (Mathf.Abs(shuttle.transform.position.x - target.transform.position.x) <= proxTolerance)
        {
            if (target == left)
            {
                Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -3);
                if (MakeMoney(pos, shuttleMakesThisMuch))
                {
                    IncrementShuttle();
                    MMGameManager.Instance.mainCamera.GetComponent<ScreenShake>().StartShake(0.1f + maxShake * Mathf.Clamp(MMGameManager.Instance.dollars, 0, 1000000.0f) / 1000000.0f, 0.1f + maxShake * Mathf.Clamp(MMGameManager.Instance.dollars, 0, 1000000.0f) / 1000000.0f);
                }
                target = right;
            }
            else
            {
                Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -3);
                if (MakeMoney(pos, shuttleMakesThisMuch))
                {
                    IncrementShuttle();
                }
                target = left;
            }
        }

        float x = 0;
        foreach (var m in clickers)
        {
            if (m.clickingMakesThisMuch > x)
            {
                x = m.clickingMakesThisMuch;
            }
        }
        foreach (var c in clickers)
        {
            c.clickingMakesThisMuch = x;
        }
    }

    private void IncrementShuttle()
    {
        shuttleMakesThisMuch += shuttleAdd;
        shuttleMakesThisMuch *= shuttleMultiplier;
    }

    public bool MakeMoney(Vector3 pos, float amount)
    {
        if (MMGameManager.Instance.MakeDollars(amount))
        {
            GameObject.Instantiate(moneyPrefab, pos, Quaternion.identity);
            audio.PlayOneShot(audio.clip);
            return true;
        }
        else
        {
            return false;
        }
    }
}