using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {
    //-----------------------------------Банально обьявляю кнопки 
    //Список всех поинтов в базе
    public List<Point> points = new List<Point>();
    public Dictionary<Vector3, Point> pointList = new Dictionary<Vector3, Point>();
    //Список всех кнопок в базе
    public List<Buttons> buttonsList = new List<Buttons>();
    //скрипт контроллера свайпов
    public SwipeControl controll = null;

    //ссылка на класс рекламы:
    //public UnityADSJ0schi unityADS = null;
    int restartCount = 1;

    public static Base Instance = null;

    public GameObject win = null;

    //шаги по x и z
    public float xStep;
    public float zStep;

    //текущая выбранная кнопка
    public Buttons selectedButton = null;
    //позиция в которую требуется передвинуть нашу кнопку
    public Vector3 testVector = Vector3.zero;
    public Vector3 nextPosition = Vector3.zero;
    //двигается ли в текущий момент кнопка
    public bool IsMoving = false;
    //скорость перемещения кнопки
    float speed = 10f;

    //тип расстановки цифр требуемый для победы
    public sortType sort;
    public enum sortType {
        wosr,
        ubiv,
    }

    void Awake() {
        Instance = this;
        for (int i = 0; i < points.Count; i++) {
                pointList.Add(points[i].gameObject.transform.position, points[i]);
        }
        //Debug.Log(pointList.Count);
    }

    void Update() {
        
        //Если кнопка которую мы собираемся двигать уже выбрана:
        if (selectedButton != null && Base.Instance.IsMoving) {
            //Выполняем перемещение нашей кнопки
                move(nextPosition);
        }
    }

    public void ButtonMove()
    {
        //Обрабатываем свайпы:
        switch(controll.swipeDirection)
        {
            case SwipeControl.SwipeDirection.Up:
                Debug.Log("Переместить вверх.");
                moveUp();
                break;
            case SwipeControl.SwipeDirection.Down:
                moveDown();
                break;
            case SwipeControl.SwipeDirection.Left:
                moveLeft();
                break;
            case SwipeControl.SwipeDirection.Right:
                moveRight();
                break;
        }
    }

    //метод определяет победили ли мы
    bool winner() {
        bool result = true;
        for (int i = 0; i < buttonsList.Count; i++) {
            Buttons sel = buttonsList[i];
            if(sel.recuiredPosition != pointList[sel.myPointPosition])
            {
                return false;
            }
        }
        return result;
    }

    //Метод срабатывает при нажатии на кнопку старт:
    public void restart() {
        Debug.Log("Tach to restart.");
        //каждый второй раз пытаемся запустить рекламу:
        //if (restartCount % 2 ==0) {
        //    unityADS.schoowADS();
        //}
        //выключаем кубок
        win.SetActive(false);
        //сортируем случайным образом кнопки
        Point randomPoint = null;
        Buttons randomButton = null;
        List<Buttons> sortedButtons = new List<Buttons>();
        List<Point> sortedPoints = new List<Point>(); 
        bool sort = true;
        //формируем список кнопок которые необходимо отсортировать
        for (int i = 0; i < buttonsList.Count; i++)
        {
            sortedButtons.Add(buttonsList[i]);
        }
        for (int j = 0; j < points.Count; j++)
        {
            sortedPoints.Add(points[j]);
        }

        for(int i = 0;i < buttonsList.Count; i++ ) {
            randomPoint = sortedPoints[getRandomInt(sortedPoints.Count)];
            randomButton = buttonsList[i];
            randomPoint.empty = false;
            //ставим кнопку на ннужное место:
            randomButton.transform.position = randomPoint.pointPosition;
            randomButton.myPointPosition = randomPoint.pointPosition;
            randomButton.rend.material.color = randomButton.myColor;
            //Удаляем point чтобы он не участвовал в рандоме.
            sortedPoints.Remove(randomPoint);

            if(i == buttonsList.Count - 1) {
                //делаем оставшийся поинт свободным
                sortedPoints[0].empty = true;
                //очищаем текущее выделение:
                selectedButton = null;
                nextPosition = Vector3.zero;
            }
        }

        /*
        while (sort) {
            //теперь делаем от цикла к циклу следующее:
            if (sortedButtons.Count > 0)
            {
                //итак мы приступили к сортировке(получаем первый случайный элемент):
                randomButton = sortedButtons[getRandomInt(sortedButtons.Count)];
                randomPoint = sortedPoints[getRandomInt(sortedPoints.Count)];
                //засовываем случайные поинт с кнопкой 
                sortedButtons.Remove(randomButton);
                sortedPoints.Remove(randomPoint);
                randomPoint.empty = false;
                //ставим кнопку на ннужное место:
                randomButton.transform.position = randomPoint.pointPosition;
                randomButton.myPointPosition = randomPoint.pointPosition;
                randomButton.rend.material.color = randomButton.myColor;
            }
            else {
                //делаем оставшийся поинт свободным
                sortedPoints[0].empty = true;
                //очищаем текущее выделение:
                selectedButton = null;
                nextPosition = Vector3.zero;
                //выходим из зацикливания:
                sort = false;
            }
        }
        restartCount += 1;
        */
    }

    //метод возвращает случайный инт
    public int getRandomInt(int count) {
        int result = (int)Random.Range(0, count);
        return result;
    }

//-----------------------------------------------------------------Перемещение по методам--------------------------(ВНИМАНИЕ! так-как расположено во второй четверти кординат знаки другие!)
    public void moveUp() {

        Debug.Log("Свайп вверх старт!");
        //теперь если выполнен свайп вверх мы просто проверяем:
        if (selectedButton != null)
        {
            testVector = selectedButton.myPointPosition;
            testVector.z -= zStep;
            //есть ли вообще такой поинт:
            if(pointList.ContainsKey(testVector))
            {
                //теперь проверяем а не занят ли он
                if(pointList[testVector].empty)
                {
                    //то перемещаем сюда кнопку:
                    pointList[testVector].empty = false;
                    pointList[selectedButton.myPointPosition].empty = true;
                    nextPosition = pointList[testVector].pointPosition;
                    Base.Instance.IsMoving = true;
                }
                else {
                    Debug.Log("Свайп вверх невозможен клетка вверху занята.");
                }
            }
            else {
                Debug.Log("Свайп вверх невозможен нет такой позиции.");
            }
        }
    }

    public void moveDown()
    {
        //теперь если выполнен свайп вверх мы просто проверяем:
        if (selectedButton != null)
        {
            testVector = selectedButton.myPointPosition;
            testVector.z += zStep;
            //есть ли вообще такой поинт:
            if(pointList.ContainsKey(testVector))
            {
                //теперь проверяем а не занят ли он
                if(pointList[testVector].empty)
                {
                    //то перемещаем сюда кнопку:
                    pointList[testVector].empty = false;
                    pointList[selectedButton.myPointPosition].empty = true;
                    nextPosition = pointList[testVector].pointPosition;
                    Base.Instance.IsMoving = true;
                }
                else {
                    Debug.Log("Свайп вниз невозможен клетка вверху занята.");
                }
            }
            else {
                Debug.Log("Свайп вниз невозможен нет такой позиции.");
            }
        }
    }

    public void moveRight()
    {
        //теперь если выполнен свайп вверх мы просто проверяем:
        if(selectedButton != null)
        {
            testVector = selectedButton.myPointPosition;
            testVector.x -= zStep;
            //есть ли вообще такой поинт:
            if(pointList.ContainsKey(testVector))
            {
                //теперь проверяем а не занят ли он
                if(pointList[testVector].empty)
                {
                    //то перемещаем сюда кнопку:
                    pointList[testVector].empty = false;
                    pointList[selectedButton.myPointPosition].empty = true;
                    nextPosition = pointList[testVector].pointPosition;
                    Base.Instance.IsMoving = true;
                }
            }
            else {
                Debug.Log("Свайп вправо невозможен клетка вверху занята.");
            }
        }
        else {
            Debug.Log("Свайп вправо невозможен нет такой позиции.");
        }
    }

    public void moveLeft()
    {
        //теперь если выполнен свайп вверх мы просто проверяем:
        if(selectedButton != null)
        {
            testVector = selectedButton.myPointPosition;
            testVector.x += zStep;
            //есть ли вообще такой поинт:
            if(pointList.ContainsKey(testVector))
            {
                //теперь проверяем а не занят ли он
                if(pointList[testVector].empty)
                {
                    //то перемещаем сюда кнопку:
                    pointList[testVector].empty = false;
                    pointList[selectedButton.myPointPosition].empty = true;
                    nextPosition = pointList[testVector].pointPosition;
                    Base.Instance.IsMoving = true;
                }
            }
            else {
                Debug.Log("Свайп влево невозможен клетка слева занята.");
            }
        }
        else {
            Debug.Log("Свайп влево невозможен нет такой позиции.");
        }
    }


    //Выполнение перемещения к точке:
    void move(Vector3 nextPosition)
    {
        if (selectedButton != null) {
            if (selectedButton.transform.position != nextPosition)
            {
                IsMoving = true;
                selectedButton.transform.position = Vector3.MoveTowards(selectedButton.transform.position, nextPosition, speed * Time.deltaTime);
            }
            else
            {
                //перемещение закончено необходимо проверить а не выйграли ли мы
                selectedButton.myPointPosition = nextPosition;
                nextPosition = Vector3.zero;
                IsMoving = false;
                win.SetActive(winner());
            }
        }
    }
}
