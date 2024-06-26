using UnityEngine;

public class HumanManager
{
    public Transform mainHuman; // Reference to the main human
    public HumanDetector[] humanDetectors; // Array of HumanDetector instances

    public HumanManager(Transform mainHuman, HumanDetector[] humanDetectors)
    {
        this.mainHuman = mainHuman;
        this.humanDetectors = humanDetectors;
    }

    public void Initialize()
    {
        // Set the main human for each HumanDetector instance
        foreach (HumanDetector detector in humanDetectors)
        {
            detector.SetMainHuman(mainHuman);
        }
    }
}
