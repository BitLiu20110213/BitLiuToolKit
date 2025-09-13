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
        public const string ModGUID = "BitLiu.BitLiuToolKit";
        public const string ModName = "BitLiuToolKit";
        public const string ModVer = "1.0";

        public override bool Load()
        {
            string str = @"
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
#               BitLiuToolKit 1.0:A creative Mod                  #
#                     BitLiu  (C)  2025                           #
#           The Tool Kit is made for EVERYONE to use!             #
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
";
            string Stay = @"
I do the same thing I told you that I never would

I told you I changed, even when I knew I never could

Know that I can't find nobody else as good as you

I need you to stay, need you to stay, hey


I get drunk, wake up, I'm wasted still

I realize the time that I wasted here

I feel like you can't feel the way I feel

I'll be **** up if you can't be right here


Oh, whoa

Oh, whoa

Oh, whoa

I'll be **** up if you can't be right here


I do the same thing I told you that I never would

I told you I changed, even when I knew I never could

Know that I can't find nobody else as good as you

I need you to stay, need you to stay, hey

I do the same thing I told you that I never would

I told you I changed, even when I knew I never could

Know that I can't find nobody else as good as you

I need you to stay, need you to stay, hey


When I'm away from you, I miss your touch

You're the reason I believe in love

It's been difficult for me to trust

And I'm afraid that I'ma **** it up

Ain't no way that I can leave you stranded

Cause you ain't never left me empty-handed

And you know that I know that I can't live without you

So, baby, stay


Oh, whoa

Oh, whoa

Oh, whoa

I'll be **** up if you can't be right here


I do the same thing I told you that I never would

I told you I changed, even when I knew I never could

Know that I can't find nobody else as good as you

I need you to stay, need you to stay, hey

I do the same thing I told you that I never would

I told you I changed, even when I knew I never could

Know that I can't find nobody else as good as you

I need you to stay, need you to stay, hey


I need you to stay, need you to stay, hey
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Stay);
            Console.ResetColor();
            LogDebug("Loading Executables...");
            ExecutableManager.RegisterExecutable<BitLiuTool>("#BITLIU_TOOL#");
            ExecutableManager.RegisterExecutable<SSLFastTool>("#SSL_FAST#");
            PortManager.RegisterPort("BitLiuPort", "BitLiu Backdoor", 213);
            return true;
        }

        private void LogDebug(string message)
        {
            Log.LogDebug(message);
        }
    }
}