using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AgentCountOptions : MonoBehaviour
{
    public GameObject MainMenu;

    public Slider MerchantSlider;
    public TMP_Text MerchantText;

    public Slider WoodcutterSlider;
    public TMP_Text WoodcutterText;

    public Slider CarpenterSlider;
    public TMP_Text CarpenterText;

    public Slider FarmerSlider;
    public TMP_Text FarmerText;

    private void Update()
    {
        SpawnAgents.Params.MerchantCount = (int)MerchantSlider.value;
        MerchantText.text = $"Merchants: {SpawnAgents.Params.MerchantCount}";

        SpawnAgents.Params.WoodcutterCount = (int)WoodcutterSlider.value;
        WoodcutterText.text = $"Woodcutters: {SpawnAgents.Params.WoodcutterCount}";

        SpawnAgents.Params.CarpenterCount = (int)CarpenterSlider.value;
        CarpenterText.text = $"Carpenters: {SpawnAgents.Params.CarpenterCount}";

        SpawnAgents.Params.FarmerCount = (int)FarmerSlider.value;
        FarmerText.text = $"Farmers: {SpawnAgents.Params.FarmerCount}";
    }

    private void Start()
    {
        MerchantSlider.value = SpawnAgents.Params.MerchantCount;
        WoodcutterSlider.value = SpawnAgents.Params.WoodcutterCount;
        CarpenterSlider.value = SpawnAgents.Params.CarpenterCount;
        FarmerSlider.value = SpawnAgents.Params.FarmerCount;
    }

    public void Back()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
