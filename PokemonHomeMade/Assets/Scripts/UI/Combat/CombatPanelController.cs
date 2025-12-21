using DG.Tweening;
using System.Collections;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

public class CombatPanelController : MonoBehaviour
{
    [Header("Opponent Info Panel")]
    [SerializeField] private Transform OpponentInfoPanelContainter;
    [SerializeField] private CanvasGroup OpponentInnerCanvas;
    [SerializeField] private TextMeshProUGUI OpponentName;
    [SerializeField] private TextMeshProUGUI OpponentLevel;
    [SerializeField] private Image OpponentLifeBar;
    [SerializeField] private Image OpponentPokemon;

    [Header("Player Info Panel")]
    [SerializeField] private Transform PlayerInfoPanelContainter;
    [SerializeField] private CanvasGroup PlayerInnerCanvas;
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TextMeshProUGUI PlayerLevel;
    [SerializeField] private Image PlayerLifeBar;
    [SerializeField] private Image PlayerExpBar;
    [SerializeField] private Image PlayerPokemon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpponentInfoPanelContainter.DOScaleX(0, 0);
        PlayerInfoPanelContainter.DOScaleX(0, 0);
        OpponentInnerCanvas.alpha = 0;
        PlayerInnerCanvas.alpha = 0;
        OpponentPokemon.transform.DOScale(0, 0);
        PlayerPokemon.transform.DOScale(0, 0);
    }

    public void StartOpponentOpening(string opponentName, int opponentLevel, float opponentLifeAmount)
    {
        UpdateOpponentInfos(opponentName, opponentLevel, opponentLifeAmount);
        StartCoroutine(DOOpponentOpening());
    }

    public void StartPlayerOpening(string playerName, int playerLevel, float playerLifeAmount, float playerExpAmount)
    {
        UpdatePlayerInfos(playerName, playerLevel, playerLifeAmount, playerExpAmount);
        StartCoroutine(DOPlayerOpening());
    }

    private IEnumerator DOPlayerOpening()
    {
        PlayerPokemon.transform.DOScale(1, 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.2f);
        PlayerInfoPanelContainter.DOScaleX(1, .5f);
        yield return new WaitForSeconds(.6f);
        PlayerInnerCanvas.DOFade(1, .4f);
    }
    private IEnumerator DOOpponentOpening()
    {
        OpponentPokemon.transform.DOScale(1, 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.2f);
        OpponentInfoPanelContainter.DOScaleX(1, .5f);
        yield return new WaitForSeconds(.6f);
        OpponentInnerCanvas.DOFade(1, .4f);
    }

    public void ChangeOpponentLifeAmount(float newLifeAmount, bool isHealing = false)
    {
        OpponentLifeBar.DOFillAmount(newLifeAmount, 1);
        OpponentLifeBar.transform.DOShakePosition(1, isHealing ? .3f:1);

        if (!isHealing)
        {
            StartCoroutine(DoBlink(OpponentPokemon));
        }
    }

    private IEnumerator DoBlink(Image img)
    {
        img.DOFade(0, .7f).SetEase(Ease.InOutElastic);
        yield return new WaitForSeconds(.8f);
        img.DOFade(1, .7f).SetEase(Ease.InOutElastic);
    }

    public void ChangePlayerLifeAmount(float newLifeAmount, bool isHealing = false)
    {
        PlayerLifeBar.DOFillAmount(newLifeAmount, 1);
        PlayerLifeBar.transform.DOShakePosition(1, isHealing ? .3f : 1);
        if (!isHealing)
        {
            StartCoroutine(DoBlink(PlayerPokemon));
        }
    }
    public void ChangePlayerExpAmount(float newLifeAmount, bool doItSlowly = false)
    {
        PlayerExpBar.DOFillAmount(newLifeAmount, 1);
        PlayerExpBar.transform.DOShakePosition(1, doItSlowly ? .3f : 1);
    }

    public void UpdateOpponentInfos(string name, int level, float lifeAmount)
    {
        OpponentName.text = name;
        OpponentLevel.text = $"Niveau {level}";
        OpponentLifeBar.fillAmount = lifeAmount;
    }

    public void UpdatePlayerInfos(string name, int level, float lifeAmount, float expAmount)
    {
        PlayerName.text = name;
        PlayerLevel.text = $"Niveau {level}";
        PlayerLifeBar.fillAmount = lifeAmount;
        PlayerExpBar.fillAmount = expAmount;
    }

    public void RecallOpponentPokemon()
    {
        OpponentPokemon.transform.DOScale(0, 1).SetEase(Ease.InBounce);
    }

    public void RecallPlayerPokemon()
    {
        PlayerPokemon.transform.DOScale(0, 1).SetEase(Ease.InBounce);
    }
}
