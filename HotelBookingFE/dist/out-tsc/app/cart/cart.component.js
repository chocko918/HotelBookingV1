import { __decorate } from "tslib";
import { Component } from '@angular/core';
let CartComponent = class CartComponent {
    constructor(service, cookieService) {
        this.service = service;
        this.cookieService = cookieService;
        this.CartList = [];
        this.TotalPrice = 0;
    }
    ngOnInit() {
        this.refreshCartList();
    }
    refreshCartList() {
        console.log("REFRESHED Cart");
        this.service.getAllCartItems().subscribe((data) => {
            this.CartList = data.rooms;
            this.TotalPrice = data.totalPrice;
            console.log(data);
        }, error => {
            console.error('Error fetching cart items:', error);
        });
    }
    deleteCartItem(itemID) {
        if (confirm("Are you sure you want to delete this item?")) {
            this.service.deleteCartItem(itemID).subscribe(response => {
                console.log("Item deleted:", response);
                this.refreshCartList();
            }, error => {
                console.error("Error deleting item:", error);
            });
        }
    }
    confirmCartItem() {
        const customerID = this.cookieService.get('customerID');
        if (!customerID) {
            console.error('CustomerID not found in cookies.');
            return;
        }
        this.service.confirmCartItem(customerID).subscribe(response => {
            // Handle successful confirmation
            console.log('Cart item confirmed:', response);
            // Perform any additional actions if needed
        }, error => {
            // Handle error
            console.error('Error confirming cart item:', error);
        });
    }
};
CartComponent = __decorate([
    Component({
        selector: 'app-cart',
        templateUrl: './cart.component.html',
        styleUrls: ['./cart.component.css']
    })
], CartComponent);
export { CartComponent };
//# sourceMappingURL=cart.component.js.map