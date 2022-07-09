using System;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class AirPollutionDataProcessingPipeline
    {
        private TransformBlock<Message, ValueTuple<Message, MonitoringStation>> _pipelineHead;
        private readonly ValidateMessageBlock _validateMessageBlock;
        private readonly UpdateStationInfoBlock _updateStationInfoBlock;
        private readonly AcknowledgeMessageBlock _acknowledgeMessageBlock;

        public AirPollutionDataProcessingPipeline(ValidateMessageBlock validateMessageBlock, 
            UpdateStationInfoBlock updateStationInfoBlock, AcknowledgeMessageBlock acknowledgeMessageBlock)
        {
            _validateMessageBlock = validateMessageBlock;
            _updateStationInfoBlock = updateStationInfoBlock;
            _acknowledgeMessageBlock = acknowledgeMessageBlock;
            BuildPipeline();
        }
        private void BuildPipeline()
        {
            _pipelineHead = _validateMessageBlock.Instance;
            var updateStationBlockInstance = _updateStationInfoBlock.Instance;
            var acknowledgeMessageBlockMessageBlockInstance = _acknowledgeMessageBlock.Instance;
            
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            _pipelineHead.LinkTo(updateStationBlockInstance, linkOptions);
            updateStationBlockInstance.LinkTo(acknowledgeMessageBlockMessageBlockInstance, linkOptions);
        }

        public void PostMessage(Message message)
        {
            _pipelineHead.Post(message);
        }
    }
    
    //Name: General validation block -- [CPU Intensive block]
    //Description: Accepts an incoming message and checks all the preliminary basic validations and message structure
    //In case if everything is correct it emmit a newly created domain model (AirMonitoringStation.cs) with AirPollution and Particles included.

    //Name: Update Station Info Block -- [I/O bounded block]
    //Description: The following block receives a AirMonitoringStation.cs and perform UpSert operation in DB. 

    //Name: Acknowledge successfully processed messages -- [I/O bounded block]
    
    //Name: Move message to DLQ block -- [I/O bounded block]

}