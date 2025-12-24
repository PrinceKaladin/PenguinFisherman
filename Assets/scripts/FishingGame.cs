using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishingGame : MonoBehaviour
{
    [Header("UI")]
    public Button fishingButton;
    public Slider fishingSlider;
    public GameObject endGamePanel;
    public Text successText;
    public Text scoretext;

    [Header("Rod")]
    public Image rodImage;
    public Sprite rodIdle;
    public Sprite rodInWater;

    [Header("Catch")]
    public Image catchImage;
    public Image catchImage2;
    public Sprite[] fishSprites;
    public Sprite[] trashSprites;

    [Header("Slider Settings")]
    public float sliderSpeed = 1.5f;
    public float successMin = 0.4f;
    public float successMax = 0.6f;

    private bool isFishing = false;
    private bool sliderMovingRight = true;
    private void Awake()
    {
        PlayerPrefs.SetInt("score", 0);
        if (!PlayerPrefs.HasKey("bestscore")) {
            PlayerPrefs.SetInt("bestscore", 0);
        }
    }
    void Start()
    {
        fishingSlider.gameObject.SetActive(false);
        catchImage.gameObject.SetActive(false);
        endGamePanel.SetActive(false);
        successText.gameObject.SetActive(false);

        rodImage.sprite = rodIdle;
        fishingButton.onClick.AddListener(OnFishingButton);
    }

    void Update()
    {
        if (isFishing)
        {
            MoveSlider();
        }
    }

    void OnFishingButton()
    {
        
        // ѕервый клик Ч закидываем удочку
        if (!isFishing && !fishingSlider.gameObject.activeSelf)
        {
            StartFishing();
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                GameObject.Find("clickkk").GetComponent<AudioSource>().Play();
            }
        }
        // ¬торой клик Ч провер€ем результат
        else if (isFishing)
        {
            CheckCatch();
        }
    }

    void StartFishing()
    {
        rodImage.sprite = rodInWater;
        fishingSlider.value = 0f;
        fishingSlider.gameObject.SetActive(true);
        isFishing = true;
    }

    void MoveSlider()
    {
        if (sliderMovingRight)
            fishingSlider.value += sliderSpeed * Time.deltaTime;
        else
            fishingSlider.value -= sliderSpeed * Time.deltaTime;

        if (fishingSlider.value >= 1f)
            sliderMovingRight = false;
        else if (fishingSlider.value <= 0f)
            sliderMovingRight = true;
    }

    void CheckCatch()
    {
        isFishing = false;
        fishingSlider.gameObject.SetActive(false);
        rodImage.sprite = rodIdle;

        float value = fishingSlider.value;
        fishingButton.gameObject.SetActive(false);
        if (value >= successMin && value <= successMax)
        {
            CatchFish();
        }
        else
        {
            CatchTrash();
        }
    }

    void CatchFish()
    {
        Sprite fish = fishSprites[Random.Range(0, fishSprites.Length)];
        catchImage.sprite = fish;
        catchImage2.sprite = fish;
        catchImage.gameObject.SetActive(true);

        int score = PlayerPrefs.GetInt("score", 0);
        int plusscore = Random.Range(1, 6);
        score += plusscore;
        PlayerPrefs.SetInt("score", score);
        scoretext.text = score.ToString();
        if (PlayerPrefs.GetInt("bestscore") < score) {
            PlayerPrefs.SetInt("bestscore", score);

        }

        StartCoroutine(SuccessRoutine(plusscore));
    }

    void CatchTrash()
    {
        if (PlayerPrefs.GetInt("sound")==1)
        {
            GameObject.Find("wrong").GetComponent<AudioSource>().Play();
        }
        Sprite trash = trashSprites[Random.Range(0, trashSprites.Length)];
        catchImage.sprite = trash;
        catchImage.gameObject.SetActive(true);

        endGamePanel.SetActive(true);
    }

    IEnumerator SuccessRoutine(int score)
    {
        successText.gameObject.SetActive(true);
        successText.text = "YOU CAUGHT A FISH!!!\n +" + score.ToString();
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject.Find("soundeffect").GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(3f);
        fishingButton.gameObject.SetActive(true);
        successText.gameObject.SetActive(false);
        catchImage.gameObject.SetActive(false);
    }
}
