using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    [Header("Components")]
    [SerializeField] private Volume globalVolume;

    [Header("Cycle Settings")]
    [Range(0f, 24f)] public float timeOfDay = 12f;
    public float dayDurationInMinutes = 1f;

    [Header("Atmosphere")]
    public Gradient nightToDayColor; // Pour la teinte (Color Filter)
    public AnimationCurve exposureCurve; // Pour l'obscurité (Post Exposure)

    private ColorAdjustments colorAdjustments;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // On récupère l'override pour ne pas le chercher à chaque frame
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            // Optionnel : On instancie le profil pour ne pas modifier le fichier sur le disque
            globalVolume.profile = Instantiate(globalVolume.profile);
            globalVolume.profile.TryGet(out colorAdjustments);
        }
    }

    void Update()
    {
        UpdateClock();
        ApplyPostProcessing();
    }

    private void UpdateClock()
    {
        timeOfDay += (Time.deltaTime / (dayDurationInMinutes * 60f)) * 24f;
        if (timeOfDay >= 24f) timeOfDay = 0f;
    }

    private void ApplyPostProcessing()
    {
        if (colorAdjustments == null) return;

        float normalizedTime = timeOfDay / 24f;

        // 1. Modifier la teinte (Bleu nuit -> Blanc midi -> Orange soir)
        colorAdjustments.colorFilter.Override(nightToDayColor.Evaluate(normalizedTime));

        // 2. Modifier l'exposition (Assombrir la nuit)
        colorAdjustments.postExposure.Override(exposureCurve.Evaluate(normalizedTime));

        // 3. Désaturer légèrement la nuit pour un look plus "Game Boy Advance"
        float saturationValue = Mathf.Lerp(-40f, 0f, exposureCurve.Evaluate(normalizedTime) + 1f);
        colorAdjustments.saturation.Override(saturationValue);
    }
}
