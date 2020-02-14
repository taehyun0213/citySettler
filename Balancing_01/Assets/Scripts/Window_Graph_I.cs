using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph_I : MonoBehaviour
{
    [SerializeField] Sprite happinessSprite;
    [SerializeField] Sprite wealthSprite;
    [SerializeField] Sprite populationSprite;
    [SerializeField] Sprite pollutionSprite;
    [SerializeField] Sprite famineSprite;
    [SerializeField] Sprite employmentSprite;

    RectTransform graphContainer;
    RectTransform labelTemplateX;
    RectTransform labelTemplateY;
    RectTransform dashTemplateX;
    RectTransform dashTemplateY;

    GameManager _buildingDatabase;

    public List<float> happiness_s = new List<float>();
    public List<float> wealth_s = new List<float>();
    public List<float> population_s = new List<float>();
    public List<float> pollution_s = new List<float>();
    public List<float> famine_s = new List<float>();
    public List<float> employment_s = new List<float>();

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        _buildingDatabase = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //_buildingDatabase.Running();
            happiness_s.Add(_buildingDatabase._happiness_f);
            ShowGraph(happiness_s);
            wealth_s.Add(_buildingDatabase._wealth_f);
            ShowGraph(wealth_s);
            population_s.Add(_buildingDatabase._population_f);
            ShowGraph(pollution_s);
            pollution_s.Add(_buildingDatabase._pollution_f);
            ShowGraph(pollution_s);
            famine_s.Add(_buildingDatabase._famine_f);
            ShowGraph(famine_s);
            employment_s.Add(_buildingDatabase._employment_f);
            ShowGraph(employment_s);
        }
    }

    GameObject CreateHappiness(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));  
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = happinessSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreateWealth(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = wealthSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreatePopulation(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image)); 
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = populationSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreatePollution(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));  
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = pollutionSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreateFamine(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image)); 
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = famineSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreateEmployment(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));  
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = employmentSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition + new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }



    void ShowGraph(List<float> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }

        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = valueList[0];
        float yMinumum = valueList[0];

        foreach (float value in valueList)
        {
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinumum)
            {
                yMinumum = value;
            }
        }

        float xSize = 60.0f;

        GameObject lastCircleGameObject = null;

        if (valueList == happiness_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreateHappiness(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;

                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -20f);
                labelX.GetComponent<Text>().text = getAxisLabelX(i + 1);

                RectTransform dashX = Instantiate(dashTemplateX);
                dashX.SetParent(graphContainer);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, -20f);
            }

            int separatorCount = 10;
            for (int i = 0; i <= separatorCount; i++)
            {
                RectTransform labelY = Instantiate(labelTemplateY);
                labelY.SetParent(graphContainer);
                labelY.gameObject.SetActive(true);
                float normalizedValue = i * 1f / separatorCount;
                labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = getAxisLabelY(yMinumum + (normalizedValue * (yMaximum - yMinumum)));

                RectTransform dashY = Instantiate(dashTemplateY);
                dashY.SetParent(graphContainer);
                dashY.gameObject.SetActive(true);
                dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            }
        }
        else if (valueList == wealth_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreateWealth(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        else if (valueList == population_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreatePopulation(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        else if (valueList == pollution_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreatePollution(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        else if (valueList == famine_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreateFamine(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        else if (valueList == employment_s)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = ((valueList[i] - yMinumum) / (yMaximum - yMinumum) * graphHeight);
                GameObject circleGameObject = CreateEmployment(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
    }

    void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
