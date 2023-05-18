using System;
using System.Collections.Generic;
using System.Text;
using ABI_RC.Core.Networking;
using ABI_RC.Core.Savior;
using ABI_RC.Systems.ModNetwork;
using DarkRift;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Kafe.NetworkTest;

public class NetworkTest : MelonMod {


    private static readonly Guid ModGuid = Guid.Parse("5affbc06-d288-f39b-6fd0-b54a51b86612");

    public override void OnInitializeMelon() {
        ModConfig.InitializeMelonPrefs();
    }

    private static void OnMessage(ModNetworkMessage msg) {
        msg.Read(out string readString);
        MelonLogger.Msg($"data [str]: {readString}");
    }

    private static void SendMsgToSpecificPlayers(string msg, List<string> playerGuids) {
        using var modMsg = new ModNetworkMessage(ModGuid, playerGuids.ToArray());
        modMsg.Write(msg);
        modMsg.Send();
    }

    private static void SendMsgToAllKb(int bytes) {

        using var modMsg = new ModNetworkMessage(ModGuid);

        // Sending a string with bytes
        var sb = new StringBuilder();
        for (var i = 0; i < bytes / 2; i++) {
            sb.Append("A");
        }

        modMsg.Write(sb.ToString());
        modMsg.Send();
    }

    private static void SendMsgToAll(string msg) {
        using var modMsg = new ModNetworkMessage(ModGuid);
        modMsg.Write(msg);
        modMsg.Send();
    }

    public override void OnUpdate() {

        if (Input.GetKeyDown(KeyCode.P)) {
            if (NetworkManager.Instance == null || NetworkManager.Instance.GameNetwork.ConnectionState != ConnectionState.Connected) {
                MelonLogger.Warning($"Attempted to send a game network messaged without being connected to an online instance...");
                return;
            }
            MelonLogger.Msg("Sending everyone important info! P");
            SendMsgToAll($"[{AuthManager.username}] I like Peanuts!");
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            if (NetworkManager.Instance == null || NetworkManager.Instance.GameNetwork.ConnectionState != ConnectionState.Connected) {
                MelonLogger.Warning($"Attempted to send a game network messaged without being connected to an online instance...");
                return;
            }

            if (AuthManager.username == "Lop") {
                MelonLogger.Msg("Ignoring because is Lop Sending...");
                return;
            }

            MelonLogger.Msg("Sending Lop important info! L");
            var a = new [] { "5affbc06-d288-f39b-6fd0-b54a51b86612" };
            SendMsgToSpecificPlayers($"[{AuthManager.username}] I like Specific!", new List<string> {"5affbc06-d288-f39b-6fd0-b54a51b86612"});
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            if (NetworkManager.Instance == null || NetworkManager.Instance.GameNetwork.ConnectionState != ConnectionState.Connected) {
                MelonLogger.Warning($"Attempted to send a game network messaged without being connected to an online instance...");
                return;
            }
            MelonLogger.Msg($"Sending everyone important info! G -> Headers + {ModConfig.MeStringSize.Value} Bytes");
            SendMsgToAllKb(ModConfig.MeStringSize.Value);
        }

        if (Input.GetKeyDown(KeyCode.Home)) {
            MelonLogger.Msg($"Current Instance: {MetaPort.Instance.CurrentInstanceId}");
        }
    }

    [HarmonyPatch]
    internal class HarmonyPatches {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.Awake))]
        public static void After_NetworkManager_Awake(NetworkManager __instance) {
            try {
                MelonLogger.Msg($"Subscribed to the mod network with: {ModGuid.ToString()}...");
                ModNetworkManager.Subscribe(ModGuid, OnMessage);
            }
            catch (Exception e) {
                MelonLogger.Error($"Error during the patched function {nameof(After_NetworkManager_Awake)}");
                MelonLogger.Error(e);
            }
        }
    }
}
