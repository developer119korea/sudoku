using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace view
{
    public class SquareCell : MonoBehaviour
    {
        public model.BoardCoordinate BoardCoorinate { get { return this._boardCoordinate; } }
        public Text numberValue_ = null;
        public Image backGroundImage_ = null;

        private model.BoardCoordinate _boardCoordinate = null;

        private Button _button = null;

        private int _packIndex = 0;
        private int _orderIdex = 0;
        private int _column = 0;
        private int _row = 0;

        public void Awake()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.size = DefineData.CELLSIZE;

            _button = this.gameObject.AddComponent<Button>();
        }

        public void Initialize(controller.GameController.OnClick onClickCell, int packIndex, int orderIndex)
        {
            _button.onClick.AddListener
            (
                delegate
                {
                    onClickCell(_boardCoordinate.column, _boardCoordinate.row);
                }
            );

            this._packIndex = packIndex;
            this._orderIdex = orderIndex;
            this._column = orderIndex % DefineData.MAX_COLUMN_COUNT;
            this._row = orderIndex / DefineData.MAX_ROW_COUNT;
            this._boardCoordinate = new model.BoardCoordinate(packIndex, this._column, this._row);
            //Debug.Log(string.Format("Name :{0} -  [{1}, {2}]", name, _boardCoordinate.column, _boardCoordinate.row));
            SetPosition(orderIndex);
        }

        public void UpdateTrim(string imageName, bool isZooming)
        {
            ChangeImage(imageName);

            if(isZooming)
            {
                ChangeScale(1.1f);
            }
            else
            {
                ChangeScale(1.0f);
            }
        }

        public void UpdateText(int numberValue)
        {
            numberValue_.text = numberValue.ToString();
            //numberValue_.text = string.Format("[{0}, {1}]", _boardCoordinate.column, _boardCoordinate.row);
        }

        public void UpdateText(string text) // ��ǥ ���� ��
        {
            numberValue_.text = text;
        } 

        private void ChangeScale(float scale)
        {
            this.transform.localScale = Vector3.one * scale;
        } 

        private void ChangeImage(string imageName)
        {
            Sprite image = Resources.Load(string.Format("Image/{0}", imageName), typeof(Sprite)) as Sprite;
            backGroundImage_.sprite = image;
        } 

        private void SetPosition(int orderIndex)
        {
            RectTransform imageRect = backGroundImage_.GetComponent<RectTransform>();
            imageRect.sizeDelta = DefineData.CELLSIZE;

            RectTransform textRect = numberValue_.GetComponent<RectTransform>();
            textRect.sizeDelta = DefineData.CELLSIZE;

            int column = _orderIdex % DefineData.MAX_COLUMN_COUNT;
            int row = _orderIdex / DefineData.MAX_ROW_COUNT;

            float startX = -((imageRect.sizeDelta.x * DefineData.MAX_COLUMN_COUNT) + (DefineData.MAX_COLUMN_COUNT + 1)) * 0.5f;
            float startY = ((imageRect.sizeDelta.y * DefineData.MAX_ROW_COUNT) + (DefineData.MAX_ROW_COUNT + 1)) * 0.5f;

            float posX = startX + (imageRect.sizeDelta.x * 0.5f) + ((imageRect.sizeDelta.x * column) + (column + 1));
            float posY = startY - (imageRect.sizeDelta.y * 0.5f) - ((imageRect.sizeDelta.y * row) - (row + 1));

            this.transform.localPosition = new Vector3(posX, posY, 0);
        }

    }
}