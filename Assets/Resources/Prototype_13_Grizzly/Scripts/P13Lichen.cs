public class P13Lichen : P13Item
{
    protected override void Start()
    {
        base.Start();
        property = P13GameManager.Resources.Thirst;
        value = 5;
    }

    private void OnDrawGizmos()
    {}
}