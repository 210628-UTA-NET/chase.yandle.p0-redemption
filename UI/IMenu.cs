using System;

namespace UI
{
    public enum MenuTitle
    {
        SplashMenu,
        LoginMenu,
        MainMenu,
        AddMenu,
        SearchMenu,
        OrderMenu,
        CustomerAddMenu,
        LocationAddMenu,
        CustomerSearchMenu,
        LocationSearchMenu,
        ProductSearchMenu,
        GameSearchMenu,
        SystemSearchMenu,
        CustomerOrderMenu,
        StockOrderMenu,
        StockOrderSearchMenu,
        CustomerOrderSearchMenu,
        LineItemSearch,
        Exit,
        Error
    }
    public interface IMenu
    {
        void Menu();
        MenuTitle UInput();
    }
}