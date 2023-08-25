using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private StageProgress _stageProgress;

    private void Start()
    {
        _stageProgress = StageProgress.GetInstance();    
    }

    private void Update()
    {
        _progressBar.fillAmount = _stageProgress.GetProgressPercentage();
    }
}
