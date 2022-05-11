using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UITextFlash : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private bool m_enabledByDefault;
    // Start is called before the first frame update

    private bool isFlashing = false;

    void Start()
    {
        m_text.enabled = true;
        if (m_enabledByDefault)
        {
            StartFlash();
        }
    }

    public void StopFlash()
    {
        CancelInvoke();
        isFlashing = false;
        m_text.enabled = false;
    }

    public void StartFlash()
    {
        if (isFlashing) return;

        if (m_text != null)
        {
            isFlashing = true;
            InvokeRepeating("ToggleFlashState", 0.5f, 0.5f);
        }
    }

    public void ToggleFlashState()
    {
        m_text.enabled = !m_text.enabled;
    }
}
