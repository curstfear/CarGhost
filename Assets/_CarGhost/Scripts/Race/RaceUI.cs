using TMPro;
using UnityEngine;

public class RaceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _raceText;

    public void SetRace(int raceNumber)
    {
        _raceText.text = $"RACE {raceNumber}";
    }

    public void ShowRace()
    {
        _raceText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _raceText.gameObject.SetActive(false);
    }
}