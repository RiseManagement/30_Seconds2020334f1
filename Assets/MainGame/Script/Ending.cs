using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ending :MonoBehaviour
{
    [SerializeField] GameObject escapedBothPanel, escapedOneSidePanel, noOneEscapedBothPanel;
    void Start()
    {
        SetEnding();
    }
    void SetEnding()
    {
        bool isClearUserA = MainGameManager.isClearUserA;
        bool isClearUserB = MainGameManager.isClearUserB;

        bool isEscapedBoth= isClearUserA && isClearUserB;
        bool isEscapedOneSide= isClearUserA || isClearUserB;
        bool isNoOneEscaped = !isClearUserA && !isClearUserB;

        if (isEscapedBoth)
        {
            escapedBothPanel.SetActive(true);
            escapedOneSidePanel.SetActive(false);
            noOneEscapedBothPanel.SetActive(false);
        }
        else if (isEscapedOneSide)
        {
            escapedBothPanel.SetActive(false);
            escapedOneSidePanel.SetActive(true);
            noOneEscapedBothPanel.SetActive(false);
        }
        else if(isNoOneEscaped)
        {
            escapedBothPanel.SetActive(false);
            escapedOneSidePanel.SetActive(false);
            noOneEscapedBothPanel.SetActive(true);
        }
    }
    public void OnClickTitleButton()
    {
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.TITLE);
    }
}
