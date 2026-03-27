using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference footstepsEvent;
    private FMOD.Studio.EventInstance footsteps;

    private Rigidbody playerRB;

    void Start()
    {
        // znajdź Rigidbody w rodzicu (Player)
        playerRB = GetComponentInParent<Rigidbody>();

        // utwórz instancję FMOD
        footsteps = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
    }

    // TA FUNKCJA JEST WYWOŁYWANA PRZEZ ANIMATOR
    public void PlayFootsteps()
    {
        // zabezpieczenie żeby nie grało w miejscu
        if (playerRB != null && Mathf.Abs(playerRB.velocity.x) < 0.1f)
            return;

        if (!footsteps.isValid()) return;

        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        footsteps.start();
    }
}