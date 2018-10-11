using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ServerManager : MonoBehaviour
{

    public class DataMessage : MessageBase
    {
        public string message;
    }

    public GameObject gameManager;
    public AdminUIManager adminUiMgr;
    int port = 5555;

    //Message types
    private const short MESSAGE_ID = 1000;
    private const short QUEUE_ID = 1001;

    int player1Id=0;
    int player2Id=0;
    List<eCommands> playerOneCommands = new List<eCommands>();
    List<eCommands> playerTwoCommands = new List<eCommands>();
    bool playerOneReady = false;
    bool playerTwoReady = false;

    void Start()
    {
        CreateServer();
    }

    void Update()
    {
        if (playerOneReady && playerTwoReady)
        {
            startMovement();
        }
    }

    //Setup server
    void CreateServer()
    {
        RegisterHandlers();

        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        var ht = new HostTopology(config, 5);

        if (!NetworkServer.Configure(ht))
        {
            Debug.Log("No server created, error on the configuration definition");
            return;
        }
        else
        {
            // Start listening on the defined port
            if (NetworkServer.Listen(port))
                Debug.Log("Server created, listening on port: " + port);
            else
                Debug.Log("No server created, could not listen to the port: " + port);
        }
    }

    void OnApplicationQuit()
    {
        NetworkServer.Shutdown();
    }

    private void RegisterHandlers()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
        
        // Custom stuff
        NetworkServer.RegisterHandler(MESSAGE_ID, OnMessageReceived);
        NetworkServer.RegisterHandler(QUEUE_ID, OnQueueReceived);
    }

    private void RegisterHandler(short t, NetworkMessageDelegate handler)
    {
        NetworkServer.RegisterHandler(t, handler);
    }

    void OnClientConnected(NetworkMessage netMessage)
    {
        DataMessage messageContainer = new DataMessage();
        if (player1Id == 0)
        {
            player1Id = netMessage.conn.connectionId;
            adminUiMgr.setPlayerIsConnected(ePlayers.ONE, true);
            Debug.Log("Player 1 connected");
            messageContainer.message = "Thanks for joining! You are Player 1";
        }
        else if (player2Id == 0)
        {
            player2Id = netMessage.conn.connectionId;
            adminUiMgr.setPlayerIsConnected(ePlayers.TWO, true);
            Debug.Log("Player 2 connected");
            messageContainer.message = "Thanks for joining! You are Player 2";
        }
        else
        {
            messageContainer.message = "No more players accepted";
        }

        // This sends a message to a specific client, using the connectionId
        NetworkServer.SendToClient(netMessage.conn.connectionId, MESSAGE_ID, messageContainer);

        // Send a message to all the clients connected
        messageContainer = new DataMessage();
        messageContainer.message = "A new player has connected to the server.";

        // Broadcast a message a to everyone connected
        NetworkServer.SendToAll(MESSAGE_ID, messageContainer);
    }

    void OnQueueReceived(NetworkMessage netMessage)
    {
        int playerId = netMessage.conn.connectionId;
        var objectMessage = netMessage.ReadMessage<DataMessage>();
        string msg = objectMessage.message;
        Debug.Log(msg);

        List<eCommands> commandList;
        if (playerId == player1Id)
        {
            commandList = playerOneCommands;
        }
        else if (playerId == player2Id)
        {
            commandList = playerTwoCommands;
        }
        else return;

        commandList.Clear();

        for (int i = 0; i<msg.Length;i++)
        {
            char c = msg[i];
            switch (c)
            {
                case 'U':
                    commandList.Add(eCommands.UP);
                    break;
                case 'D':
                    commandList.Add(eCommands.DOWN);
                    break;
                case 'R':
                    commandList.Add(eCommands.RIGHT);
                    break;
                case 'L':
                    commandList.Add(eCommands.LEFT);
                    break;
                case 'W':
                    commandList.Add(eCommands.NONE);
                    break;
            }
        }
        if (playerId == player1Id)
        {
            playerOneReady = true;
            Debug.Log("Player One is ready");
        }
        else if (playerId == player2Id)
        {
            playerTwoReady = true;
            Debug.Log("Player Two is ready");
        }
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        if (netMessage.conn.connectionId == player1Id)
        {
            player1Id = 0;
            Debug.Log("Player One is out");
            adminUiMgr.setPlayerIsConnected(ePlayers.ONE, false);
        }
        else if (netMessage.conn.connectionId == player2Id)
        {
            player2Id = 0;
            Debug.Log("Player Two is out");
            adminUiMgr.setPlayerIsConnected(ePlayers.ONE, false);
        }
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        var objectMessage = netMessage.ReadMessage<DataMessage>();
        Debug.Log("Message received: " + objectMessage.message);

    }

    public void startMovement()
    {
        gameManager.GetComponent<PlayerMovement>().moveTwoPlayers(playerOneCommands, playerTwoCommands);
        playerOneReady = false;
        playerTwoReady = false;
    }
}
