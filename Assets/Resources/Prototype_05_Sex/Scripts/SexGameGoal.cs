using UnityEngine;

public class SexGameGoal : MonoBehaviour
{

    public SexBallGameManager.Team team;
    public GameObject ball;
    private SexBallGameManager gm;
    public int score;

    private void Start()
    {
        gm = SexBallGameManager.Instance as SexBallGameManager;        
        ball = gm.ball;
        score = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            gm.ScoreGoal(team);
        }
    }
}