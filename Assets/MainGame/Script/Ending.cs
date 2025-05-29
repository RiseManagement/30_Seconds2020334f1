using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ending :MonoBehaviour
{
    [SerializeField] GameObject escapedBothPanel, escapedOneSidePanel, noOneEscapedBothPanel;
    
    int endingNo = 0;
    public bool IsEndingEnd
    {
        get
        {
            bool endf = false;
            if(endingNo == 1)
            {
                endf = true;
            }

            return endf;
        }
    }
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
            endingNo = 1;
        }
        else if (isEscapedOneSide)
        {
            escapedBothPanel.SetActive(false);
            escapedOneSidePanel.SetActive(true);
            noOneEscapedBothPanel.SetActive(false);
            endingNo = 0;
        }
        else if(isNoOneEscaped)
        {
            escapedBothPanel.SetActive(false);
            escapedOneSidePanel.SetActive(false);
            noOneEscapedBothPanel.SetActive(true);
            endingNo = 0;
        }
    }
    public void OnClickTitleButton()
    {
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.TITLE);
    }
}
