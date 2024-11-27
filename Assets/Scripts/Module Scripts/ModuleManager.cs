using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{

    // States for the power up. Power up will have a specific type assigned that will go into the module manager.
    public enum ModuleType { Speed, Shield, Fire, Missile, Laser, Health }

 
    //Dictionaries for module system
    private Dictionary<ModuleType, int> fragmentCounts = new Dictionary<ModuleType, int>();
    private Dictionary<ModuleType, int> fragmentRequirements = new Dictionary<ModuleType, int>();
    private Dictionary<ModuleType, bool> moduleAssembled = new Dictionary<ModuleType, bool>();

    // Reference to the combined module script
    private CombinedModules combinedModules;

    void Start()
    {
        // Set the dictionaries
        foreach (ModuleType type in System.Enum.GetValues(typeof(ModuleType)))
        {
            //Assign the the starting value for all the module fragments.
            fragmentCounts[type] = 0;

            //Assign required fragement amounts for module types in the dictionary
            fragmentRequirements[type] = GetFragmentRequirement(type);

            // None of the modules start off as assemebled in the dictionaries
            moduleAssembled[type] = false; 
        }

        // Get reference to the combined module script
        combinedModules = GetComponent<CombinedModules>();
    }

    // Function that runs when you collect a fragment.
    // Prevents the player from collecting more fragments than required.
    // Increments by one if the player can collect the fragment.
    // Checks if player has met the requirements for a module.
    // If the player meets the requirement the module is automatically assembeled but not activated.
    public void CollectFragment(ModuleType moduleType)
    {
        //Prevent player from going over required amount for collecting fragments.
        if(fragmentCounts[moduleType] < fragmentRequirements[moduleType])
        {
            //Increment fragment collection
            fragmentCounts[moduleType]++;
            Debug.Log("Collected " + moduleType + " fragment. Total: " + fragmentCounts[moduleType]);

            // Assemble the fragments if they have enough.
            if (fragmentCounts[moduleType] >= fragmentRequirements[moduleType])
            {
                // Run function that Automatically assmebles the module.
                AssembleModule(moduleType);
            }

            // Update UI to show amount of fragments collected.
            FindObjectOfType<FragmentUIManager>().UpdateUI();
        }

        
    }


    // Function that uses a switch to determine the amount each fragment requires to activate.
    private int GetFragmentRequirement(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Speed:
                return 3;
            case ModuleType.Shield:           
                return 5;
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

    //Assemble the module for the player.
    private void AssembleModule(ModuleType moduleType)
    {

        // Mark module as assembled
        moduleAssembled[moduleType] = true;
        Debug.Log(moduleType + " module assembled!");
    }


    //Public Function for the UI to get the fragment count.
    public int GetFragmentCount(ModuleType moduleType)
    {
        return fragmentCounts[moduleType];
    }
    
    /*
    public bool IsModuleAssembled(ModuleType moduleType)
    {
        return moduleAssembled[moduleType];
    }*/

    public void UseModule(ModuleType moduleType)
    {
        if (moduleAssembled[moduleType])
        {
            // Mark the module as used
            moduleAssembled[moduleType] = false;

            // Reset fragment count after assembly
            // Bug: Does not reset after pressing the key, only resets after you collect another power up.
            fragmentCounts[moduleType] = 0; 
            Debug.Log(moduleType + " module used!");
            ActivateModule(moduleType);
        }
        else
        {
            Debug.Log("Module " + moduleType + " not assembled yet.");
        }
    }

    // Function to activate modules while referencing the combined modules script and functions.
    private void ActivateModule(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Speed:       
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
