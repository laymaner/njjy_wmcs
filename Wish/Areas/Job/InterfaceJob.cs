using Quartz;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core.Support.Quartz;
using Wish.ViewModel.Interface.InterfaceSendBackVMs;

namespace Wish.Areas.Job
{
    [QuartzRepeat(10, 0, true)]
    [QuartzJob("Interface")]
    [QuartzGroup("Wtm_WMS")]
    [DisallowConcurrentExecution]
    public class InterfaceJob : WtmJob
    {
        public override async Task<Task> Execute(IJobExecutionContext context)
        {
            InterfaceSendBackVM interfaceSendBackVM = Wtm.CreateVM<InterfaceSendBackVM>();
            await interfaceSendBackVM.SendInfoToInter();
            return Task.CompletedTask;
        }
    }
}
