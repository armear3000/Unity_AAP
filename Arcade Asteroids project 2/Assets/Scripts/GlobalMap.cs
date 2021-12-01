using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    public GameObject map;
    public GameObject ship;
    public Camera  main_camera;
    

    // Исходные размеры объекта фона
    private float background_original_size_x = 0;
    private float background_original_size_y = 0;

    // Направление движения
    private Vector3 move_vector;

    // Вспомогательные переменные
    private bool _mousePressed = false;
    private float _halfScreenWidth = 0;
    
    void Start()
    {
        // Стартовое направление движения
        move_vector = new Vector3(0, 1.5f, 0);
        
        // Исходные размеры фона
        SpriteRenderer sr = map.GetComponent<SpriteRenderer>();
        var original_size = sr.size;
        background_original_size_x = original_size.x;
        background_original_size_y = original_size.y;

        // Высота камеры равна ортографическому размеру
        float orthographic_size = main_camera.orthographicSize;
        // Ширина камеры равна ортографическому размеру, помноженному на соотношение сторон
        float screen_aspect = (float)Screen.width / (float)Screen.height;
        // Радиус окружности, описывающей камеру
        _spaceCircleRadius = Mathf.Sqrt(orthographicSize * screenAspect * orthographicSize * screenAspect + orthographicSize * orthographicSize);

        // Конечный размер фона должен позволять сдвинуться на один базовый размер фона в любом направлении + перекрыть радиус камеры также во всех направлениях
        sr.size = new Vector2(_spaceCircleRadius * 2 + _backgroundOriginalSizeX * 2, _spaceCircleRadius * 2 + _backgroundOriginalSizeY * 2);
        //sr.size = new Vector2(background_original_size_x * 2, background_original_size_y * 2);
    }

    void Update()
    {
        // Изменение направления движения по клику кнопки мыши
        if (Input.GetMouseButtonDown (0)) {
            _mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mousePressed = false;
        }
        
        if (_mousePressed)
        {
            // Направление поворота определяется в зависимости от стороны экрана, по которой произошёл клик
            int rotation = Input.mousePosition.x >= _halfScreenWidth ? -1 : 1;

            // Расчёт поворота вектора направления
            float xComp = (float)(_moveVector.x * Math.Cos(_rotationSpeed * rotation * Time.deltaTime) - _moveVector.y * Math.Sin(_rotationSpeed * rotation * Time.deltaTime));
            float yComp = (float) (_moveVector.x * Math.Sin(_rotationSpeed * rotation * Time.deltaTime) + _moveVector.y * Math.Cos(_rotationSpeed * rotation * Time.deltaTime));
            _moveVector = new Vector3(xComp, yComp,0);

            // Поворот спрайта корабля и камеры вдоль вектора направления
            float rotZ = Mathf.Atan2(_moveVector.y, _moveVector.x) * Mathf.Rad2Deg;
            ship.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        }

        // Сдвигаем фон в противоположном движению направлении
        space.transform.Translate(-_moveVector.x * Time.deltaTime, -_moveVector.y * Time.deltaTime, 0);

        // При достижении фоном сдвига равного исходному размеру фона в каком-либо направлении, возвращаем его в исходную точно по этому направлению
        if (space.transform.position.x >= _backgroundOriginalSizeX)
        {
            space.transform.Translate(-_backgroundOriginalSizeX, 0, 0);
        }
        if (space.transform.position.x <= -_backgroundOriginalSizeX)
        {
            space.transform.Translate(_backgroundOriginalSizeX, 0, 0);
        }
        if (space.transform.position.y >= _backgroundOriginalSizeY)
        {
            space.transform.Translate(0, -_backgroundOriginalSizeY, 0);
        }
        if (space.transform.position.y <= -_backgroundOriginalSizeY)
        {
            space.transform.Translate(0, _backgroundOriginalSizeY, 0);
        }
    }
    
    private void OnDrawGizmos()
    {
        // Окружность, описывающая камеру
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(Vector3.zero , Vector3.back, _spaceCircleRadius);

        // Направление движения
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawLine(Vector3.zero, _moveVector);
    }
}