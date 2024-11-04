using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    public Slider chargeSlider;
    public ShipGun playerShooting;

    void Update()
    {
        chargeSlider.value = playerShooting.chargeCounter / playerShooting.chargeTime;
    }
}
