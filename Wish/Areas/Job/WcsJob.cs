using Quartz;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core.Support.Quartz;
using Wish.ViewModel.DevConfig.DeviceConfigVMs;

namespace Wish.Areas.Job
{
    public class WcsJob : WtmJob
    {
        public override Task Execute(IJobExecutionContext context)
        {
            DeviceConfigVM deviceConfigVM = Wtm.CreateVM<DeviceConfigVM>();
            //deviceConfigVM.Start();
            return Task.CompletedTask;
        }
    }
}
