using UnityEngine;

public class ParticleEmitterOH : MonoBehaviour
{
    public static ParticleEmitterOH shared;
    private ParticleSystem ps;

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        ps = GetComponent<ParticleSystem>();
        ParticleEmitterOH.shared = this;
    }

    public void EmitParticles(Vector3 position, int count)
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;

        // Emit multiple particles with randomized velocities
        for (int i = 0; i < count; i++)
        {
            emitParams.velocity = Random.insideUnitSphere * 5f; // Random velocity
            emitParams.startLifetime = Random.Range(0.3f, 0.6f); // Random lifetime
            emitParams.startSize = Random.Range(0.04f, 0.25f); // Random size
            emitParams.startColor = Color.red; // why is this not working?

            ps.Emit(emitParams, 1);
        }
    }
}
