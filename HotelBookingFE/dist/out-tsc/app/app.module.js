import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { RoomComponent } from './room/room.component';
import { LoginComponent } from './customer/login/login.component';
import { ServicesService } from './services.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomerLoginPageComponent } from './customer/login/customer-login-page/customer-login-page.component';
import { RoomAvailabilityComponent } from './room/room-availability/room-availability.component';
import { CartComponent } from './cart/cart.component';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            CustomerComponent,
            RoomComponent,
            LoginComponent,
            CustomerLoginPageComponent,
            RoomAvailabilityComponent,
            CartComponent,
        ],
        imports: [
            BrowserModule,
            AppRoutingModule,
            HttpClientModule,
            FormsModule,
            ReactiveFormsModule,
            NgbModule,
        ],
        providers: [
            ServicesService,
            CookieService,
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map