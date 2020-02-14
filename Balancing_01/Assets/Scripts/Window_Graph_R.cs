using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph_R : MonoBehaviour
{
    [SerializeField] Sprite steelSprite;
    [SerializeField] Sprite goldSprite;
    [SerializeField] Sprite foodSprite;

    RectTransform graphContainer;
    RectTransform labelTemplateX;
    RectTransform labelTemplateY;
    RectTransform dashTemplateX;
    RectTransform dashTemplateY;

    GameManager _buildingDatabase;

    public List<int> steels = new List<int>();
    public List<int> golds = new List<int>();
    public List<int> foods = new List<int>();

    int maxRun = 0;

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
            maxRun++;

            if (maxRun <= 51)
            {
                _buildingDatabase.Running();

                steels.Add(_buildingDatabase._steel);
                ShowGraph(steels);
                golds.Add(_buildingDatabase._gold);
                ShowGraph(golds);
                foods.Add(_buildingDatabase._food);
                ShowGraph(foods);
            }
            else
            {
                Debug.Log("You cannot run the rounds more than 51!");
            }
        }
    }

    GameObject CreateSteel(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = steelSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreateGold (Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = goldSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    GameObject CreateFood(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = foodSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(40, 40);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY= null)
    {
        if(getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }

        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 500.0f;
        float xSize = 60.0f;        

        GameObject lastCircleGameObject = null;

        if (valueList == steels)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
                GameObject circleGameObject = CreateSteel(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;

                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -40f);
                labelX.GetComponent<Text>().text = getAxisLabelX(i + 1);

                RectTransform dashX = Instantiate(dashTemplateX);
                dashX.SetParent(graphContainer);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, 0f);
            }

            int separatorCount = 20;
            for (int i = 0; i <= separatorCount; i++)
            {
                RectTransform labelY = Instantiate(labelTemplateY);
                labelY.SetParent(graphContainer);
                labelY.gameObject.SetActive(true);
                float normalizedValue = i * 1f / separatorCount;
                labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);

                RectTransform dashY = Instantiate(dashTemplateY);
                dashY.SetParent(graphContainer);
                dashY.gameObject.SetActive(true);
                dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            }
        }
        else if (valueList == golds)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
                GameObject circleGameObject = CreateGold(new Vector2(xPosition, yPosition));
                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }
                lastCircleGameObject = circleGameObject;
            }
        }
        else if (valueList == foods)
        {
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
                GameObject circleGameObject = CreateFood(new Vector2(xPosition, yPosition));
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
