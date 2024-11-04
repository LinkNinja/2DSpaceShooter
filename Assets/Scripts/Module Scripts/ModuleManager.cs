using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public enum ModuleType { Speed, Shield, Fire, Missile, Laser, Health }

    private Dictionary<ModuleType, int> fragmentCounts = new Dictionary<ModuleType, int>();
    private Dictionary<ModuleType, int> fragmentRequirements = new Dictionary<ModuleType, int>();
    private Dictionary<ModuleType, bool> moduleAssembled = new Dictionary<ModuleType, bool>();

    // Reference to the combined module script
    private CombinedModules combinedModules;

    void Start()
    {
        foreach (ModuleType type in System.Enum.GetValues(typeof(ModuleType)))
        {
            fragmentCounts[type] = 0;
            fragmentRequirements[type] = GetFragmentRequirement(type);
            moduleAssembled[type] = false; // Initialize as not assembled
        }

        // Get reference to the combined module script
        combinedModules = GetComponent<CombinedModules>();
    }

    // Inside ModuleManager.cs
    public void CollectFragment(ModuleType moduleType)
    {
        fragmentCounts[moduleType]++;
        Debug.Log("Collected " + moduleType + " fragment. Total: " + fragmentCounts[moduleType]);

        if (fragmentCounts[moduleType] >= fragmentRequirements[moduleType])
        {
            AssembleModule(moduleType);
        }

        // Update UI
        FindObjectOfType<FragmentUIManager>().UpdateUI();
    }


    private int GetFragmentRequirement(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Speed:
                return 3;
            case ModuleType.Shield:
                return 3;
            case ModuleType.Fire:
                return 3;
            case ModuleType.Missile:
                return 5;
            case ModuleType.Laser:
                return 4;
            case ModuleType.Health:
                return 2;
            default:
                return 3;
        }
    }

    private void AssembleModule(ModuleType moduleType)
    {
        fragmentCounts[moduleType] = 0; // Reset fragment count after assembly
        moduleAssembled[moduleType] = true; // Mark module as assembled
        Debug.Log(moduleType + " module assembled!");
    }

    public int GetFragmentCount(ModuleType moduleType)
    {
        return fragmentCounts[moduleType];
    }

    public bool IsModuleAssembled(ModuleType moduleType)
    {
        return moduleAssembled[moduleType];
    }

    public void UseModule(ModuleType moduleType)
    {
        if (moduleAssembled[moduleType])
        {
            moduleAssembled[moduleType] = false; // Mark module as used
            Debug.Log(moduleType + " module used!");
            ActivateModule(moduleType);
        }
        else
        {
            Debug.Log("Module " + moduleType + " not assembled yet.");
        }
    }

    private void ActivateModule(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Speed:
                //FindObjectOfType<AudioManager>().Play("ShieldRecharge");
                StartCoroutine(combinedModules.ActivateSpeed());
                break;
            case ModuleType.Shield:
                
                StartCoroutine(combinedModules.ActivateShield());
                break;
            case ModuleType.Fire:
                StartCoroutine(combinedModules.ActivateFire());
                break;
            case ModuleType.Missile:
                StartCoroutine(combinedModules.ActivateMissile());
                break;
            case ModuleType.Laser:
                StartCoroutine(combinedModules.ActivateLaser());
                break;
            case ModuleType.Health:
                StartCoroutine(combinedModules.ActivateHealth());
                break;
        }
    }
}
