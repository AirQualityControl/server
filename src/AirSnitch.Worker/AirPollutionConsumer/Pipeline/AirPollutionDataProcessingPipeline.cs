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
            
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = false, };
            _pipelineHead.LinkTo(updateStationBlockInstance, linkOptions);
            updateStationBlockInstance.LinkTo(acknowledgeMessageBlockMessageBlockInstance, linkOptions);
        }

        public void PostMessage(Message message)
        {
            _pipelineHead.Post(message);
        }
    }
}