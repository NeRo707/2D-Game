using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
  [SerializeField] private Slider slider;

  public void updateHealthBar(float currentValue, float maxValue) {
    if (slider != null)
      slider.value = currentValue / maxValue;
    else
      Debug.LogWarning("HealthBar slider is not assigned in the inspector!");
  }

  void Update() {

  }

}
