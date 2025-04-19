using UnityEngine;
using System.Collections;
using GameDesign;
public class LightController : MonoBehaviour
{
    static LightController instance; // Singleton instance
    public Light lightningLight;         // Assign your directional light
    public float flashIntensity = 20f;   // Brightness during the lightning
    public float baseIntensity = 30f; // Base brightness of the light
    public bool triggerFlash = false; // Optional: trigger flash from inspecto
    public bool testDim = false; // Optional: trigger flash from inspector
    public bool testDark = false; // Optional: trigger flash from inspector
    public bool testSunny = false; // Optional: trigger flash from inspector
    public AudioSource lightningSound;   // Optional: assign a thunder sound
    public Color flashColor = new Color(1f, 1f, 1f); // Optional: color of the flash
    public Color baseColor = new Color(1f, 1f, 1f); // Base color of the light
    public float fadeFactor = 0.1f; // Factor to control the fade speed
    public float randomThunderFactor = 0.0f; // Factor to control the random thunder sound
    private Vector3 initialRotation; // Store the initial rotation of the light


    void Start()
    {
        instance = this; // Initialize the singleton instance
        initialRotation = transform.rotation.eulerAngles; // Store the initial rotation of the light
    }
    void Update()
    {
        if (testSunny) // Test sunny condition
        {
            LightController.sunny(); // Set to sunny
            testSunny = false; // Reset trigger after use
        }
        if (testDim) // Test dim condition
        {
            LightController.dim(); // Set to dim
            testDim = false; // Reset trigger after use
        }
        if (testDark) // Test dark condition
        {
            LightController.dark(); // Set to dark
            testDark = false; // Reset trigger after use
        }
        float randomNumber = Random.Range(0f, 1f); // Generate a random number between 0 and 1
        if (randomNumber < randomThunderFactor/10) // Check if the random number is less than the threshold
        {
            triggerFlash = true; // Trigger the flash
        }
        
        if (triggerFlash)
        {
            transform.rotation = Quaternion.Euler(initialRotation.x, initialRotation.y + Random.Range(-10f, 10f), initialRotation.z); // Randomly rotate the light
            SoundEffectPlayer.shared.PlaySoundEffect("thunder2", 1f); // Play thunder sound
            triggerFlash = false; // Reset trigger after use
            lightningLight.color = flashColor; // Set the flash color
            lightningLight.intensity = flashIntensity; // Set the flash intensity
        }
        float factor = fadeFactor;
        Color c = new Color(baseColor.r*factor+lightningLight.color.r*(1f-factor), baseColor.g*factor+lightningLight.color.g*(1f-factor), baseColor.b*factor+lightningLight.color.b*(1f-factor)); // Gradually return to base color
        lightningLight.color = c; // Gradually return to base color
        lightningLight.intensity = baseIntensity*factor + lightningLight.intensity*(1f-factor); // Gradually return to base intensity
    }
    public static void sunny()
    {
        instance.transform.rotation = Quaternion.Euler(instance.initialRotation); // Reset the rotation of the light
        instance.randomThunderFactor = 0f; // Trigger random thunder sound
        instance.baseIntensity = 30f; // Base brightness of the light
        instance.baseColor = new Color(1f,0.7f,0.7f); // Base color of the light
    }
    public static void dim(){
        instance.randomThunderFactor = 0.02f; // Trigger random thunder sound
        instance.baseIntensity = 10f; // Base brightness of the light
        instance.baseColor = new Color(1f,0.5f,0.3f); // Base color of the light
    }

    public static void dark()
    {
        instance.randomThunderFactor = 0.1f; // Trigger random thunder sound
        instance.baseIntensity = 0.1f; // Base brightness of the light
        instance.baseColor = new Color(1f,1f,1f); // Base color of the light
    }
}
