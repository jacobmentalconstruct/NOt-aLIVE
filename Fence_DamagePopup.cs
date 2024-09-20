using UnityEngine;
using UnityEngine.UI;

public class Fence_DamagePopup : MonoBehaviour
{
    private Text damageText;
    private Color textColor;
    private float disappearTimer;
    private Vector3 moveVector;

    private void Awake()
    {
        damageText = GetComponent<Text>();
    }

    public void Setup(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        textColor = damageText.color;
        disappearTimer = 1f; // Popup duration
        moveVector = new Vector3(0, 1, 0); // Initial movement direction
    }

    private void Update()
    {
        // Move the popup upwards
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime; // Dampen movement

        // Fade out the popup
        disappearTimer -= Time.deltaTime;
        if (disappearTimer > 0.5f)
        {
            // Fade in
            float increaseAlpha = 2f;
            textColor.a += increaseAlpha * Time.deltaTime;
        }
        else
        {
            // Fade out
            float decreaseAlpha = 2f;
            textColor.a -= decreaseAlpha * Time.deltaTime;
        }
        damageText.color = textColor;

        // Destroy the popup after it fades out
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
