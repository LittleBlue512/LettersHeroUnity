using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 1;

    private GameObject fireFX;
    private GameObject fireWorkFX;
    private ParticleSystem fireParticleSystem;
    private ParticleSystem fireWorkParticleSystem;


    void Start()
    {
        initialize();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy this object, if it hit anything.
        DestroyFireBall();
    }

    private void initialize()
    {
        fireFX = this.transform.GetChild(0).gameObject;
        fireWorkFX = this.transform.GetChild(1).gameObject;

        fireParticleSystem = fireFX.GetComponent<ParticleSystem>();
        fireWorkParticleSystem = fireWorkFX.GetComponent<ParticleSystem>();
    }

    private bool isOutCameraView()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.y > Screen.height)
            return true;
        return false;
    }

    private void DestroyFireBall()
    {
        // Activate fireWorkFX.
        fireWorkFX.SetActive(true);

        // Detach the child game object.
        fireFX.transform.parent = null;
        fireWorkFX.transform.parent = null;

        // Disable particle emission.
        ParticleSystem.EmissionModule fireFX_em = fireParticleSystem.emission;
        fireFX_em.enabled = false;

        // Get time before destroy the particle systems.
        float fireFX_duration = fireParticleSystem.main.startLifetime.constant;
        float fireWorkFX_duration = fireParticleSystem.main.startLifetime.constant;

        // Destroy particles.
        Destroy(fireFX, fireFX_duration);
        Destroy(fireWorkFX, fireWorkFX_duration);

        // Destroy parent object.
        Destroy(this.gameObject);
    }
}
