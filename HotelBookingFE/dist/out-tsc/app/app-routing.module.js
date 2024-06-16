import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { LoginComponent } from './customer/login/login.component';
import { CustomerLoginPageComponent } from './customer/login/customer-login-page/customer-login-page.component';
import { RoomComponent } from './room/room.component';
import { RoomAvailabilityComponent } from './room/room-availability/room-availability.component';
import { CartComponent } from './cart/cart.component';
const routes = [
    { path: 'customer', component: CustomerComponent },
    { path: 'login', component: LoginComponent },
    { path: 'loginpage', component: CustomerLoginPageComponent },
    { path: 'room', component: RoomComponent },
    { path: 'roomAvailability', component: RoomAvailabilityComponent },
    { path: 'cart', component: CartComponent },
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = __decorate([
    NgModule({
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map