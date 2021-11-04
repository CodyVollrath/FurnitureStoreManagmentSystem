using System;
using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    /// <summary>The View Model for the Customers Window</summary>
    /// <author>Daniel Crumpler</author>
    public class FurnitureViewModel
    {
        private FurnitureDal furnitureDal { get; set; }
        private ObservableCollection<Furniture> furnitureSearchResults;

        /// <summary>Gets or sets the error label.</summary>
        /// <value>The error label.</value>
        public string ErrorLabel { get; set; } = "";

        /// <summary>Gets or sets the customer search results.</summary>
        /// <value>The customer search results.</value>
        public ObservableCollection<Furniture> FurnitureSearchResults
        {
            get { return this.furnitureSearchResults; }
            set
            {
                furnitureSearchResults = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Initializes a new instance of the <see cref="FurnitureViewModel" /> class.</summary>
        public FurnitureViewModel()
        {
            this.furnitureDal = new FurnitureDal();
        }

        public ObservableCollection<Furniture> LoadSearchResults(String search)
        {

            List<Furniture> searchResults = new List<Furniture>();
            var results = this.furnitureDal.GetFurniture();
            searchResults = searchResults.Concat(results).ToList();
            List<Furniture> appendedResults = new List<Furniture>();
            foreach (var fur in searchResults)
            {
                if (fur.ItemName.ToLower().Contains(search.ToLower()) || fur.ItemDescription.ToLower().Contains(search.ToLower()) ||
                    fur.StyleName.ToLower().Contains(search.ToLower()) || fur.CategoryName.ToLower().Contains(search.ToLower()) ||
                    fur.Id.ToString().ToLower().Contains(search.ToLower()) || fur.Quantity.ToString().ToLower().Contains(search.ToLower()))
                {
                    appendedResults.Add(fur);
                }
            }
            this.FurnitureSearchResults = appendedResults.ConvertToObservable();
            return appendedResults.ConvertToObservable();
        }

        /// <summary>Occurs when [property changed].</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
