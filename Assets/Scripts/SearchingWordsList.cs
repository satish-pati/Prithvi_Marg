using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SearchingWordsList : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject searchingWordPrefab;
    public float offset = 0.0f;
    public int maxColumns = 5;
    public int maxRows = 4;

    private int _columns = 2;
    private int _rows;
    private int _wordNumber;
    private List<GameObject> _words = new List<GameObject>();
     void Start()
    {
        _wordNumber = currentGameData.selectedboardData.SearchWords.Count;
        if (_wordNumber < _columns)
        {
            _rows = 1;
        }
        else { CalculateColumnsAndRowsNumber(); }
        CreateWordObject();
        SetWordsPosition();
        GameEvents.SetTotalWords(currentGameData.selectedboardData.SearchWords.Count);
    }

    private void CalculateColumnsAndRowsNumber()
    {
        do
        {
            _columns++;
            _rows = _wordNumber / _columns;
        }
        while (_rows >= maxRows);
        if (_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordNumber / _columns;
        }
    }
    private bool TryIncreaseColumnNumber()
    {
        _columns++;
        _rows = _wordNumber / _columns;
        if (_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordNumber / _columns;
            return false;
        }
        if (_wordNumber % _columns == 0)
        {
            _rows++;
        }
        return true;
    }
    private void CreateWordObject()
    {
        var squareScale = GetSquareScale(new Vector3(1f, 1f, 0.1f));
        for (var index = 0; index < _wordNumber; index++)
        {
            _words.Add(Instantiate(searchingWordPrefab) as GameObject);
            _words[index].transform.SetParent(this.transform);
            _words[index].GetComponent<RectTransform>().localScale = squareScale;
            _words[index].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            _words[index].GetComponent<SearchingWords>().SetWord(currentGameData.selectedboardData.SearchWords[index].Word);
        }
    }
    private Vector3 GetSquareScale(Vector3 scale)
    {
        var finalScale = scale;
        var adjustment = 0.01f;
        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;
         if(finalScale.x <=0 | finalScale.y <= 0)
            {
                finalScale.x=adjustment;
                finalScale.y=adjustment;

                return finalScale;
            }
            
        
        }
        return finalScale;
    }
    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = searchingWordPrefab.GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();

        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.rect.width * targetScale.x + offset;
        squareSize.y = squareRect.rect.height * targetScale.y + offset;

        var totalSquaresHeight = squareSize.y * _rows;

        if (totalSquaresHeight > parentRect.rect.height)
        {
            while (totalSquaresHeight > parentRect.rect.height)
            {
                if (TryIncreaseColumnNumber())
                    totalSquaresHeight = squareSize.y * _rows;
                else
                    return true;
            }
        }

        var totalSquareWidth = squareSize.x * _columns;

        if (totalSquareWidth > parentRect.rect.width)
            return true;

        return false;
    }
    private void SetWordsPosition()
    {
        var squareRect = _words[0].GetComponent<RectTransform>();
        var wordOffset = new Vector2
        {
            x = squareRect.rect.width * squareRect.transform.localScale.x + offset,
            y = squareRect.rect.height * squareRect.transform.localScale.y + offset,
        };
        int colNumber = 0;
        int rowNumber = 0;
        var startPosition = GetFirstSquarePosition();

        foreach (var word in _words)
        {
            if (colNumber + 1 > _columns)
            {
                colNumber = 0;
                rowNumber++;
            }
            var positionX = startPosition.x + wordOffset.x * colNumber;
            var positionY = startPosition.y - wordOffset.y * rowNumber;
            word.GetComponent<RectTransform>().localPosition = new Vector2(positionX, positionY);
            colNumber++;
        }
}

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(x: 0f, transform.position.y);
        var squareRect = _words[0].GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();
        var squareSize = new Vector2(x: 0f, y: 0f);

        squareSize.x = squareRect.rect.width * squareRect.transform.localScale.x + offset;
        squareSize.y = squareRect.rect.height * squareRect.transform.localScale.y + offset;
        var shiftBy = (parentRect.rect.width - (squareSize.x * _columns)) / 2;
        startPosition.x = ((parentRect.rect.width - squareSize.x) / 2) * (-1);
        startPosition.x += shiftBy;
        startPosition.y = (parentRect.rect.height - squareSize.y) / 2;
        return startPosition;
    }

}
