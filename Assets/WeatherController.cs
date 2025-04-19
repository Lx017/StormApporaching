using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace GameDesign
{
    public class WeatherController : MonoBehaviour
    {
        public Volume volume; // Assign this in Inspector
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (volume.profile.TryGet<Fog>(out var fog))
        {
                //print out all the properties of the fog component
                foreach (var field in fog.GetType().GetFields())
                {
                    Debug.Log($"{field.Name}: {field.GetValue(fog)}");
                }
                // // Now you can access and modify the values
                // fog.fogAttenuationDistance.overrideState = true;
                // fog.fogAttenuationDistance.value = 100f; // Change to your desired value

                // // Example: change the color too
                // fog.color.overrideState = true;
                // fog.color.value = Color.cyan;
            }
            else
            {
                Debug.LogWarning("Fog component not found in Volume Profile.");
            }
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
