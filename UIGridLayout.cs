using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridLayout : LayoutGroup {

    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    //TO DO: PADDING
    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();
        
        cellSize.x = (rectTransform.rect.width / columns) - ((spacing.x / columns) * (columns - 1)) - padding.left - padding.right;
        cellSize.y = (rectTransform.rect.height / rows) - ((spacing.y / rows) * (rows - 1)) - padding.top - padding.bottom;
        if (rows * columns < rectChildren.Count) {
            Debug.LogWarning("There is more children in UI Layout Group than cells!");
        }
        int itemsCount = 0;
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                itemsCount++;
                if (itemsCount > rectChildren.Count) break;
                var item = rectChildren[columns*row+column];
                float xPos = padding.left + (cellSize.x * column) + (spacing.x * column);
                float yPos = padding.top + (cellSize.y * row) + (spacing.y * row);

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }
       

    }

    public override void CalculateLayoutInputVertical() {
        cellSize.x = (rectTransform.rect.width / columns) - ((spacing.x / columns) * (columns - 1)) - padding.left - padding.right;
        cellSize.y = (rectTransform.rect.height / rows) - ((spacing.y / rows) * (rows - 1)) - padding.top - padding.bottom;
        if (rows * columns < rectChildren.Count) {
            Debug.LogWarning("There is more children in UI Layout Group than cells!");
        }
        int itemsCount = 0;
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                itemsCount++;
                if (itemsCount > rectChildren.Count) break;
                var item = rectChildren[columns*row+column];
                float xPos = padding.left + (cellSize.x * column) + (spacing.x * column);
                float yPos = padding.top + (cellSize.y * row) + (spacing.y * row);

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }
    }

    public override void SetLayoutHorizontal() {
        
    }

    public override void SetLayoutVertical() {
        
    }
}
