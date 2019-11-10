using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour {
    [SerializeField] Canvas staff = null;

    private void Start ( ) {
        staff.enabled = false;
    }
    public void StartBtnPress ( ) {
        SceneManager.LoadScene (1);
    }

    public void StaffBtnPress ( ) {
        staff.enabled = true;
    }

    public void BackBtnPress ( ) {
        staff.enabled = false;
    }
}
