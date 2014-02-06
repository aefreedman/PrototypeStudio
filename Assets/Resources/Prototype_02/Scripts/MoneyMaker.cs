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
    
    private void Start()
    {
        target = left;
    }

    private void Update()
    {
        float newPos = Mathf.Lerp(shuttle.transform.position.x, target.transform.position.x, speed * Time.deltaTime);
        shuttle.transform.position = new Vector3(newPos, shuttle.transform.position.y, shuttle.transform.position.z);

        if (Mathf.Abs(shuttle.transform.position.x - target.transform.position.x) <= proxTolerance)
        {
            if (target == left)
            {
                MakeMoney(target.transform.position, shuttleMakesThisMuch);
                target = right;
            }
            else
            {
                MakeMoney(target.transform.position, shuttleMakesThisMuch);
                target = left;
            }
        }
    }

    public void MakeMoney(Vector3 pos, float amount)
    {
        if (amount > 0)
        {
            GameObject.Instantiate(moneyPrefab, pos, Quaternion.identity);
            MMGameManager.instance.MakeDollars(amount);
            audio.PlayOneShot(audio.clip);
        }
    }
}