using BepInEx;
using BepInEx.Hacknet;
using Pathfinder.Executable;
using Pathfinder.Port;
using System;

namespace BitLiuToolKit
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class BitLiuToolKit : HacknetPlugin
    {
        public const string ModGUID = "com.BitLiu.BitLiuToolKit";
        public const string ModName = "BitLiuToolKit";
        public const string ModVer = "0.0.1";

        public override bool Load()
        {
            string str = @"
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
#               BitLiuToolKit 0.0.1:A useful Mod                  #
#                     BitLiu  (C)  2025                           #
#           The Tool Kit is especially made for you.              #
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();
            this.LogDebug("Loading Executables...");
            ExecutableManager.RegisterExecutable<BitLiuTool>("#BITLIU_TOOL#");
            PortManager.RegisterPort("BitLiuPort", "BitLiu Backdoor", 213);
            return true;
        }

        private void LogDebug(string message)
        {
            Log.LogDebug(message);
        }
    }
}