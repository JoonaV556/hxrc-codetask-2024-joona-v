using UnityEngine;

public class SpinnerSpeedModifier : MonoBehaviour
{
    [Tooltip("How much extra speed is added to spinner on this obstacle, per every 10 meters of height")]
    public float ExtraSpeed = 10f;

    private void Start()
    {
        float height = transform.position.y;
        int timesToAddSpeed = (int)(height / 10f);

        if (gameObject.TryGetComponent(out Spinner spinner) && timesToAddSpeed > 0)
        {
            if (spinner.SpinSpeed < 0f) // Take into account different spin directions
            {
                spinner.SpinSpeed -= (ExtraSpeed * timesToAddSpeed);
            }
            else
            {
                spinner.SpinSpeed += (ExtraSpeed * timesToAddSpeed);
            }
        }
    }
}
