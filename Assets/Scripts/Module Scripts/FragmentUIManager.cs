using UnityEngine;
using TMPro;

public class FragmentUIManager : MonoBehaviour
{
    public TMP_Text speedFragmentText;
    public TMP_Text shieldFragmentText;
    public TMP_Text fireFragmentText;
    public TMP_Text missileFragmentText;
    public TMP_Text laserFragmentText;
    public TMP_Text healthFragmentText;

    private ModuleManager moduleManager;

    void Start()
    {
        moduleManager = FindObjectOfType<ModuleManager>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (moduleManager != null)
        {
            speedFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Speed);
            shieldFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Shield);
            fireFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Fire);
            missileFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Missile);
            laserFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Laser);
            healthFragmentText.text = "" + moduleManager.GetFragmentCount(ModuleManager.ModuleType.Health);

       
        }
    }
}
