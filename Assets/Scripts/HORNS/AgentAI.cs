using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentAI : MonoBehaviour, IDisplayable
{
    public string ClassName;
    private HORNS.Agent agent = new HORNS.Agent();
    public BasicAction CurrentAction { get; set; }
    public bool PerformedActionThisFrame { get; set; }

    private CancellationTokenSource source;
    private bool shouldRecalculate;
    private Task<HORNS.Action> aiTask;

    private string objectName;
    private AgentDisplay display;

    private PlanTimeStats timeStats;
    private bool skippedFirstTime;
    public bool InitializedActions { get; set; }

    private bool logTime;
    private TimeLogger timeLogger;

    public void EnableTimeLog(string filename)
    {
        timeLogger = new TimeLogger(filename);
        logTime = true;
    }

    public void DisableTimeLog()
    {
        logTime = false;
        timeLogger = null;
    }

    private void OnDestroy()
    {
        timeLogger?.Destroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(CommandLineParser.DisableCollisions)
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<NavMeshAgent>().radius = 0.01f;
        }

        source = new CancellationTokenSource();
        objectName = name;

        timeStats = FindObjectOfType<PlanTimeStats>();

        agent.SetRecalculateCallback((a) =>
        {
            if(skippedFirstTime)
            {
                Debug.Log($"Calculated plan for {objectName}: {a.PlannedActions.Count()} action{(a.PlannedActions.Count() > 1 ? "s" : "")}, Cost: {a.PlannedActions.Sum(ac => ac.CachedCost)} ({a.LastPlanTime.TotalMilliseconds}ms)");
                timeStats.AddTime(a.LastPlanTime.TotalMilliseconds, objectName);

                if(logTime)
                {
                    timeLogger.LogPlanTime(a.LastPlanTime.TotalMilliseconds);
                }
            }
            else
            {
                skippedFirstTime = true;
            }
        });
    }

    public void Init()
    {
        foreach (IDemoNeed need in gameObject.GetComponentsInChildren<IDemoNeed>())
        {
            need.AddTo(agent);
        }

        foreach (BasicAction action in gameObject.GetComponentsInChildren<BasicAction>())
        {
            if (action.IsIdle)
            {
                agent.AddIdleAction(action.CreateAction());
            }
            else
            {
                agent.AddAction(action.CreateAction());
            }
        }
    }

    private void Update()
    {
        if (!InitializedActions) return;
        if(aiTask != null && aiTask.IsCompleted)
        {
            aiTask.Result?.Perform();
            if(display != null)
            {
                if (display.transform == null)
                {
                    display = null;
                }
                else
                {
                    display.SetContent(agent.PlannedActions, agent.CurrentAction);
                }
            }
        }

        if (aiTask == null || aiTask.IsCanceled || aiTask.IsCompleted || aiTask.IsFaulted)
        {
            aiTask = HandleAI(source.Token);
        }
    }

    private Task<HORNS.Action> HandleAI(CancellationToken token)
    {
        return Task.Run(async () =>
        {
            try
            {

                bool recalculated = false;
                if (shouldRecalculate)
                {
                    await agent.RecalculateActionsAsync(source.Token);
                    shouldRecalculate = false;
                    recalculated = true;
                }
                if (CurrentAction == null || recalculated)
                {
                    return await GetNextActionAsync(token);
                }
            }
            catch (TaskCanceledException e) { }
            return null;
        });
    }

    private async Task<HORNS.Action> GetNextActionAsync(CancellationToken token)
    {
        try
        {
            HORNS.Action action = await agent.GetNextActionAsync(token);
            return action;
        }
        catch (TaskCanceledException)
        {
            return null;
        }
    }

    public void RecalculatePlan()
    {
        Debug.Log($"[{gameObject.name}] Recalc");
        if (source == null)
        {
            //We didn't even start calculations, probably someone called us too early as the reaction to variable value initialization
            return;
        }
        shouldRecalculate = true;
        source.Cancel();
        source = new CancellationTokenSource();
    }

    public void CancelAction()
    {
        CurrentAction?.Cancel();
    }

    public LayoutGroup GetComponent()
    {
        LayoutGroup canvas = FindObjectOfType<UIProvider>().AgentPrefab;
        var go = Instantiate(canvas);
        display = go.GetComponent<AgentDisplay>();
        display.AgentAI = this;
        return go;
    }
}
