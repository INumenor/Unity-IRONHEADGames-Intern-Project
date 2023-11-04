using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{

    public static NetworkManager Instance { get; private set; }
    public NetworkRunner SessionRunner { get; private set; }

    [SerializeField]
    private GameObject _runnerPrefab;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {



        StartSharedSession("1234");

    }
    public async void StartSharedSession(string SessionName = "")
    {
        if(SessionName == "")
        {
            SessionName = GenereateSessionCode();
        }
        else
        {
            if(SessionName.Length != 4)
            {
                Debug.LogError("Wrong SEssion Name");
                return;
            }
        }
        //Create Runner
        CreateRunner();
        //Load Scene
        await LoadScene();
        //ConnectSession
        await Connect(SessionName);
    }
    public async Task LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }
    }
    public void CreateRunner()
    {
        SessionRunner = Instantiate(_runnerPrefab,transform).GetComponent<NetworkRunner>();

        SessionRunner.AddCallbacks(this);
    }

    private async Task Connect(string SessionName)
    {
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = SessionName,
            SceneManager = GetComponent<NetworkSceneManagerDefault>(),
            Scene = 1 
         
        };

        var result = await SessionRunner.StartGame(args);

        if (result.Ok)
        {
            Debug.Log("StartGame successfull");
        }
        else
        {
            Debug.LogError(result.ErrorMessage);
        }

       
    }
    string GenereateSessionCode(int length = 4)
    {
        char[] chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789".ToCharArray();
        string code = "";
        for (int i = 0; i < length; i++)
        {
            code += chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        Debug.Log("Session Code: " + code);
        return code;
    }




    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("A new player joined to the session");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
      
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
      
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
      
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("Runner Shutdown");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
      
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
      
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
      
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
      
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
      
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
      
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
      
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
      
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
      
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
      
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
      
    }
}
