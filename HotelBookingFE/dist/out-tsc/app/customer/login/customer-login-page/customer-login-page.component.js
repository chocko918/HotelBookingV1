import { __decorate } from "tslib";
import { Component } from '@angular/core';
let CustomerLoginPageComponent = class CustomerLoginPageComponent {
    constructor(route) {
        this.route = route;
        this.token = '';
        this.user = {};
    }
    ngOnInit() {
        // Extract token and user information from route parameters
        const state = history.state;
        if (state && state.token && state.user) {
            this.token = state.token;
            this.user = state.user;
            console.log(this.user);
        }
    }
};
CustomerLoginPageComponent = __decorate([
    Component({
        selector: 'app-customer-login-page',
        templateUrl: './customer-login-page.component.html',
        styleUrl: './customer-login-page.component.css'
    })
], CustomerLoginPageComponent);
export { CustomerLoginPageComponent };
//# sourceMappingURL=customer-login-page.component.js.map