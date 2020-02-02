using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cinemachine;

public class UIController : MonoBehaviour
{
    public Canvas AgentCanvasGroup;
    public Canvas TopPanel;
    public CinemachineVirtualCamera FollowCamera;
    private AgentUI agentUI;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TopPanel.enabled = !TopPanel.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            var agent = agentUI.GetSelectedAgent();
            if (agent != null)
            {
                FollowCamera.enabled = true;
                FollowCamera.Follow = agent;
                FollowCamera.LookAt = agent;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FollowCamera.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Period))
        {
            if(Time.timeScale <= 25)
            {
                Time.timeScale *= 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.Comma))
        {
            if(Time.timeScale > 0.1f)
            {
                Time.timeScale /= 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.Slash))
        {
            Time.timeScale = 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo);
                if (hit)
                {
                    GameObject go = hitInfo.transform.gameObject;
                    Clickable clickable = go.GetComponentInParent<Clickable>();
                    if (clickable != null)
                    {
                        clickable.OnClick(hitInfo.point);
                    }
                    else
                    {
                        DeselectAgent();
                    }
                }
                else
                {
                    DeselectAgent();
                }
            }
        }
    }

    private void DeselectAgent()
    {
        AgentCanvasGroup.enabled = false;
        agentUI.SelectAgent(null);
    }

    // Update is called once per frame
    void Start()
    {
        agentUI = AgentCanvasGroup.GetComponent<AgentUI>();
    }
}
