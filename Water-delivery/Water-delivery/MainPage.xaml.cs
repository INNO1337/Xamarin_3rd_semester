using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Water_delivery
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Good good = new Good();
        public Dictionary<string, UInt64> list = new Dictionary<string, UInt64>();
        public bool added = false;
        List<string> drinks = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            good.goods.Add("water", 110);
            good.goods.Add("beer", 500);
            good.goods.Add("cocacola", 650);
            drinks.Add("water");
            drinks.Add("beer");
            drinks.Add("cocacola");
        }

        async void invoice(object sender, EventArgs e)
        {
            string str = "";
            foreach (var drink in list)
            {
                JsonItem jitem = new JsonItem(drink.Key, drink.Value, good.goods[drink.Key]);
                str += JsonConvert.SerializeObject(jitem) + "\n";
            }
            if (await DisplayAlert("Confirm your order.", str, "Confirm", "Cancel")) {
                invce.Children.Clear();
                list.Clear();
                order_.IsEnabled = false;
                await DisplayAlert("Заказ сформирован", "Ожидайте заказ", "Ок");
            }
        }

        void choose(object sender, EventArgs e)
        {
            OrderPage order = new OrderPage();
            order.Disappearing += (s, e_) =>
            {
                added = false;
                if (order.pushed)
                {
                    if (list.ContainsKey(order.goods.name))
                    {
                        list[order.goods.name] += order.goods.quantity;
                        invce.Children.Clear();
                        for (int i = 0; i < 3; i++)
                        {
                            add_to_cart(drinks[i], list[drinks[i]]);
                        }
                        added = true;
                        order_.IsEnabled = true;
                    }
                    else
                    {
                        list.Add(order.goods.name, order.goods.quantity);
                        add_to_cart(order.goods.name, list[order.goods.name]);
                        added = true;
                        order_.IsEnabled = true;
                    }
                }
            };
            Navigation.PushAsync(order);
        }

        void add_to_cart(string name_of_purchase, UInt64 good_quantity)
        {

            Grid grid = new Grid()
            {
                Padding = 5,
                VerticalOptions = LayoutOptions.Start,
            };

            Image photo = new Image()
            {
                Source = name_of_purchase + ".jpg",
                WidthRequest = 50,
                HeightRequest = 50,
            };
            Label nickname = new Label()
            {
                Text = name_of_purchase,
            };
            Label ammount = new Label()
            {
                Text = good_quantity.ToString(),
            };
            Label payment = new Label()
            {
                Text = (good_quantity * good.goods[name_of_purchase]).ToString(),
            };
            Button delete = new Button
            {
                Text = "Отмена",
                CornerRadius = 15,
                WidthRequest = 150,
                HeightRequest = 5,
                FontSize = 9,
                BackgroundColor = Color.Red,
            };

            grid.Children.Add(photo, 0, 0);
            grid.Children.Add(nickname, 1, 0);
            grid.Children.Add(ammount, 2, 0);
            grid.Children.Add(payment, 3, 0);
            grid.Children.Add(delete, 4, 0);
            invce.Children.Add(grid);
            delete.Clicked += (_s_, _e_) =>
            {
                invce.Children.Remove(grid);
                list.Remove(name_of_purchase);
                if (list.Count == 0)
                {
                    order_.IsEnabled = false;
                }
            };
        }
    }
}