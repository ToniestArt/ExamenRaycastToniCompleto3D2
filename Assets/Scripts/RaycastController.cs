using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
  public Text _uiText;
  private InputAction _clickInput;
  private InputAction _positionInput;
  private Vector2 _mousePosition;
  private float _initialSeconds = 5f;
  private float _remainingSeconds;
  private bool _timerFlag;
  private string _escenaPendiente;

  void Awake ()
    {
        _clickInput = InputSystem.actions["Attack"];
        _positionInput = InputSystem.actions["MousePosition"];

    }  

 void Update ()
    {
        Debug.Log("Update Inicializado.");
        if (_timerFlag)
        {
            Debug.Log("if _timerFlag True inicializado");
            _remainingSeconds -= Time.deltaTime;   
            _uiText.text = Mathf.CeilToInt(_remainingSeconds).ToString();

            if (_remainingSeconds <= 0f)
            {
                Debug.Log("SceneManeger LoadScene");
                _timerFlag = false;               
                SceneManager.LoadScene(_escenaPendiente);  
            }
        }

        if (_clickInput.WasPressedThisFrame())
        {
            Debug.Log("ClickInputWasPressed.");
            ShotRaycast();
        }
    }

void ShotRaycast()
    {
         Debug.Log("Void ShotRaycast Inicializada.");
    _mousePosition = _positionInput.ReadValue<Vector2>();
    Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "Cube1")
            {
                Debug.Log("Has impacatado con el Cubo 1.");
                _escenaPendiente = "Scene1";
                ReiniciarTimer();
                
            }
            if(hit.collider.name == "Cube2")
            {
                Debug.Log("Has impacatado con el Cubo 2.");
                _escenaPendiente = "Scene2";
                ReiniciarTimer();
                
            }
            if(hit.collider.name == "Sphere1")
            {
                Debug.Log("Has impacatado con el Esfera 1.");
                _escenaPendiente = "Scene3";
                ReiniciarTimer();
                
            }

            if(hit.collider.CompareTag("Objetos"))
            {
                Debug.Log("Has impacatado a un objeto del tag Objetos.");
                
            }

            if(hit.collider.gameObject.layer == 3)
            {
                Debug.Log("Has impactado contra un objeto de la layer 3.");
                
            }
        }
        Debug.Log("No has impactado en nada.");
    }

    void ReiniciarTimer ()
    {
        _remainingSeconds = _initialSeconds;
        _timerFlag = true;
    }
}