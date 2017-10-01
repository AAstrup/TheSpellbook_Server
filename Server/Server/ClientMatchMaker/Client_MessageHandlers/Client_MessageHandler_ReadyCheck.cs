using System;
using System.Collections.Generic;

internal class Client_MessageHandler_ReadyCheck : IMessageHandlerCommandClient
{
    private MatchMakerClient mM_NetworkTransmitter;
    private Client client;
    private UpdateController updateController;
    public Message_ServerRequest_ReadyCheck readyCheck;
    private Counter counter;

    public Client_MessageHandler_ReadyCheck(UpdateController updateController,Client client,MatchMakerClient mM_NetworkTransmitter)
    {
        this.mM_NetworkTransmitter = mM_NetworkTransmitter;
        this.client = client;
        this.updateController = updateController;
    }

    public void Handle(object objdata)
    {
        var data = (Message_ServerRequest_ReadyCheck)objdata;
        readyCheck = data;
        var timesOutTargets = new List<Counter.timesOut>() {
            new Counter.timesOut(client.Dispose),
            new Counter.timesOut(mM_NetworkTransmitter.eventHandler.StartMenu)
        };
        counter = new Counter(data.duration,timesOutTargets);        
        mM_NetworkTransmitter.eventHandler.QueueReady(counter);
        updateController.Add(counter);
    }

    public void Update(float deltaTime)
    {
        counter.Update(deltaTime);
    }
}