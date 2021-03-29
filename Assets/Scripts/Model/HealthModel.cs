namespace Model
{
    public class HealthModel : StatModel
    {
        public override void Enter(float currentStatValue, float maxStatValue)
        {
            StatBarName = "Health";
            base.Enter(currentStatValue, maxStatValue);
        }
    }
}
