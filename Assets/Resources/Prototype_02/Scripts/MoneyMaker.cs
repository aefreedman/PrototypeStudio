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
                Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -3);
                MakeMoney(pos, shuttleMakesThisMuch);
                target = right;
            }
            else
            {
                Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -3);
                MakeMoney(pos, shuttleMakesThisMuch);
                target = left;
            }
        }
    }

    public void MakeMoney(Vector3 pos, float amount)
    {
        if (MMGameManager.instance.MakeDollars(amount))
        {
            GameObject.Instantiate(moneyPrefab, pos, Quaternion.identity);
            audio.PlayOneShot(audio.clip);
            shuttleMakesThisMuch += shuttleAdd;
            shuttleMakesThisMuch *= shuttleMultiplier;
        }
    }
}