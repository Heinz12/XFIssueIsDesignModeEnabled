using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using XFIssueIsDesignModeEnabled.Models;
using XFIssueIsDesignModeEnabled.Views;

namespace XFIssueIsDesignModeEnabled.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(), canExecute: () => !IsRefreshing);

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        int count = 0;
        public int Count
        {
            get { return count; }
            set { SetProperty(ref count, value); }
        }
        bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (SetProperty(ref isRefreshing, value))
                {
                    AddItemCommand?.ChangeCanExecute();
                }
            }
        }
        async Task ExecuteLoadItemsCommand()
        {
            //IsBusy = true;
            //IsRefreshing = true;
            ++Count;

            await Task.Delay(3000);     // dummy delay used to reproduce hanging of RefreshView Indicator on iOS
            Debug.WriteLine($"ExecuteLoadItemsCommand: count={Count:00}");

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}