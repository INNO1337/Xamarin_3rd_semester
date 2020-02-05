using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Water_delivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public Good goods = new Good();
        public bool pushed = false;
        public OrderPage()
        {

            InitializeComponent();
            goods.goods.Add("water", 110);
            goods.goods.Add("beer", 500);
            goods.goods.Add("cocacola", 650);
            picker.Items.Add("water");
            picker.Items.Add("beer");
            picker.Items.Add("cocacola");
        }

        void add_to_cart(object sender, EventArgs e)
        {
            goods.name = picker.SelectedItem.ToString();
            goods.quantity = (UInt64)plus_minus.Value;
            goods.price = goods.quantity * (UInt64)goods.goods[picker.SelectedItem.ToString()];
            pushed = true;
            Navigation.PopAsync();
        }

        void quantity(object sender, ValueChangedEventArgs e)
        {
            price.Text = (goods.goods[picker.SelectedItem.ToString()] * e.NewValue).ToString() + " py6.";
            chislo.Text = e.NewValue.ToString() + " шт.";
            if (e.NewValue == 0)
            {
                add_to_cart_.IsEnabled = false;
            }
            else
            {
                add_to_cart_.IsEnabled = true;
            }
        }

        void menu(object sender, EventArgs e)
        {
            goods.name = picker.Items[picker.SelectedIndex];
            photo_.Source = goods.name + ".jpg";
            plus_minus.IsEnabled = true;
        }
    }
}