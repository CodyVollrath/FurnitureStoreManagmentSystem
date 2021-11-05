using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using JetBrains.Annotations;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    /// <summary>The View Model for the Customers Window</summary>
    /// <author>Daniel Crumpler</author>
    public class FurnitureViewModel
    {
        #region Data members

        private ObservableCollection<Furniture> furnitureSearchResults;

        private ObservableCollection<Furniture> cart;

        #endregion

        #region Properties

        private FurnitureDal FurnitureDal { get; }

        /// <summary>Gets or sets the error label.</summary>
        /// <value>The error label.</value>
        public string ErrorLabel { get; set; } = "";

        /// <summary>Gets or sets the furniture search results.</summary>
        /// <value>The furniture search results.</value>
        public ObservableCollection<Furniture> FurnitureSearchResults
        {
            get => this.furnitureSearchResults;
            set
            {
                this.furnitureSearchResults = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the furniture cart.</summary>
        /// <value>The furniture cart.</value>
        public ObservableCollection<Furniture> Cart
        {
            get => this.cart;
            set
            {
                this.cart = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="FurnitureViewModel" /> class.</summary>
        public FurnitureViewModel()
        {
            this.FurnitureDal = new FurnitureDal();
        }

        #endregion

        #region Methods

        /// <summary>Loads the search results.</summary>
        /// <param name="search">The search.</param>
        /// <returns>
        ///     A observable furniture list
        /// </returns>
        public ObservableCollection<Furniture> LoadSearchResults(string search)
        {
            var searchResults = new List<Furniture>();
            var results = this.FurnitureDal.GetFurniture();
            searchResults = searchResults.Concat(results).ToList();
            var appendedResults = new List<Furniture>();
            foreach (var fur in searchResults)
            {
                if (fur.ItemName.ToLower().Contains(search.ToLower()) ||
                    fur.ItemDescription.ToLower().Contains(search.ToLower()) ||
                    fur.StyleName.ToLower().Contains(search.ToLower()) ||
                    fur.CategoryName.ToLower().Contains(search.ToLower()) ||
                    fur.Id.ToString().ToLower().Contains(search.ToLower()) ||
                    fur.Quantity.ToString().ToLower().Contains(search.ToLower()) ||
                    fur.Price.ToString().ToLower().Contains(search.ToLower()))
                {
                    appendedResults.Add(fur);
                }
            }

            this.FurnitureSearchResults = appendedResults.ConvertToObservable();
            return appendedResults.ConvertToObservable();
        }

        /// <summary>Loads the cart.</summary>
        /// <returns>
        ///     A observable furniture list
        /// </returns>
        public ObservableCollection<Furniture> LoadCart()
        {
            this.Cart = Singletons.FurnitureCart.ConvertToObservable();
            return Singletons.FurnitureCart.ConvertToObservable();
        }

        /// <summary>Creates the transaction.</summary>
        /// <param name="cID">The customer identifier.</param>
        public void CreateTransaction(int cID)
        {
            this.FurnitureDal.CreateTransaction(Singletons.CurrentEmployee.Id, cID);
        }

        /// <summary>Creates the item check out.</summary>
        /// <param name="fID">The furniture identifier.</param>
        /// <param name="tID">The transaction identifier.</param>
        /// <param name="quantity">The quantity.</param>
        public void CreateItemCheckOut(int fID, int tID, int quantity)
        {
            this.FurnitureDal.CreateItemCheckOut(fID, tID, quantity);
        }

        /// <summary>Modifies the furniture quantity.</summary>
        /// <param name="fID">The furniture identifier.</param>
        /// <param name="quantity">The quantity.</param>
        public void ModifyFurnitureQuantity(int fID, int quantity)
        {
            this.FurnitureDal.ModifyFurnitureQuantity(fID, quantity);
        }

        /// <summary>Creates the rental transaction.</summary>
        /// <param name="tID">The transaction identifier.</param>
        /// <param name="cost">The cost.</param>
        public void CreateRental(int tID, double cost)
        {
            this.FurnitureDal.CreateRental(tID, cost);
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

        #endregion
    }
}