using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class OptionFrameVM : FrameViewModel<OptionFrame, MainWindow>
    {
        public OptionFrameVM(OptionFrame owner, MainWindow window) : base(owner, window)
        {
        }
    }
}
