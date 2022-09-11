using Content.Shared.Info;
using Robust.Shared.Log;

namespace Content.Client.Info;

public sealed class InfoSystem : EntitySystem
{
    public RulesMessage Rules = new RulesMessage("Server Rules", "The server did not send any rules.");
    [Dependency] private readonly RulesManager _rules = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeNetworkEvent<RulesMessage>(OnRulesReceived);
        Logger.DebugS("info", "Requested server info.");
        RaiseNetworkEvent(new RequestRulesMessage());
    }

    protected void OnRulesReceived(RulesMessage message, EntitySessionEventArgs eventArgs)
    {
        Logger.DebugS("info", "Received server rules.");
        Rules = message;
        _rules.UpdateRules();
    }
}