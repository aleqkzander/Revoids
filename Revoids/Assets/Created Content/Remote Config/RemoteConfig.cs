using Unity.RemoteConfig;
using UnityEngine;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [Header("Leaderboard Settings")]
    public bool leaderboardDelete;
    public bool leaderboardEnabled;
    public string leaderboardLink;

    private LeaderboardManager leaderboardManager;

    [System.Obsolete]
    private void Awake()
    {
        leaderboardManager = GameObject.Find("LeaderboardManager").GetComponent<LeaderboardManager>();

        ConfigManager.FetchCompleted += FetchLeaderboardConfig;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    [System.Obsolete]
    public void FetchLeaderboardConfig(ConfigResponse response)
    {
        if (response.status == ConfigRequestStatus.Success)
        {
            leaderboardDelete = ConfigManager.appConfig.GetBool("leaderboardDelete");
            leaderboardEnabled = ConfigManager.appConfig.GetBool("leaderboardEnabled");
            leaderboardLink = ConfigManager.appConfig.GetString("leaderboardLink");
            Debug.Log("Config successfully loaded");
        }
        else
        {
            Debug.Log("Failing to load the config...");
        }
    }
}
