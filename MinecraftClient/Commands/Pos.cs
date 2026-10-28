using System;
using Brigadier.NET;
using Brigadier.NET.Builder;
using MinecraftClient.CommandHandler;

namespace MinecraftClient.Commands
{
    class Pos : Command
    {
        public override string CmdName { get { return "pos"; } }
        public override string CmdUsage { get { return "pos"; } }
        public override string CmdDesc { get { return Translations.cmd_pos_desc; } }

        public override void RegisterCommand(CommandDispatcher<CmdResult> dispatcher)
        {
            dispatcher.Register(l => l.Literal("help")
                .Then(l => l.Literal(CmdName)
                    .Executes(r => GetUsage(r.Source, string.Empty))
                )
            );

            dispatcher.Register(l => l.Literal(CmdName)
                .Executes(r => DoLogPos(r.Source))
                .Then(l => l.Literal("_help")
                    .Executes(r => GetUsage(r.Source, string.Empty))
                    .Redirect(dispatcher.GetRoot().GetChild("help").GetChild(CmdName)))
            );
        }

        private int GetUsage(CmdResult r, string? cmd)
        {
            return r.SetAndReturn(cmd switch
            {
#pragma warning disable format // @formatter:off
                _           =>  GetCmdDescTranslated(),
#pragma warning restore format // @formatter:on
            });
        }

        private int DoLogPos(CmdResult r)
        {
            McClient handler = CmdResult.currentHandler!;
            var p = handler.GetCurrentLocation();
            return r.SetAndReturn(CmdResult.Status.Done, $"{Translations.cmd_pos_current}: {p.X:F0}, {p.Y:F0}, {p.Z:F0}");
        }
    }
}
