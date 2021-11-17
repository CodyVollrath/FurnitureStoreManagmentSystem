using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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

        public string FurnitureId { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Category { get; set; }

        public List<string> Categories { get; set; }

        public List<string> Styles { get; set; }
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

            this.Categories = (List<string>) this.FurnitureDal.GetCategories();
            this.Styles = (List<string>)this.FurnitureDal.GetStyles();
        }

        #endregion

        #region Methods

        /// <summary>Loads the cart.</summary>
        /// <returns>
        ///     A observable furniture list
        /// </returns>
        public ObservableCollection<Furniture> LoadCart()
        {
            this.Cart = Singletons.FurnitureCart.ConvertToObservable();
            return Singletons.FurnitureCart.ConvertToObservable();
        }

        public void LoadSearchResults() 
        {
            int id;
            List<Furniture> searchResults = new List<Furniture>();
            this.ValidateFields();
            if (this.FurnitureId != null && this.FurnitureId != string.Empty) 
            {
                id = int.Parse(this.FurnitureId);
                var results = this.FurnitureDal.GetFurnituresById(id);
                searchResults = searchResults.Concat(results).ToList();
            }

            if (this.Name != null && this.Name != string.Empty) 
            {
                var results = this.FurnitureDal.GetFurnituresByName(this.Name);
                searchResults = searchResults.Concat(results).ToList();
            }
            
            if (this.Style != null && this.Style != string.Empty) 
            {
                var results = this.FurnitureDal.GetFurnituresByStyleName(this.Style);
                searchResults = searchResults.Concat(results).ToList();
            }

            if (this.Category != null && this.Category != string.Empty) 
            {
                var results = this.FurnitureDal.GetFurnituresByCategoryName(this.Category);
                searchResults = searchResults.Concat(results).ToList();
            }

            this.FurnitureSearchResults = searchResults.ConvertToObservable();
        }

        private void ValidateFields() 
        {
            Regex idRegex = new Regex(@"^\d*$");
            if (this.FurnitureId != null && this.FurnitureId != string.Empty && !idRegex.IsMatch(this.FurnitureId)) 
            {
                this.ErrorLabel = "Invalid ID Entered";
            }
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