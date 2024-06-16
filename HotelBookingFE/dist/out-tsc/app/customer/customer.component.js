import { __decorate } from "tslib";
import { Component } from '@angular/core';
let CustomerComponent = class CustomerComponent {
    constructor(service) {
        this.service = service;
        this.CustomerList = [];
    }
    ngOnInit() {
        this.refreshCustomerList();
    }
    refreshCustomerList() {
        console.log("REFRESHED");
        this.service.getCustomerList().subscribe(data => {
            this.CustomerList = data;
            console.log(this.CustomerList);
        });
    }
};
CustomerComponent = __decorate([
    Component({
        selector: 'app-customer',
        templateUrl: './customer.component.html',
        styleUrls: ['./customer.component.css']
    })
], CustomerComponent);
export { CustomerComponent };
//# sourceMappingURL=customer.component.js.map