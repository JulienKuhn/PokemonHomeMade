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
        PlayerPokemon.color = Color.red;
        yield return null;
        PlayerPokemon.DOColor(Color.white,.5f).SetEase(Ease.InCirc);
        PlayerPokemon.transform.DOScale(1, 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.2f);
        PlayerInfoPanelContainter.DOScaleX(1, .5f);
        yield return new WaitForSeconds(.6f);
        PlayerInnerCanvas.DOFade(1, .4f);
    }
    private IEnumerator DOOpponentOpening()
    {
        OpponentPokemon.color = Color.red;
        yield return null;
        OpponentPokemon.DOColor(Color.white, .5f).SetEase(Ease.InCirc);
        OpponentPokemon.transform.DOScale(1, 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.2f);
        OpponentInfoPanelContainter.DOScaleX(1, .5f);
        yield return new WaitForSeconds(.6f);
        OpponentInnerCanvas.DOFade(1, .4f);
    }

    private IEnumerator DoBlink(Image img)
    {
        yield return new WaitForSeconds(.5f);
        img.DOFade(0f, .4f).SetEase(Ease.InOutElastic);
        yield return new WaitForSeconds(.45f);
        img.DOFade(1, .4f).SetEase(Ease.InOutElastic);
    }

    private IEnumerator DoPlayerATK()
    {
        float x = PlayerPokemon.transform.position.x;
        float y = PlayerPokemon.transform.position.y;

        PlayerPokemon.transform.DOMoveX(x + x / 5, .5f).SetEase(Ease.InCirc);
        PlayerPokemon.transform.DOMoveY(y + y / 5, .5f).SetEase(Ease.InCirc);
        yield return new WaitForSeconds(.6f);
        PlayerPokemon.transform.DOMoveX(x, .5f).SetEase(Ease.OutCirc);
        PlayerPokemon.transform.DOMoveY(y, .5f).SetEase(Ease.OutCirc);
    }
    private IEnumerator DoOpponentATK()
    {
        float x = OpponentPokemon.transform.position.x;
        float y = OpponentPokemon.transform.position.y;

        OpponentPokemon.transform.DOMoveX(x - x / 5, .5f).SetEase(Ease.InCirc);
        OpponentPokemon.transform.DOMoveY(y - y / 5, .5f).SetEase(Ease.InCirc);
        yield return new WaitForSeconds(.6f);
        OpponentPokemon.transform.DOMoveX(x, .5f).SetEase(Ease.OutCirc);
        OpponentPokemon.transform.DOMoveY(y, .5f).SetEase(Ease.OutCirc);
    }

    public void ChangeOpponentLifeAmount(float newLifeAmount, bool isHealing = false)
    {
        OpponentLifeBar.DOFillAmount(newLifeAmount, 1);
        OpponentLifeBar.transform.DOShakePosition(2, isHealing ? .3f : 5);

        if (!isHealing)
        {
            StartCoroutine(DoBlink(OpponentPokemon));
            StartCoroutine(DoPlayerATK());
        }
    }

    public void ChangePlayerLifeAmount(float newLifeAmount, bool isHealing = false)
    {
        PlayerLifeBar.DOFillAmount(newLifeAmount, 1);
        PlayerLifeBar.transform.DOShakePosition(2, isHealing ? .3f : 5);
        if (!isHealing)
        {
            StartCoroutine(DoBlink(PlayerPokemon));
            StartCoroutine(DoOpponentATK());
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
