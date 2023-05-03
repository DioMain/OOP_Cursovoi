using PCBuilder.Utilities;
using PCBuilder.View;
using PCBuilder.View.Frames;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCBuilder.ViewModel
{
    public class BasketFrameVM : FrameViewModel<BasketFrame, MainWindow>
    {
        private List<BasketItemVM> items;
        public List<BasketItemVM> Items { get { return items; } }

        public string TotalPrice
        {
            get
            {
                int total = 0;
                foreach (var item in Items)
                    total += item.PriceInt;

                return $"{total}$";
            }
        }

        public BasketFrameVM(BasketFrame owner, MainWindow window) : base(owner, window)
        {
            SyncData();
        }

        public void SyncData()
        {
            items = new List<BasketItemVM>();

            foreach (var item in BasketManager.Instance.Items)
            {
                items.Add(new BasketItemVM(item, this));
            }

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void RemoveItem(BasketItemVM item)
        {
            BasketManager.Instance.Remove(item.BasketItem);

            SyncData();
        }
    }
}
