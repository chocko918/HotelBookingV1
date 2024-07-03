import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CookieService } from 'ngx-cookie-service';
import { v4 as uuidv4 } from 'uuid';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { RoomComponent } from './room/room.component';
import { LoginComponent } from './customer/login/login.component';
import { ServicesService } from './services.service';
import { SharedService } from './shared.service';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomerLoginPageComponent } from './customer/login/customer-login-page/customer-login-page.component';
import { RoomAvailabilityComponent } from './room/room-availability/room-availability.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './customer/register/register.component';
import { NavbarComponent } from './navbar/navbar.component';
import { BookinginfoComponent } from './customer/bookinginfo/bookinginfo.component';


@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    RoomComponent,
    LoginComponent,
    CustomerLoginPageComponent,
    RoomAvailabilityComponent,
    CartComponent,
    RegisterComponent,
    NavbarComponent,
    BookinginfoComponent,

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
    SharedService,

  ],
   
  bootstrap: [AppComponent]
})
export class AppModule { }
