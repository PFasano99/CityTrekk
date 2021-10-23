using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavigatePages : MonoBehaviour
{
    public Image mainPage, previewPage, mapsPage, stopPage;
    [Space]
    public Button homeB;
    public Button itaB, engB;
    [Space]
    public Button itinerary1B;
    public Button itinerary2B, itinerary3B, itinerary4B, itinerary5B;
    [Space]
    public TextMeshProUGUI itinerary1T;
    public TextMeshProUGUI itinerary2T, itinerary3T, itinerary4T, itinerary5T;
    [Space]
    public Button stopListB;
    public TextMeshProUGUI stopListT;
    public Button nextStopB, prevStopB;
    public TextMeshProUGUI nextStopT, prevStopT;
    [Space]
    public string[] ItineraryNameITA;
    public string[] ItineraryNameENG;
    [Space]
    public string language = "ENG";
    public Image stopImage;
    public TextMeshProUGUI stopText;
    [Space]
    public EtineraryStop[] etinerary1, etinerary2, etinerary3, etinerary4, etinerary5;
    public EtineraryStop[] etinerary6, etinerary7, etinerary8, etinerary9, etinerary10;
    public EtineraryStop[] etinerary11, etinerary12, etinerary13, etinerary14, etinerary15;
    public EtineraryStop[] etinerary16, etinerary17, etinerary18, etinerary19, etinerary20;
    public EtineraryStop[] etinerary21, etinerary22, etinerary23, etinerary24, etinerary25;

    private EtineraryStop[] selectedEtinerary;
    private EtineraryStop[] possibleEtinerary1, possibleEtinerary2, possibleEtinerary3, possibleEtinerary4, possibleEtinerary5;
    
    private int stopCount = 0;
    [Space]
    public Button[] goToStopButton;
    public TextMeshProUGUI[] goToStopT;
    [Space]
    public Button directionsB;
    public TextMeshProUGUI directonsT;
    [Space]
    public TextMeshProUGUI preEtineraryT;
    public TextMeshProUGUI startFromEtineraryT;
    [Space]
    public Button[] selectRegionB;
    public TextMeshProUGUI[] selectRegionT;
    private int region = 0;
    [Space]
    public Sprite[] flags;
    public Image originalFlag;
    public Sprite[] schoolLogos;
    public Image schoolLogo;
    [Space]
    public ScrollRect scroll;
    private Coroutine scrollRoutine = null;
    public TextMeshProUGUI titloScroll;


    private void Awake()
    {        
        scrollRoutine = StartCoroutine(scrollCoroutine());
        changeLanguage();
        setPanelVisibility(mapsPage);
        engB.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.aspect = 9f / 16f;

        itaB.onClick.AddListener(delegate { language = "ITA"; changeLanguage(); engB.gameObject.SetActive(true); itaB.gameObject.SetActive(false); });
        engB.onClick.AddListener(delegate { language = "ENG"; changeLanguage(); itaB.gameObject.SetActive(true); engB.gameObject.SetActive(false); });
        homeB.onClick.AddListener(delegate { setPanelVisibility(mapsPage); });
        stopListB.onClick.AddListener(delegate { setPanelVisibility(previewPage); });

        itinerary1B.onClick.AddListener(delegate { setPanelVisibility(previewPage); showPreview(possibleEtinerary1); selectedEtinerary = possibleEtinerary1; stopCount = 0; });
        itinerary2B.onClick.AddListener(delegate { setPanelVisibility(previewPage); showPreview(possibleEtinerary2); selectedEtinerary = possibleEtinerary2; stopCount = 0; });
        itinerary3B.onClick.AddListener(delegate { setPanelVisibility(previewPage); showPreview(possibleEtinerary3); selectedEtinerary = possibleEtinerary3; stopCount = 0; });
        itinerary4B.onClick.AddListener(delegate { setPanelVisibility(previewPage); showPreview(possibleEtinerary4); selectedEtinerary = possibleEtinerary4; stopCount = 0; });
        itinerary5B.onClick.AddListener(delegate { setPanelVisibility(previewPage); showPreview(possibleEtinerary5); selectedEtinerary = possibleEtinerary5; stopCount = 0; });

        goToStopButton[0].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 1); });
        goToStopButton[1].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 2); });
        goToStopButton[2].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 3); });
        goToStopButton[3].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 4); });
        goToStopButton[4].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 5); });
        goToStopButton[5].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 6); });
        goToStopButton[6].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 7); });
        goToStopButton[7].onClick.AddListener(delegate { setPanelVisibility(stopPage); showStop(selectedEtinerary, 8); });

        nextStopB.onClick.AddListener(delegate { nextStop(selectedEtinerary); });
        prevStopB.onClick.AddListener(delegate { prevStop(selectedEtinerary); });

        directionsB.onClick.AddListener(delegate { openMaps(selectedEtinerary[stopCount - 1].stopURL); });

        selectRegionB[0].onClick.AddListener(delegate { setPanelVisibility(mainPage); region = 0; loadRegion();  originalFlag.sprite = flags[0]; schoolLogo.sprite = schoolLogos[0]; });
        selectRegionB[1].onClick.AddListener(delegate { setPanelVisibility(mainPage); region = 5; loadRegion();  originalFlag.sprite = flags[1]; schoolLogo.sprite = schoolLogos[1]; });
        selectRegionB[2].onClick.AddListener(delegate { setPanelVisibility(mainPage); region = 10; loadRegion(); originalFlag.sprite = flags[2]; schoolLogo.sprite = schoolLogos[2]; });
        selectRegionB[3].onClick.AddListener(delegate { setPanelVisibility(mainPage); region = 15; loadRegion(); originalFlag.sprite = flags[3]; schoolLogo.sprite = schoolLogos[3]; });
        selectRegionB[4].onClick.AddListener(delegate { setPanelVisibility(mainPage); region = 20; loadRegion(); originalFlag.sprite = flags[4]; schoolLogo.sprite = schoolLogos[4]; });
    }

    void changeLanguage()
    {
        if (language == "ITA")
        {
            itinerary1T.text = ItineraryNameITA[region + 0].ToUpper();
            itinerary2T.text = ItineraryNameITA[region + 1].ToUpper();
            itinerary3T.text = ItineraryNameITA[region + 2].ToUpper();
            itinerary4T.text = ItineraryNameITA[region + 3].ToUpper();

            stopListT.text = "Lista fermate";
            nextStopT.text = "Prossima fermata";
            prevStopT.text = "Fermata precedente";
            directonsT.text = "INDICAZIONI";
            preEtineraryT.text = "Preview itinerario"; 
            startFromEtineraryT.text = "Inizia dalla tappa:";
        }
        else if (language == "ENG")
        {
            itinerary1T.text = ItineraryNameENG[region + 0].ToUpper();
            itinerary2T.text = ItineraryNameENG[region + 1].ToUpper();
            itinerary3T.text = ItineraryNameENG[region + 2].ToUpper();
            itinerary4T.text = ItineraryNameENG[region + 3].ToUpper();

            stopListT.text = "Stops list";
            nextStopT.text = "Next stop";
            prevStopT.text = "Previous stop";
            directonsT.text = "DIRECTIONS";
            preEtineraryT.text = "Preview itinerary";
            startFromEtineraryT.text = "Start from:";
        }

        if (selectedEtinerary != null)
        {
            showPreview(selectedEtinerary);
            if(stopCount > 0)
            showStop(selectedEtinerary, stopCount);
        }
    }

    void setPanelVisibility(Image visiblePanel)
    {
        mainPage.gameObject.SetActive(false);
        previewPage.gameObject.SetActive(false);
        mapsPage.gameObject.SetActive(false);
        stopPage.gameObject.SetActive(false);

        visiblePanel.gameObject.SetActive(true);
    }

    void showPreview(EtineraryStop[] etinerary)
    {
        for (int i = 0; i < goToStopButton.Length; i++)
        {
            if (i < etinerary.Length)
            {             
                goToStopButton[i].gameObject.SetActive(true);
                if (language == "ITA")
                    goToStopT[i].text = "Start: "+ etinerary[i].stopTitleITA.ToUpper();
                else if (language == "ENG")
                    goToStopT[i].text = "Start: " + etinerary[i].stopTitleENG.ToUpper();
            }
            else
            {
                goToStopButton[i].gameObject.SetActive(false);
            }
        }
    }

    void showStop(EtineraryStop[] etinerary, int c)
    {
        scroll.verticalNormalizedPosition = .8f;
        stopCount = c;
        if (language == "ITA")
        {
            titloScroll.text = etinerary[c - 1].stopTitleITA.ToUpper();
            stopText.text = etinerary[c - 1].descriptionITA;            
        }
        else if (language == "ENG")
        {
            titloScroll.text = etinerary[c - 1].stopTitleENG.ToUpper();
            stopText.text = etinerary[c - 1].descriptionENG;

        }

        stopImage.sprite = etinerary[c - 1].stopImage;
    }

    void nextStop(EtineraryStop[] etinerary)
    {
        if (stopCount < etinerary.Length)
            showStop(etinerary, stopCount + 1);
    }
    void prevStop(EtineraryStop[] etinerary)
    {
        if (stopCount > 1)
            showStop(etinerary, stopCount - 1);
    }

    void openMaps(string url)
    {
        Application.OpenURL(url);
    }

    void loadRegion()
    {
        if (region == 0)
        {
            possibleEtinerary1 = etinerary1;
            possibleEtinerary2 = etinerary2;
            possibleEtinerary3 = etinerary3;
            possibleEtinerary4 = etinerary4;
            possibleEtinerary5 = etinerary5;

        }
        else if (region == 5)
        {
            possibleEtinerary1 = etinerary6;
            possibleEtinerary2 = etinerary7;
            possibleEtinerary3 = etinerary8;
            possibleEtinerary4 = etinerary9;
            possibleEtinerary5 = etinerary10;
        }
        else if (region == 10)
        {
            possibleEtinerary1 = etinerary11;
            possibleEtinerary2 = etinerary12;
            possibleEtinerary3 = etinerary13;
            possibleEtinerary4 = etinerary14;
            possibleEtinerary5 = etinerary15;
        }
        else if (region == 15)
        {
            possibleEtinerary1 = etinerary16;
            possibleEtinerary2 = etinerary17;
            possibleEtinerary3 = etinerary18;
            possibleEtinerary4 = etinerary19;
            possibleEtinerary5 = etinerary20;
        }
        else if (region == 20)
        {
            possibleEtinerary1 = etinerary21;
            possibleEtinerary2 = etinerary22;
            possibleEtinerary3 = etinerary23;
            possibleEtinerary4 = etinerary24;
            possibleEtinerary5 = etinerary25;
        }

        changeLanguage();
    }

    IEnumerator scrollCoroutine()
    {
        while (true)
        {
            if (scroll.verticalNormalizedPosition >= .8f)
            {
                scroll.verticalNormalizedPosition = .8f;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}

[Serializable]
public struct EtineraryStop
{
    public string stopTitleITA;
    [TextArea]
    public string descriptionITA;
    public string stopTitleENG;
    [TextArea]
    public string descriptionENG;
    public Sprite stopImage;
    public string stopURL;
}
