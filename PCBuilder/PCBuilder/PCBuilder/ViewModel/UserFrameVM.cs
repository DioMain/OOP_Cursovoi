using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.ViewModel
{
    public class UserFrameVM : FrameViewModel<UserFrame, MainWindow>
    {
        public UserFrameVM(UserFrame owner, MainWindow window) : base(owner, window)
        {
        }
    }
}
