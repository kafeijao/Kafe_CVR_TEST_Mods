﻿using System;
using System.Reflection;
using Kafe.NetworkTest;
using Kafe.NetworkTest.Properties;
using MelonLoader;


[assembly: AssemblyVersion(AssemblyInfoParams.Version)]
[assembly: AssemblyFileVersion(AssemblyInfoParams.Version)]
[assembly: AssemblyInformationalVersion(AssemblyInfoParams.Version)]
[assembly: AssemblyTitle(nameof(Kafe.NetworkTest))]
[assembly: AssemblyCompany(AssemblyInfoParams.Author)]
[assembly: AssemblyProduct(nameof(Kafe.NetworkTest))]

[assembly: MelonInfo(
    typeof(NetworkTest),
    nameof(Kafe.NetworkTest),
    AssemblyInfoParams.Version,
    AssemblyInfoParams.Author,
    downloadLink: "https://github.com/kafeijao/Kafe_CVR_Mods"
)]
[assembly: MelonGame("Alpha Blend Interactive", "ChilloutVR")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.WINDOWS_X64)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.MONO)]
[assembly: MelonColor(ConsoleColor.Green)]
[assembly: MelonAuthorColor(ConsoleColor.DarkYellow)]

namespace Kafe.NetworkTest.Properties;
internal static class AssemblyInfoParams {
    public const string Version = "0.0.1";
    public const string Author = "kafeijao";
}
